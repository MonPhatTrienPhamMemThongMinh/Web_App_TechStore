using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class SupplierBLL
    {
        SupplierDAL sdal = new SupplierDAL();
        public SupplierBLL() { }

        public List<NhaCungCap> getAllSuppliers()
        {
            return sdal.getAllSuppliers();
        }

        public NhaCungCap GetSupplierById(string ma)
        {
            return sdal.GetNCCById(ma);
        }

        public bool AddSupplier(NhaCungCap ncc)
        {
            return sdal.AddSupplier(ncc);
        }

        public bool UpdateSupplier(NhaCungCap ncc)
        {
            return sdal.UpdateNCC(ncc);
        }

        public bool DeleteSupplier(NhaCungCap ncc)
        {
            return sdal.DeleteNCC(ncc);
        }

        public string TaoMaNhaCungCap()
        {
            return sdal.TaoMaNhaCungCap();
        }
    }
}
