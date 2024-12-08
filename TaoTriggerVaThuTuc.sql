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
