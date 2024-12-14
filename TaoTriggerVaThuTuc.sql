USE DBGAMINGGEAR
GO
-------------------------------------------------TRIGGER
CREATE TRIGGER TRG_CapNhatTrangThaiSP ON Products
AFTER UPDATE, INSERT 
AS
BEGIN
	DECLARE @soLuong INT,@maSP VARCHAR(50)
	SELECT @soLuong = Quantity, @maSP = ProductID FROM inserted
	IF(@soLuong=0)
		BEGIN
			UPDATE Products
			SET AvailabilityStatus = N'OutOfStock' 
			WHERE ProductID = @maSP
		END
	ELSE
		BEGIN
			UPDATE Products
			SET AvailabilityStatus = N'InStock' 
			WHERE ProductID = @maSP
		END
END
GO
CREATE TRIGGER TRG_CapNhatSoLuongSanPhamSauKhiThanhToan
ON OrderDetails
AFTER INSERT
AS
BEGIN
    -- Cập nhật số lượng sản phẩm trong bảng SanPham
    UPDATE Products
    SET Quantity = Products.Quantity - i.Quantity
    FROM inserted i
    WHERE Products.ProductID = i.ProductID;
END;
GO
CREATE TRIGGER TRG_UpdateDonGiaSaleKhiDungThuCong
ON KhuyenMais
AFTER UPDATE
AS
BEGIN
	UPDATE Products
	SET SalePrice = NULL
	FROM inserted i
	WHERE i.trangThai = N'Đã kết thúc'
END
GO
CREATE TRIGGER TRG_TaoChiTietPhieuNhap ON ChiTietPhieuNhaps
AFTER INSERT
AS
BEGIN
	DECLARE @maSP VARCHAR(50),@maPD VARCHAR(50), @soLuong INT, @donGiaNhap DECIMAL(18,2)
	SELECT @maSP = ProductID, @maPD = MaPhieuDat, @soLuong = SoLuong, @donGiaNhap = DonGia FROM INSERTED

	DECLARE @soLuongDaNhan INT, @soLuongDat INT
	SELECT @soLuongDaNhan = SoLuongNhan, @soLuongDat = SoLuongDat FROM ChiTietPhieuDats WHERE ProductID = @maSP AND MaPhieuDat = @maPD
	IF(@soLuongDaNhan + @soLuong > @soLuongDat)
		ROLLBACK
	ELSE
		BEGIN
			--Cập nhật số lượng mới
			UPDATE Products
			SET Quantity = Quantity + @soLuong
			WHERE ProductID = @maSP			
			--Cập nhật đơn giá bán
			UPDATE Products
			SET Price = @donGiaNhap+(@donGiaNhap*40/100)
			WHERE ProductID = @maSP			
			--Cập nhật số lượng đã nhận
			UPDATE ChiTietPhieuDats
			SET soLuongNhan = soLuongNhan + @soLuong
			WHERE ProductID = @maSP AND MaPhieuDat = @maPD
		END
END
GO
-------------------------------------------------PROCEDURE
GO
CREATE PROCEDURE XoaPhieuDat_Proc @maPhieuDat VARCHAR(50)
AS
	--Xóa chi tiết phiếu đặt
	DELETE ChiTietPhieuDats WHERE MaPhieuDat = @maPhieuDat
	--Xóa phiếu đặt
	DELETE PhieuDats WHERE MaPhieuDat = @maPhieuDat
GO
CREATE PROCEDURE sp_UpdateTrangThaiKhuyenMai
AS
BEGIN
    DECLARE @tgHienTai DATETIME = GETDATE();

    -- Cập nhật trạng thái của khuyến mãi, bỏ qua khuyến mãi đã có trạng thái 'Đã kết thúc'
    UPDATE KhuyenMais
	SET trangThai = N'Đang diễn ra'
	WHERE @tgHienTai >= ngayBatDau 
	  AND @tgHienTai <= ngayKetThuc 
	  AND (trangThai IS NULL OR trangThai NOT IN (N'Đã kết thúc'));

	UPDATE KhuyenMais
	SET trangThai = N'Chưa diễn ra'
	WHERE @tgHienTai < ngayBatDau
	  AND (trangThai IS NULL OR trangThai NOT IN (N'Đã kết thúc'));

	UPDATE KhuyenMais
	SET trangThai = N'Đã kết thúc'
	WHERE @tgHienTai > ngayKetThuc
	  AND (trangThai IS NULL OR trangThai NOT IN (N'Đã kết thúc'));


    -- Cập nhật trạng thái của KhuyenMaiSanPham khi KhuyenMai chuyển sang 'Đã kết thúc'
    UPDATE KhuyenMaiSanPhams
    SET trangThai = N'Hết hiệu lực'
    WHERE maKhuyenMai IN (
        SELECT maKhuyenMai
        FROM KhuyenMais
        WHERE trangThai = N'Đã kết thúc'
    );

    -- Cập nhật trạng thái của KhuyenMaiSanPham khi KhuyenMai chuyển sang 'Đang diễn ra'
    UPDATE KhuyenMaiSanPhams
    SET trangThai = N'Có hiệu lực'
    WHERE maKhuyenMai IN (
        SELECT maKhuyenMai
        FROM KhuyenMais
        WHERE trangThai = N'Đang diễn ra'
    );

    -- Cập nhật donGiaSale cho sản phẩm thuộc khuyến mãi đang diễn ra
    UPDATE Products
    SET SalePrice = Price - (Price * kmsp.phanTramGiam / 100)
    FROM Products sp
    JOIN KhuyenMaiSanPhams kmsp ON sp.ProductID = kmsp.maSanPham
    JOIN KhuyenMais km ON kmsp.maKhuyenMai = km.maKhuyenMai
    WHERE km.trangThai = N'Đang diễn ra';

    -- Cập nhật donGiaSale của SanPham thành NULL khi KhuyenMai kết thúc
	UPDATE Products
	SET SalePrice = NULL
	WHERE ProductID IN (
    SELECT kmsp.maSanPham
    FROM KhuyenMaiSanPhams kmsp
    LEFT JOIN KhuyenMais km ON kmsp.maKhuyenMai = km.maKhuyenMai
    WHERE kmsp.maSanPham = Products.ProductID
      AND NOT EXISTS (
          SELECT 1
          FROM KhuyenMais km2
          JOIN KhuyenMaiSanPhams kmsp2 ON km2.maKhuyenMai = kmsp2.maKhuyenMai
          WHERE km2.trangThai = N'Đang diễn ra'
            AND kmsp2.maSanPham = kmsp.maSanPham
      )
	);
END
