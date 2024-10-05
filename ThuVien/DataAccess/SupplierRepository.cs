using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThuVien.Models;

namespace ThuVien.DataAccess
{
    public class SupplierRepository
    {
        private SQLClass sql;

        public SupplierRepository(string cnn)
        {
            sql = new SQLClass();
            sql.createConnection(cnn);
        }

        public List<Supplier> getAllSuppliers()
        {
            List<Supplier> ncclist = new List<Supplier>();
            string query = "SELECT maNhaCungCap, tenNhaCungCap, soDienThoai, diaChi, email FROM NhaCungCap";
            DataTable dt = sql.ExecuteQuery(query);

            if(dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ncclist.Add(new Supplier
                    {
                        SupplierID = row["maNhaCungCap"].ToString(),
                        SupplierName = row["tenNhaCungCap"].ToString(),
                        SupplierPhone = row["soDienThoai"].ToString(),
                        SupplierAddress = row["diaChi"].ToString(),
                        SupplierEmail = row["email"].ToString()
                    });
                }
            }

            return ncclist;
        }

        public bool AddSupplier(Supplier supplier)
        {
            string query = "INS";
            return true;
        }
    }
}
