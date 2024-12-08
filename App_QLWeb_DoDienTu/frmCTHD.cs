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
    public partial class frmCTHD : Form
    {
        OrderBLL odbll = new OrderBLL();
        OrderDetailBLL cthdbll = new OrderDetailBLL();
        private string mahd;
        private Order hoadon;
        public frmCTHD(string mahd)
        {
            InitializeComponent();
            this.Load += FrmCTHD_Load;
            this.mahd = mahd;

            this.dgvCTHD.CellFormatting += DgvCTHD_CellFormatting;
            this.btnThoat.Click += BtnThoat_Click;
        }

        private void DgvCTHD_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvCTHD.Columns[e.ColumnIndex].Name == "Quantity")
            {
                dgvCTHD.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                return;
            }
            if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal tien))
            {
                dgvCTHD.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                e.Value = tien.ToString("N0").Replace(",", ".") + "đ";
                e.FormattingApplied = true;
            }
        }

        private void BtnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCTHD_Load(object sender, EventArgs e)
        {
            hoadon = odbll.LoadHoaDonTheoMa(mahd);
            lblMaHD.Text = mahd;
            lblTenKH.Text = hoadon.CustomerName;
            lblNgayLap.Text = hoadon.CreatedDate.ToString("dd/MM/yyyy");
            lblHinhThucTra.Text = hoadon.PaymentMethod;
            lblDiaChi.Text = hoadon.CustomerAddress;
            lblSdt.Text = hoadon.CustomerPhone;
            lblEmail.Text = hoadon.CustomerEmail;

            dgvCTHD.DataSource = cthdbll.LoadOrderDetail(mahd);
            dgvCTHD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SettingDgv();
            decimal tongTienThanhToan = hoadon.TotalAmount;

            lblTongTienHD.Text = tongTienThanhToan.ToString("N0").Replace(",", ".") + "đ";
        }

        private void SettingDgv()
        {
            if (dgvCTHD.Columns["Product"] != null)
            {
                dgvCTHD.Columns["Product"].Visible = false;
            }
            if (dgvCTHD.Columns["Order"] != null)
            {
                dgvCTHD.Columns["Order"].Visible = false;
            }
            if (dgvCTHD.Columns["OrderId"] != null)
            {
                dgvCTHD.Columns["OrderId"].Visible = false;
            }

            if (dgvCTHD.Columns["ProductName"] != null)
            {
                dgvCTHD.Columns["ProductName"].HeaderText = "Tên sản phẩm";
            }
            if (dgvCTHD.Columns["ProductID"] != null)
            {
                dgvCTHD.Columns["ProductID"].HeaderText = "Mã sản phẩm";
            }
            if (dgvCTHD.Columns["Quantity"] != null)
            {
                dgvCTHD.Columns["Quantity"].HeaderText = "Số lượng";
            }
            if (dgvCTHD.Columns["UnitPrice"] != null)
            {
                dgvCTHD.Columns["UnitPrice"].HeaderText = "Đơn giá";
            }
            if (dgvCTHD.Columns["Price"] != null)
            {
                dgvCTHD.Columns["Price"].HeaderText = "Tổng tiền";
            }

            dgvCTHD.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvCTHD.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvCTHD.Columns["ProductID"].DisplayIndex = 0;
            dgvCTHD.Columns["ProductName"].DisplayIndex = 1;
            dgvCTHD.Columns["Quantity"].DisplayIndex = 2;
            dgvCTHD.Columns["UnitPrice"].DisplayIndex = 3;
            dgvCTHD.Columns["Price"].DisplayIndex = 4;
        }
    }
}
