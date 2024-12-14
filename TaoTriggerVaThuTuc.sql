USE DBGAMINGGEAR
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

CREATE PROCEDURE XoaPhieuDat_Proc @maPhieuDat VARCHAR(50)
AS
	--Xóa chi tiết phiếu đặt
	DELETE ChiTietPhieuDats WHERE MaPhieuDat = @maPhieuDat
	--Xóa phiếu đặt
	DELETE PhieuDats WHERE MaPhieuDat = @maPhieuDat
GO
