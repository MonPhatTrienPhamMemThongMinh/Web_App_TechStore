using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
namespace App_QLWeb_DoDienTu
{
    public partial class frmQLNhapHang : Form
    {
        private SupplierBLL nhaCungCapBLL;
        private PhieuDatBLL phieuDatBLL;
        private PhieuNhapBLL phieuNhapBLL;
        private ChiTietPhieuNhapBLL chiTietPhieuNhapBLL;
        private BindingSource bindingSource;
        private BindingSource bindingSourceCTPN; 
        public frmQLNhapHang()
        {
            this.nhaCungCapBLL = new SupplierBLL();
            this.phieuDatBLL = new PhieuDatBLL();
            this.phieuNhapBLL = new PhieuNhapBLL();
            this.chiTietPhieuNhapBLL = new ChiTietPhieuNhapBLL();
            this.bindingSource = new BindingSource();
            this.bindingSourceCTPN = new BindingSource();
            InitializeComponent();
        }
        private void LoadData()
        {
            List<PhieuNhap> danhSachPhieuNhap = phieuNhapBLL.LayDanhSachPhieuNhap();
            bindingSource.DataSource = danhSachPhieuNhap;
        }
        private void frmNhapHang_Load(object sender, EventArgs e)
        {
            LoadData();
            dtgvPhieuNhap.DataSource = bindingSource;
            dtgvPhieuNhap.Columns["PhieuDat"].Visible = false;
        }   
        private void btnTaoPhieuNhap_Click(object sender, EventArgs e)
        {                          
            frmNhapHang frmNhapHang = new frmNhapHang();
            frmNhapHang.DongForm += FrmNhapHang_DongForm;
            frmNhapHang.ShowDialog();         
        }
        private void FrmNhapHang_DongForm(bool loadData)
        {
            if (loadData)
            {
                LoadData();
            }
        }
        private void btnInPhieuNhap_Click(object sender, EventArgs e)
        {
            if (dtgvPhieuNhap.SelectedRows.Count > 0)
            {
                string maPhieuNhap = dtgvPhieuNhap.SelectedRows[0].Cells["maPhieuNhap"].Value.ToString();
                frmPhieuNhapHang frmPhieuNhap = new frmPhieuNhapHang(maPhieuNhap);
                frmPhieuNhap.ShowDialog();
            }
        }
        private void dtgvPhieuNhap_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tabControlPhieuNhap.SelectedTab = tbChiTiet;
            string maPhieuNhap = dtgvPhieuNhap.Rows[e.RowIndex].Cells["maPhieuNhap"].Value.ToString();
            bindingSourceCTPN.DataSource = chiTietPhieuNhapBLL.LayDanhSachChiTietPhieuNhap(maPhieuNhap);
            dtgvChiTietPhieuNhap.DataSource = bindingSourceCTPN;
            dtgvChiTietPhieuNhap.Columns["maPD"].DisplayIndex = 1;
            dtgvChiTietPhieuNhap.Columns["PhieuNhap"].Visible = false;
            dtgvChiTietPhieuNhap.Columns["ChiTietPhieuDat"].Visible = false;
        }
        private void tabControlPhieuNhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlPhieuNhap.SelectedTab == tabPhieuNhap)
            {
                bindingSourceCTPN.Clear();
            }            
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string text = txtTimKiem.Text;
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Substring(0, 2);
                if (text=="PN")
                {
                    string maPhieuNhap = txtTimKiem.Text.Trim();
                    bindingSource.DataSource = phieuNhapBLL.TimKiemPhieuNhapTheoMaPhieuNhap(maPhieuNhap);
                }
                else if (text=="PD")
                {
                    string maPhieuDat = txtTimKiem.Text.Trim();
                    bindingSource.DataSource = phieuNhapBLL.TimKiemPhieuNhapTheoMaPhieuDat(maPhieuDat);
                }
                else
                {
                    MessageBox.Show(this, "Không tìm thấy phiếu nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            }
            else
            {
                MessageBox.Show(this, "Vui lòng nhập từ khóa để tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void dtNgayTaoPhieuNhap_ValueChanged(object sender, EventArgs e)
        {
            bindingSource.DataSource = phieuNhapBLL.LocDanhSachPhieuNhapTheoNgayLap(dtNgayTaoPhieuNhap.Value);
        }
        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                txtTimKiem.Text = "Nhập mã phiếu đặt hoặc phiếu nhập để tìm";
                txtTimKiem.ForeColor = Color.Silver;
                txtTimKiem.Font = new Font(txtTimKiem.Font, FontStyle.Italic);
                LoadData();
            }
        }
        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập mã phiếu đặt hoặc phiếu nhập để tìm")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
                txtTimKiem.Font = new Font(txtTimKiem.Font, FontStyle.Regular);
            }
        }
        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra xem phím nhấn có phải là Enter không
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Tự động click vào nút tìm kiếm
                btnTimKiem.PerformClick();

                // Ngăn chặn âm thanh bíp khi nhấn Enter
                e.Handled = true;
            }
        }
    }
}
