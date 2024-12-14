using BLL;
using DTO;
using Microsoft.Reporting.WinForms;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace App_QLWeb_DoDienTu
{
    public partial class frmDashboard : Form
    {
        private string MaNhanVien;

        OrderBLL hdbll = new OrderBLL();
        UserBLL ubll = new UserBLL();
        SupplierBLL nccbll = new SupplierBLL();
        ProductBLL spbll = new ProductBLL();
        PhieuNhapBLL pnbll = new PhieuNhapBLL();
        private List<Sunny.UI.UIButton> buttonList;
        public frmDashboard(string maNhanVien)
        {
            InitializeComponent();
            this.MaNhanVien = maNhanVien;
            dtpChonThangInPhieu.MaxDate = DateTime.Today;
            this.Load += FrmDashboard_Load;
            this.dtpNgayBatDau.Value = DateTime.Today.AddDays(-7);
            this.dtpNgayKetThuc.Value = DateTime.Now;
            this.dtpNgayBatDau.ValueChanged += DtpNgayBatDau_ValueChanged;
            this.dtpNgayKetThuc.ValueChanged += DtpNgayKetThuc_ValueChanged;
            this.btnLocThangNay.Click += BtnLocThangNay_Click;
            this.btnLoc30NgayQua.Click += BtnLoc30NgayQua_Click;
            this.btnLoc7NgayQua.Click += BtnLoc7NgayQua_Click;
            this.btnLocHomNay.Click += BtnLocHomNay_Click;
            this.btnCustom.Click += BtnCustom_Click;
            this.btnLocTheoCustom.Click += BtnLocTheoCustom_Click;
            this.btnLocNamNay.Click += BtnLocNamNay_Click;
            buttonList = new List<Sunny.UI.UIButton>
            {
                btnLoc7NgayQua,
                btnLoc30NgayQua,
                btnLocHomNay,
                btnLocThangNay,
                btnLocNamNay,
                btnCustom
            };
            // Gán sự kiện click chung cho tất cả các nút trong danh sách
            foreach (var button in buttonList)
            {
                button.Click += Button_Click;
            }    
            this.dgvSPDuoiMucToiThieu.CellFormatting += DgvSPDuoiMucToiThieu_CellFormatting;
            this.btnInPhieuBaoCao.Click += BtnInPhieuBaoCao_Click; ;
        }
        private void BtnInPhieuBaoCao_Click(object sender, EventArgs e)
        {
            var month = dtpChonThangInPhieu.Value.Month;
            var year = dtpChonThangInPhieu.Value.Year;

            DateTime ngayBatDau = new DateTime(year, month, 1);
            DateTime ngayKetThuc = ngayBatDau.AddMonths(1).AddDays(-1);

            // doanh thu của tháng được chọn
            var reportData = hdbll.ThongKeTongDoanhThuTheoThangTrongNam(ngayBatDau, ngayKetThuc);

            // lợi nhuận
            decimal tongDoanhThu = hdbll.TinhDoanhThuTheoKhoangThoiGian(ngayBatDau, ngayKetThuc);
            decimal tongTienNhapHang = pnbll.TinhTongTienPhieuNhapTheoKhoangThoiGian(ngayBatDau, ngayKetThuc);
            decimal tienLoiNhuan = tongDoanhThu - tongTienNhapHang;
            string loiNhuanText = "Lợi nhuận: " + tienLoiNhuan.ToString("N0").Replace(",", ".") + "đ";

            // top 5 sản phẩm bán chạy
            var top5SanPhamBanChay = spbll.ThongKeTop5SanPhamBanChayNhat(ngayBatDau, ngayKetThuc);
            var dataSourceTop5SanPhamBanChay = top5SanPhamBanChay.Select((x, index) => new
            {
                STT = index + 1,
                MaSanPham = x.MaSanPham,
                TenSanPham = x.TenSanPham,
                SoLuongBan = x.SoLuongBan
            }).ToList();

            AspNetUser nv = ubll.LayThongTinTheoMa(MaNhanVien);

            frmPhieuThongKeBaoCao frm = new frmPhieuThongKeBaoCao(reportData, month, year, loiNhuanText, top5SanPhamBanChay, nv.FullName);
            frm.ShowDialog();
        }
        private void Button_Click(object sender, EventArgs e)
        {
            // Đặt lại màu mặc định cho tất cả các nút
            foreach (var button in buttonList)
            {
                button.FillColor = System.Drawing.SystemColors.Window;
                button.ForeColor = System.Drawing.Color.RoyalBlue;
            }

            // Thay đổi màu sắc của nút được nhấn
            var clickedButton = sender as Sunny.UI.UIButton;
            if (clickedButton != null)
            {
                clickedButton.FillColor = System.Drawing.Color.RoyalBlue;
                clickedButton.ForeColor = System.Drawing.Color.White;
            }
        }        
        private void DgvSPDuoiMucToiThieu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && decimal.TryParse(e.Value.ToString(), out _))
            {
                dgvSPDuoiMucToiThieu.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            DateTime ngayBatDau = DateTime.Now.AddDays(-7);
            DateTime ngayKetThuc = DateTime.Now;

            Button_Click(btnLoc7NgayQua, EventArgs.Empty);
            LoadData(ngayBatDau, ngayKetThuc);
        }

        private void LoadData(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            // 4 dữ liệu đầu
            //int tongKH = khbll.TongSoKhachHang();
            //lblSoLuongKH.Text = tongKH.ToString();
            lblSoLuongHD.Text = hdbll.TongSoHoaDonTheoKhoangThoiGian(ngayBatDau, ngayKetThuc).ToString("N0").Replace(",", ".");

            decimal tongDoanhThu = hdbll.TinhDoanhThuTheoKhoangThoiGian(ngayBatDau, ngayKetThuc);
            lblDoanhThu.Text = tongDoanhThu.ToString("N0").Replace(",", ".") + "đ";

            decimal tongTienNhapHang = pnbll.TinhTongTienPhieuNhapTheoKhoangThoiGian(ngayBatDau, ngayKetThuc);
            decimal tienLoiNhuan = tongDoanhThu - tongTienNhapHang;
            lblLoiNhuan.Text = tienLoiNhuan.ToString("N0").Replace(",", ".") + "đ";

            // top 5 sản phẩm
            var top5SanPhamBanChay = spbll.ThongKeTop5SanPhamBanChayNhat(ngayBatDau, ngayKetThuc);
            var dataSourceTop5SanPhamBanChay = top5SanPhamBanChay.Select(x => new
            {
                MaSanPham = x.MaSanPham,
                TenSanPham = x.TenSanPham,
                SoLuongBan = x.SoLuongBan
            }).ToList();
            chartTopSanPham.DataSource = dataSourceTop5SanPhamBanChay;
            chartTopSanPham.Series[0].XValueMember = "TenSanPham";
            chartTopSanPham.Series[0].YValueMembers = "SoLuongBan";
            chartTopSanPham.DataBind();

            var tongSoNgay = (ngayKetThuc - ngayBatDau).Days + 1;

            // Thống kê doanh thu
            if (tongSoNgay <= 1) // Hôm nay
            {
                // Thống kê doanh thu theo giờ
                var doanhThuTheoGio = hdbll.ThongKeTongDoanhThuTheoGioTrongNgay(ngayBatDau);
                var dataSource = new List<dynamic>();

                if (doanhThuTheoGio.Any())
                {
                    int firstHourWithData = doanhThuTheoGio.Keys.Min();
                    int lastHourWithData = doanhThuTheoGio.Keys.Max();

                    // Duyệt qua từng giờ từ giờ đầu tiên có dữ liệu đến giờ cuối cùng có dữ liệu
                    for (int i = firstHourWithData; i <= lastHourWithData + 1; i++)
                    {
                        decimal doanhThuHienTai = doanhThuTheoGio.ContainsKey(i) ? doanhThuTheoGio[i] : 0;

                        // Thêm dữ liệu cho giờ hiện tại
                        dataSource.Add(new
                        {
                            Gio = $"{i}:00",
                            TongDoanhThu = doanhThuHienTai
                        });
                    }
                }
                else
                {
                    // Xử lý khi không có dữ liệu
                    dataSource.Add(new
                    {
                        Gio = "No Data",
                        TongDoanhThu = 0
                    });
                }

                chartTongDoanhThu.DataSource = dataSource;
                chartTongDoanhThu.Series[0].XValueMember = "Gio";
                chartTongDoanhThu.Series[0].YValueMembers = "TongDoanhThu";
            }
            else if (tongSoNgay <= 30)
            {
                // Lọc theo ngày
                var doanhThuTheoNgay = hdbll.ThongKeTongDoanhThuCuaTungNgay(ngayBatDau, ngayKetThuc);
                var dataSource = doanhThuTheoNgay.Select(x => new
                {
                    Ngay = x.Key.Value.ToString("dd/MM"),
                    TongDoanhThu = x.Value
                }).ToList();

                chartTongDoanhThu.DataSource = dataSource;
                chartTongDoanhThu.Series[0].XValueMember = "Ngay";
                chartTongDoanhThu.Series[0].YValueMembers = "TongDoanhThu";
            }
            else if (tongSoNgay <= 90)
            {
                var doanhThuTheoNgay = hdbll.ThongKeTongDoanhThuCuaTungNgay(ngayBatDau, ngayKetThuc);
                int buocNhay = TinhBuocLinhHoat(doanhThuTheoNgay.Count);
                var dataSource = doanhThuTheoNgay.Select((x, index) => new
                {
                    Ngay = x.Key.Value.ToString("dd/MM"),
                    TongDoanhThu = x.Value,
                    HienThi = index % buocNhay == 0
                }).Where(x => x.HienThi).ToList();

                chartTongDoanhThu.DataSource = dataSource;
                chartTongDoanhThu.Series[0].XValueMember = "Ngay";
                chartTongDoanhThu.Series[0].YValueMembers = "TongDoanhThu";
            }
            else
            {
                // Lọc theo tháng
                var doanhThuTheoThang = hdbll.ThongKeTongDoanhThuTheoThangTrongNam(ngayBatDau, ngayKetThuc);
                var dataSource = doanhThuTheoThang.Select(x => new
                {
                    Thang = x.Thang.ToString("MM/yyyy"), // Chuyển DateTime thành chuỗi định dạng MM/yyyy
                    TongDoanhThu = x.TongDoanhThu
                }).ToList();

                chartTongDoanhThu.DataSource = dataSource;
                chartTongDoanhThu.Series[0].XValueMember = "Thang";
                chartTongDoanhThu.Series[0].YValueMembers = "TongDoanhThu";
            }
            chartTongDoanhThu.DataBind();
            // Kiểm tra số lượng điểm dữ liệu và thay đổi loại biểu đồ
            if (chartTongDoanhThu.Series[0].Points.Count == 1)
            {
                chartTongDoanhThu.Series[0].ChartType = SeriesChartType.Column;
            }
            else
            {
                chartTongDoanhThu.Series[0].ChartType = SeriesChartType.SplineArea;
            }
            // 3 dữ liệu ô dưới bên trái
            //int soLuongNhanVien = nvbll.TongSoLuongNhanVien();
            int soLuongNhaCungCap = nccbll.TongSoLuongNhaCungCap();
            int soLuongSanPham = spbll.TongSoLuongSanPham();
            //lblNhanVien.Text = "" + soLuongNhanVien;
            lblNhaCungCap.Text = "" + soLuongNhaCungCap;
            lblSanPham.Text = "" + soLuongSanPham;
            // các sản phẩm dưới mức tối thiểu (40)
            var dsSanPhamDuoiMucToiThieu = spbll.ThongKeDanhSachSanPhamDuoiMucToiThieu();
            dgvSPDuoiMucToiThieu.DataSource = dsSanPhamDuoiMucToiThieu.Select(sp => new
            {
                TenSanPham = sp.TenSanPham,
                SoLuong = sp.SoLuong
            }).ToList();
            dgvSPDuoiMucToiThieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSPDuoiMucToiThieu.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            dgvSPDuoiMucToiThieu.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvSPDuoiMucToiThieu.Columns["SoLuong"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private int TinhBuocLinhHoat(int tongSoNgay)
        {
            int maxSoNhom = 6;
            return (int)Math.Ceiling((double)tongSoNgay / maxSoNhom);
        }
        private void BtnLocNamNay_Click(object sender, EventArgs e)
        {
            DateTime ngayBatDau = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime ngayKetThuc = DateTime.Now;
            LoadData(ngayBatDau, ngayKetThuc);
            DisableLocCustom();
        }
        private void BtnLocTheoCustom_Click(object sender, EventArgs e)
        {
            DateTime ngayBatDau = dtpNgayBatDau.Value;
            DateTime ngayKetThuc = dtpNgayKetThuc.Value;
            LoadData(ngayBatDau, ngayKetThuc);
        }
        private void BtnCustom_Click(object sender, EventArgs e)
        {
            dtpNgayBatDau.Enabled = true;
            dtpNgayKetThuc.Enabled = true;
            btnLocTheoCustom.Visible = true;
        }
        private void BtnLocHomNay_Click(object sender, EventArgs e)
        {
            DateTime ngayBatDau = DateTime.Today;
            DateTime ngayKetThuc = DateTime.Now;
            LoadData(ngayBatDau, ngayKetThuc);
            DisableLocCustom();
        }
        private void BtnLoc7NgayQua_Click(object sender, EventArgs e)
        {
            DateTime ngayBatDau = DateTime.Now.AddDays(-7);
            DateTime ngayKetThuc = DateTime.Now;
            LoadData(ngayBatDau, ngayKetThuc);
            DisableLocCustom();
        }
        private void BtnLoc30NgayQua_Click(object sender, EventArgs e)
        {
            DateTime ngayBatDau = DateTime.Now.AddDays(-30);
            DateTime ngayKetThuc = DateTime.Now;
            LoadData(ngayBatDau, ngayKetThuc);
            DisableLocCustom();
        }
        private void BtnLocThangNay_Click(object sender, EventArgs e)
        {
            DateTime ngayBatDau = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime ngayKetThuc = DateTime.Now;
            LoadData(ngayBatDau, ngayKetThuc);
            DisableLocCustom();
        }
        public void DisableLocCustom()
        {
            dtpNgayBatDau.Enabled = false;
            dtpNgayKetThuc.Enabled = false;
            btnLocTheoCustom.Visible = false;
        }
        private void DtpNgayKetThuc_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpNgayBatDau.Value > dtpNgayKetThuc.Value)
            {
                this.dtpNgayBatDau.Value = dtpNgayKetThuc.Value;
            }
        }
        private void DtpNgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpNgayBatDau.Value > dtpNgayKetThuc.Value)
            {
                this.dtpNgayKetThuc.Value = dtpNgayBatDau.Value;
            }
        }
    }
}
