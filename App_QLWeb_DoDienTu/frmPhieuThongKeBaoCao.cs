using DTO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_QLWeb_DoDienTu
{
    public partial class frmPhieuThongKeBaoCao : Form
    {
        private List<ThongKeDoanhThuTheoThang> _reportDoanhThuData;
        private List<SanPhamBanChay> _reportTop5SanPhamBanChayData;
        private int _month;
        private int _year;
        private string _loiNhuanText;
        private string _tenNhanVien;
        public frmPhieuThongKeBaoCao(List<ThongKeDoanhThuTheoThang> reportDoanhThuData, int month, int year, string loiNhuanText, 
                                     List<SanPhamBanChay> reportTop5SanPhamBanChayData, string tenNhanVien)
        {
            InitializeComponent();
            this._reportDoanhThuData = reportDoanhThuData;
            this._reportTop5SanPhamBanChayData = reportTop5SanPhamBanChayData;
            this._month = month;
            this._year = year;
            this._loiNhuanText = loiNhuanText;
            this._tenNhanVien = 
            _tenNhanVien = tenNhanVien;
        }

        private void frmPhieuThongKeBaoCao_Load(object sender, EventArgs e)
        {
            string reportPath = System.IO.Path.Combine(Application.StartupPath, "PhieuThongKeBaoCao.rdlc");
            rpPhieuBaoCao.LocalReport.ReportPath = reportPath;
            LoadReport(_reportDoanhThuData, _reportTop5SanPhamBanChayData);
            this.rpPhieuBaoCao.RefreshReport();
        }

        private void LoadReport(List<ThongKeDoanhThuTheoThang> reportDoanhThu, List<SanPhamBanChay> reportSanPhamBanChay)
        {
            // Doanh thu
            var reportDoanhThuTheoThang = reportDoanhThu.Select(x => new
            {
                ThangSo = x.ThangSo,
                TongDoanhThu = x.TongDoanhThu
            }).ToList();

            // Sản phẩm bán chạy
            var report5SanPhamBanChay = reportSanPhamBanChay.Select((x, stt) => new
            {
                STT = stt + 1,
                TenSanPham = x.TenSanPham,
                SoLuongBan = x.SoLuongBan
            }).ToList();

            ReportParameter[] reportParameters = new ReportParameter[] {
                new ReportParameter("Tháng", $"{_month}/{_year}"),
                new ReportParameter("LoiNhuan", _loiNhuanText),
                new ReportParameter("TenNhanVien", _tenNhanVien)
            };

            ReportDataSource rdsDoanhThu = new ReportDataSource("ThongKeDoanhThuTheoThangDataSet", reportDoanhThuTheoThang);
            ReportDataSource rdsSPBanChay = new ReportDataSource("SanPhamBanChayDataSet", report5SanPhamBanChay);

            rpPhieuBaoCao.LocalReport.DataSources.Clear();
            rpPhieuBaoCao.LocalReport.DataSources.Add(rdsDoanhThu);
            rpPhieuBaoCao.LocalReport.DataSources.Add(rdsSPBanChay);
            rpPhieuBaoCao.LocalReport.SetParameters(reportParameters);
            rpPhieuBaoCao.RefreshReport();
        }
    }
}
