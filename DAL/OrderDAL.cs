using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThuVien
{
    public class OrderDAL
    {
        DBGAMINGGEARDataContext db = new DBGAMINGGEARDataContext();
        public OrderDAL() { }

        public List<dynamic> LoadAllOrders()
        {
            var orders = db.Orders
                .Select(o => new
                {
                    o.OrderId,
                    o.CreatedDate,
                    StatusText = o.Status ? "Đã thanh toán" : "Chưa thanh toán",
                    o.UserId,
                    o.CustomerName,
                    o.CustomerPhone,
                    o.CustomerAddress,
                    o.CustomerEmail,
                    o.PaymentMethod,
                    o.TotalAmount
                })
                .ToList();
            return orders.Cast<dynamic>().ToList();
        }        
    }
}
