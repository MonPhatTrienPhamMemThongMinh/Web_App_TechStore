using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDN
{
    public class QL_NguoiDung
    {
        public QL_NguoiDung() { }
        public int Check_Config(string pConn)
        {
            if (pConn == string.Empty)
                return 1; // Chuỗi ko tồn tại
            SqlConnection _sqlConn = new SqlConnection(pConn);
            try
            {
                if (_sqlConn.State == System.Data.ConnectionState.Closed)
                    _sqlConn.Open();
                return 0; // Chuỗi cấu hình hợp lệ -> Kết nối thành công

            }
            catch
            {
                return 2; // Chuỗi cấu hình ko phù hợp
            }
        }
    }
}
