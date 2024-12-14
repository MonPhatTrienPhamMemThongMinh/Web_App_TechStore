using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongKeDoanhThuTheoThang
    {
        public DateTime Thang { get; set; }
        public decimal TongDoanhThu { get; set; }
        public int ThangSo
        {
            get { return Thang.Month; }
        }
    }
}
