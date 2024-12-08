using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderDetailDAL
    {
        DBGAMINGGEARDataContext db = new DBGAMINGGEARDataContext();
        public OrderDetailDAL() { }

        public List<OrderDetail> LoadOrderDetail(string mahd)
        {
            if (!string.IsNullOrEmpty(mahd))
            {
                var list = db.OrderDetails.Where(ct => ct.OrderId == mahd).Select(ct => ct).ToList<OrderDetail>();
                foreach (var item in list)
                {
                    item.ProductName = db.Products.Where(sp => sp.ProductID == item.ProductID).Select(sp => sp.ProductName).FirstOrDefault();
                }
                return list;
            }
            else
            {
                return new List<OrderDetail>();
            }
        }
    }
}
