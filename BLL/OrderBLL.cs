using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThuVien;

namespace BLL
{
    public class OrderBLL
    {
        OrderDAL odal = new OrderDAL();
        public OrderBLL() { }

        public List<dynamic> LoadAllOrders()
        {
            return odal.LoadAllOrders();
        }
    }
}
