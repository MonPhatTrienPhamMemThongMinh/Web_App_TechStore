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
	SET donGiaSale = NULL
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
-------------------------------------------------PROCEDURE
GO
CREATE PROCEDURE XoaPhieuDat_Proc @maPhieuDat VARCHAR(50)
AS
	--Xóa chi tiết phiếu đặt
	DELETE ChiTietPhieuDats WHERE MaPhieuDat = @maPhieuDat
	--Xóa phiếu đặt
	DELETE PhieuDats WHERE MaPhieuDat = @maPhieuDat
GO
