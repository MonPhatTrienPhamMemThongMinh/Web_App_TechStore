using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class SupplierDAL
    {
        DBGAMINGGEARDataContext db = new DBGAMINGGEARDataContext();

        public SupplierDAL()
        {

        }

        public List<NhaCungCap> getAllSuppliers()
        {
            return db.NhaCungCaps.Select(s => s).ToList<NhaCungCap>();
        }

        public bool AddSupplier(NhaCungCap supplier)
        {
            try
            {
                db.NhaCungCaps.InsertOnSubmit(supplier);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public NhaCungCap GetNCCById(string ma)
        {
            try
            {
                NhaCungCap ncc = db.NhaCungCaps.FirstOrDefault(n => n.maNhaCungCap == ma);
                if (ncc != null)
                {
                    return ncc;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Lỗi tải thông tin nhà cung cấp: " + ex.Message, ex);
            }
        }

        public bool UpdateNCC(NhaCungCap ncc)
        {
            try
            {
                NhaCungCap newNcc = db.NhaCungCaps.FirstOrDefault(n => n.maNhaCungCap == ncc.maNhaCungCap);
                if (newNcc != null)
                {
                    newNcc.tenNhaCungCap = ncc.tenNhaCungCap;
                    newNcc.soDienThoai = ncc.soDienThoai;
                    newNcc.email = ncc.email;
                    newNcc.diaChi = ncc.diaChi;
                    db.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Lỗi sửa thông tin nhà cung cấp: " + ex.Message, ex);
            }
        }

        public bool DeleteNCC(NhaCungCap ncc)
        {
            try
            {
                NhaCungCap nccDeleted = db.NhaCungCaps.Where(nhaCC => nhaCC.maNhaCungCap == ncc.maNhaCungCap).Select(nhaCC => nhaCC).First();
                db.NhaCungCaps.DeleteOnSubmit(nccDeleted);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string TaoMaNhaCungCap()
        {
            var nhaCungCaps = db.NhaCungCaps.ToList();
            if (nhaCungCaps.Any())
            {
                var nhaCungCapCuoi = nhaCungCaps.OrderByDescending(ncc => int.Parse(ncc.maNhaCungCap.Substring(3))).FirstOrDefault();

                string nhaCungCapCuoiID = nhaCungCapCuoi.maNhaCungCap;
                int stt = int.Parse(nhaCungCapCuoiID.Substring(3)) + 1;

                return "NCC" + stt.ToString("D3");
            }
            return "NCC001";
        }
    }
}
