using BLL;
using DTO;
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
    public partial class frmOrder : Form
    {
        OrderBLL odbll = new OrderBLL();
        private string MaHoaDon;
        public frmOrder()
        {
            InitializeComponent();
            this.Load += FrmOrder_Load;
            this.dtpNgayBatDau.Value = this.dtpNgayKetThuc.Value;

            this.btnReset.Click += BtnReset_Click;
            this.btnTimKiem.Click += BtnTimKiem_Click;

            this.cboTieuChi.SelectedIndexChanged += CboTieuChi_SelectedIndexChanged;
            this.btnLocTheoNgay.Click += BtnLocTheoNgay_Click;
            this.btnLocHienTai.Click += BtnLocHienTai_Click;
            this.dtpNgayBatDau.ValueChanged += DtpNgayBatDau_ValueChanged;
            this.dtpNgayKetThuc.ValueChanged += DtpNgayKetThuc_ValueChanged;

            this.dgvHoaDon.SelectionChanged += DgvHoaDon_SelectionChanged;
            this.dgvHoaDon.CellFormatting += DgvHoaDon_CellFormatting;

            this.btnXemChiTiet.Click += BtnXemChiTiet_Click;
        }

        private void DtpNgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpNgayBatDau.Value > dtpNgayKetThuc.Value)
            {
                this.dtpNgayKetThuc.Value = dtpNgayBatDau.Value;
            }
        }

        private void DtpNgayKetThuc_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpNgayBatDau.Value > dtpNgayKetThuc.Value)
            {
                this.dtpNgayBatDau.Value = dtpNgayKetThuc.Value;
            }
        }

        private void DgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvHoaDon.SelectedRows[0];
                MaHoaDon = selectedRow.Cells["OrderID"].Value.ToString();
            }
        }

        private void BtnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MaHoaDon))
            {
                frmCTHD frm = new frmCTHD(MaHoaDon);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                dtpNgayKetThuc.Value = DateTime.Now.Date;
                dtpNgayBatDau.Value = DateTime.Now.Date;
                txtTimKiem.Text = "";
                txtTimKiem.Enabled = false;
                List<Order> dsHoaDon = odbll.LoadAllOrders();
                SettingDgv(dsHoaDon);
                LoadTieuChiCombobox();
                LoadStatusCombobox();

                decimal tongDoanhThu = odbll.TinhTongDoanhThu();
                lblTongDoanhThu.Text = "Tổng doanh thu: " + tongDoanhThu.ToString("N0").Replace(",", ".") + "đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLocHienTai_Click(object sender, EventArgs e)
        {
            this.dtpNgayKetThuc.Value = DateTime.Now.Date;
            this.dtpNgayBatDau.Value = DateTime.Now.Date;
            string tieuChi = cboTieuChi.SelectedItem.ToString();
            string tenTimKiem = txtTimKiem.Text.Trim();
            string trangThai = cboStatus.SelectedItem.ToString();
            DateTime ngayBatDau = DateTime.Now.Date;
            DateTime ngayKetThuc = DateTime.Now.Date;
            LoadDanhSachHoaDonTheoNgayLoc(tieuChi, tenTimKiem, ngayBatDau, ngayKetThuc, trangThai);
        }

        private void BtnLocTheoNgay_Click(object sender, EventArgs e)
        {
            string tieuChi = cboTieuChi.SelectedItem.ToString();
            string tenTimKiem = txtTimKiem.Text.Trim();
            string trangThai = cboStatus.SelectedItem.ToString();
            LoadDanhSachHoaDonTheoNgayLoc(tieuChi, tenTimKiem, dtpNgayBatDau.Value, dtpNgayKetThuc.Value, trangThai);
        }

        private void LoadDanhSachHoaDonTheoNgayLoc(string tieuChi, string tenTimKiem, DateTime ngayBatDau, DateTime ngayKetThuc, string trangThai)
        {
            decimal tongDoanhThu = 0;

            List<Order> ketQuaTimKiem = odbll.TimKiemVaLocHoaDon(tieuChi, tenTimKiem, ngayBatDau, ngayKetThuc, trangThai)
                                             .OrderByDescending(hd => hd.CreatedDate).ToList();
            tongDoanhThu = ketQuaTimKiem.Sum(hd => hd.TotalAmount);
            SettingDgv(ketQuaTimKiem);
            lblTongDoanhThu.Text = "Tổng doanh thu: " + tongDoanhThu.ToString("N0").Replace(",", ".") + "đ";
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            string tieuChi = cboTieuChi.SelectedItem.ToString();
            string tenTimKiem = txtTimKiem.Text.Trim();
            string trangThai = cboStatus.SelectedItem.ToString();
            LoadDanhSachHoaDonTheoNgayLoc(tieuChi, tenTimKiem, dtpNgayBatDau.Value, dtpNgayKetThuc.Value, trangThai);
        }

        // Dữ liệu số nằm bên phải
        private void DgvHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvHoaDon.Columns[e.ColumnIndex].Name == "CustomerPhone")
            {
                return;
            }
            if (dgvHoaDon.Columns[e.ColumnIndex].Name == "CustomerAddress")
            {
                return;
            }
            if (dgvHoaDon.Columns[e.ColumnIndex].Name == "OrderID")
            {
                return;
            }
            if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal tien))
            {
                dgvHoaDon.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (tien != 0)
                {
                    e.Value = tien.ToString("N0").Replace(",", ".") + "đ";
                }
                else
                {
                    e.Value = tien.ToString("N0").Replace(",", ".");
                }
                e.FormattingApplied = true;
            }
        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            this.dtpNgayBatDau.MaxDate = DateTime.Now.Date;
            this.dtpNgayKetThuc.MaxDate = DateTime.Now.Date;
            List<Order> orders = odbll.LoadAllOrders();
            SettingDgv(orders);
            LoadTieuChiCombobox();
            LoadStatusCombobox();

            decimal tongDoanhThu = odbll.TinhTongDoanhThu();
            lblTongDoanhThu.Text = "Tổng doanh thu: " + tongDoanhThu.ToString("N0").Replace(",", ".") + "đ";
        }

        private void CboTieuChi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTieuChi.SelectedItem.ToString() == "Các tiêu chí")
            {
                txtTimKiem.Enabled = false;
                txtTimKiem.Text = "";
            }
            else
            {
                txtTimKiem.Enabled = true;
            }
        }

        private void SettingDgv(List<Order> dsHoaDon)
        {
            dgvHoaDon.DataSource = dsHoaDon;
            dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            if (dgvHoaDon.Columns["AspNetUser"] != null)
            {
                dgvHoaDon.Columns["AspNetUser"].Visible = false;
            }
            if (dgvHoaDon.Columns["Status"] != null)
            {
                dgvHoaDon.Columns["Status"].Visible = false;
            }
            if (dgvHoaDon.Columns["OrderID"] != null)
            {
                dgvHoaDon.Columns["OrderID"].HeaderText = "Mã hóa đơn";
            }
            if (dgvHoaDon.Columns["CustomerName"] != null)
            {
                dgvHoaDon.Columns["CustomerName"].HeaderText = "Tên khách hàng";
            }
            if (dgvHoaDon.Columns["CreatedDate"] != null)
            {
                dgvHoaDon.Columns["CreatedDate"].HeaderText = "Ngày lập";
            }
            if (dgvHoaDon.Columns["TotalAmount"] != null)
            {
                dgvHoaDon.Columns["TotalAmount"].HeaderText = "Tổng tiền hóa đơn";
            }
            if (dgvHoaDon.Columns["statusText"] != null)
            {
                dgvHoaDon.Columns["statusText"].HeaderText = "Trạng thái";
            }
            if (dgvHoaDon.Columns["PaymentMethod"] != null)
            {
                dgvHoaDon.Columns["PaymentMethod"].HeaderText = "Hình thức trả";
            }

            dgvHoaDon.Columns["OrderID"].DisplayIndex = 0;
            dgvHoaDon.Columns["UserID"].DisplayIndex = 1;
            dgvHoaDon.Columns["CustomerName"].DisplayIndex = 2;
            dgvHoaDon.Columns["CustomerPhone"].DisplayIndex = 3;
            dgvHoaDon.Columns["CustomerAddress"].DisplayIndex = 4;
            dgvHoaDon.Columns["CustomerEmail"].DisplayIndex = 5;
            dgvHoaDon.Columns["CreatedDate"].DisplayIndex = 6;
            dgvHoaDon.Columns["TotalAmount"].DisplayIndex = 7;
            dgvHoaDon.Columns["PaymentMethod"].DisplayIndex = 8;
            dgvHoaDon.Columns["statusText"].DisplayIndex = 8;
        }
        private void LoadTieuChiCombobox()
        {
            List<string> tieuChi = new List<string>
            {
                "Các tiêu chí",
                "Mã hóa đơn",
                "Tên khách hàng"
            };
            cboTieuChi.DataSource = tieuChi;

            cboTieuChi.SelectedIndex = 0;
        }

        private void LoadStatusCombobox()
        {
            List<string> status = new List<string>
            {
                "Chọn trạng thái..",
                "Chưa thanh toán",
                "Đã thanh toán"
            };
            cboStatus.DataSource = status;

            cboStatus.SelectedIndex = 0;
        }
    }
}
