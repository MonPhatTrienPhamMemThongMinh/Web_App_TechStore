using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;
using Microsoft.Reporting.WinForms;
namespace App_QLWeb_DoDienTu
{
    public partial class frmPhieuNhapHang : Form
    {
        private string maPhieuNhap;
        private PhieuNhap phieuNhap;
        private PhieuDat phieuDat;
        private NhaCungCap nhaCungCap;
        private List<ChiTietPhieuNhap> chiTietPhieuNhaps;
        private PhieuNhapBLL phieuNhapBLL;
        private PhieuDatBLL phieuDatBLL;
        private SupplierBLL nhaCungCapBLL;
        private ChiTietPhieuNhapBLL chiTietPhieuNhapBLL;
        public delegate void SendDataHandler(bool loadData);
        public event SendDataHandler DongForm;
        public frmPhieuNhapHang(string maPhieuNhap)
        {
            InitializeComponent();
            this.maPhieuNhap = maPhieuNhap;
            this.chiTietPhieuNhaps = new List<ChiTietPhieuNhap>();
            this.phieuNhapBLL = new PhieuNhapBLL();
            this.phieuDatBLL = new PhieuDatBLL();
            this.nhaCungCapBLL = new SupplierBLL();
            this.chiTietPhieuNhapBLL = new ChiTietPhieuNhapBLL();
        }

        private void frmPhieuNhapHang_Load(object sender, EventArgs e)
        {
            phieuNhap = phieuNhapBLL.TimKiemPhieuNhapTheoMaPhieuNhap(maPhieuNhap);
            chiTietPhieuNhaps = chiTietPhieuNhapBLL.LayDanhSachChiTietPhieuNhap(maPhieuNhap);
            phieuDat = phieuDatBLL.TimKiemPhieuDatTheoMaPhieuDat(phieuNhap.MaPhieuDat);
            nhaCungCap = nhaCungCapBLL.GetSupplierById(phieuDat.MaNhaCungCap);
            decimal tongTien = decimal.Parse(phieuNhap.TongTien.ToString().Split(',')[0]);
            ReportParameter[] reportParameters = new ReportParameter[] {
                new ReportParameter("tenNhaCungCap", nhaCungCap.tenNhaCungCap),
                new ReportParameter("diaChi", nhaCungCap.diaChi),
                new ReportParameter("soDienThoai", nhaCungCap.soDienThoai),
                new ReportParameter("ngayNhap", phieuNhap.NgayNhap.ToString("dd-MM-yyyy")),
                new ReportParameter("tongTienPhieuNhap", tongTien.ToString("C0"))
            };
            ReportDataSource rdsChiTiet = new ReportDataSource("ChiTietPhieuNhaps", chiTietPhieuNhaps);
            phieuNhapReportView.LocalReport.DataSources.Clear();
            phieuNhapReportView.LocalReport.DataSources.Add(rdsChiTiet);
            phieuNhapReportView.LocalReport.SetParameters(reportParameters);
            this.phieuNhapReportView.RefreshReport();
        }

        private void frmPhieuNhapHang_FormClosed(object sender, FormClosedEventArgs e)
        {
            DongForm?.Invoke(true);
        }
    }
}
