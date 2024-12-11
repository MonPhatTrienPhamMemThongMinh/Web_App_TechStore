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
namespace App_QLWeb_DoDienTu
{
    public partial class frmQLDatHang : Form
    {
        private AspNetUser nhanVien;
        private PhieuDatBLL phieuDatBLL;
        private ChiTietPhieuDatBLL chiTietPhieuDatBLL;
        //private ChiTietQuyenCuaLoaiNVBLL ctQuyen;
        private BindingSource bindingSource;
        private BindingSource bindingSourceCTPD;
        public frmQLDatHang(AspNetUser nhanVien)
        {
            this.nhanVien = nhanVien;
            this.phieuDatBLL = new PhieuDatBLL();
            this.chiTietPhieuDatBLL = new ChiTietPhieuDatBLL();
            //this.ctQuyen = new ChiTietQuyenCuaLoaiNVBLL();
            this.bindingSource = new BindingSource();
            this.bindingSourceCTPD = new BindingSource();
            InitializeComponent();
        }        
        private void btnTaoPhieuDat_Click(object sender, EventArgs e)
        {
            frmDatHang frmDatHang = new frmDatHang(nhanVien.Id, true,string.Empty,string.Empty);
            frmDatHang.DongForm += FormDatHang_Closed;
            frmDatHang.ShowDialog();
        }
        private void FormDatHang_Closed(bool loadData)
        {
            if (loadData)
            {
                LoadData();
            }            
        }
        private void btnXoaPhieuDat_Click(object sender, EventArgs e)
        {
            if (dtgvDanhSachPhieuDat.SelectedRows.Count>0)
            {
                string trangThai = dtgvDanhSachPhieuDat.SelectedRows[0].Cells["trangThai"].Value.ToString();
                string trangThaiXacNhan;
                if (dtgvDanhSachPhieuDat.SelectedRows[0].Cells["trangThaiXacNhan"].Value != null)
                {
                    trangThaiXacNhan = dtgvDanhSachPhieuDat.SelectedRows[0].Cells["trangThaiXacNhan"].Value.ToString();
                }
                else
                {
                    trangThaiXacNhan = null;
                }
                if (trangThai != "Đã duyệt" && trangThaiXacNhan !="Đã chấp thuận")
                {
                    string maPhieuDat = dtgvDanhSachPhieuDat.SelectedRows[0].Cells["maPhieuDat"].Value.ToString();
                    DialogResult r = MessageBox.Show(this, "Bạn có chắc chắn muốn xóa phiếu đặt này không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        bool result = phieuDatBLL.XoaPhieuDat(maPhieuDat);
                        if (result)
                        {
                            MessageBox.Show(this, "Xóa phiếu đặt thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show(this, "Xóa phiếu đặt thất bại", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                    }   
                }
                else
                {
                    MessageBox.Show(this, "Không thể xóa phiếu đặt", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }            
        }
        private void btnSuaPhieuDat_Click(object sender, EventArgs e)
        {
            if (dtgvDanhSachPhieuDat.SelectedRows.Count>0)
            {
                string trangThai = dtgvDanhSachPhieuDat.SelectedRows[0].Cells["trangThai"].Value.ToString();
                string trangThaiXacNhan;
                if (dtgvDanhSachPhieuDat.SelectedRows[0].Cells["trangThaiXacNhan"].Value != null)
                {
                    trangThaiXacNhan = dtgvDanhSachPhieuDat.SelectedRows[0].Cells["trangThaiXacNhan"].Value.ToString();
                }
                else
                {
                    trangThaiXacNhan = null;
                }                 
                if (trangThai!="Đã duyệt" && trangThaiXacNhan != "Đã chấp thuận")
                {
                    string maPhieuDat = dtgvDanhSachPhieuDat.SelectedRows[0].Cells["maPhieuDat"].Value.ToString();
                    string maNhaCungCap = dtgvDanhSachPhieuDat.SelectedRows[0].Cells["maNhaCungCap"].Value.ToString();
                    frmDatHang frmDatHang = new frmDatHang(nhanVien.Id, false, maPhieuDat,maNhaCungCap);
                    frmDatHang.DongForm += FormDatHang_Closed;
                    frmDatHang.ShowDialog();
                }
                else
                {
                    MessageBox.Show(this, "Không thể sửa phiếu đặt", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
        }
        private void LoadData()
        {
            List<PhieuDat> phieuDats = phieuDatBLL.LayDanhSachPhieuDat();
            bindingSource.DataSource = phieuDats;
        }
        private void frmQLDatHang_Load(object sender, EventArgs e)
        {
            LoadData();
            dtgvDanhSachPhieuDat.DataSource = bindingSource;
            dtgvDanhSachPhieuDat.AutoGenerateColumns = false;
            dtgvDanhSachPhieuDat.Columns["MaNhaCungCap"].Visible = false;
            dtgvDanhSachPhieuDat.Columns["NhaCungCap"].Visible = false;
            dtgvDanhSachPhieuDat.Columns["UserID"].Visible = false;
            dtgvDanhSachPhieuDat.Columns["AspNetUser"].Visible = false;
            dtgvDanhSachPhieuDat.Columns["GhiChu"].Visible = false;
            dtgvDanhSachPhieuDat.Columns["ghiChuKhongDuyet"].DisplayIndex = dtgvDanhSachPhieuDat.Columns.Count - 1;

            //QuyenDuyetPhieu = (ctQuyen.TimQuyenCuaNhanVien(nhanVien.maLoaiNhanVien, "Q0012") != null) ? true : false;
            //if (!QuyenDuyetPhieu)
            //{
            //    btnDuyetPhieuDat.Visible = false;
            //    btnKoDuyet.Visible = false;
            //    btnKhongXacNhan.Visible = false;
            //    btnXacNhan.Visible = false;
            //}
            //QuyenTaoPhieu = (ctQuyen.TimQuyenCuaNhanVien(nhanVien.maLoaiNhanVien, "Q0011") != null) ? true : false;
            //if (!QuyenTaoPhieu)
            //{
            //    btnTaoPhieuDat.Visible = false;
            //    btnSuaPhieuDat.Visible = false;
            //    btnXoaPhieuDat.Visible = false;
            //    btnInPhieuDat.Visible = false;
            //}
        }
        private void dtNgayTaoPhieuNhap_ValueChanged(object sender, EventArgs e)
        {
            bindingSource.DataSource = phieuDatBLL.LocDanhSachPhieuDatTheoNgayLap(dtNgayTaoPhieuNhap.Value);
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maPhieuDat = txtTimKiem.Text.Trim();
            bindingSource.DataSource = phieuDatBLL.TimKiemPhieuDatTheoMaPhieuDat(maPhieuDat);
        }
        private void dtgvDanhSachPhieuDat_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tabControlPhieuDat.SelectedTab = tabChiTiet;
            string maPhieuDat = dtgvDanhSachPhieuDat.Rows[e.RowIndex].Cells["maPhieuDat"].Value.ToString();
            bindingSourceCTPD.DataSource = chiTietPhieuDatBLL.LayChiTietPhieuDat(maPhieuDat);
            dtgvChiTietPhieuDat.DataSource = bindingSourceCTPD;
            dtgvChiTietPhieuDat.Columns["PhieuDat"].Visible = false;
            dtgvChiTietPhieuDat.Columns["SanPham"].Visible = false;              
        }
        private void tabControlPhieuDat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlPhieuDat.SelectedTab == tabDanhSach)
            {
                bindingSourceCTPD.Clear();
            }
        }
        private void btnInPhieuDat_Click(object sender, EventArgs e)
        {
            if (dtgvDanhSachPhieuDat.SelectedRows.Count>0)
            {
                string maPhieuDat = dtgvDanhSachPhieuDat.SelectedRows[0].Cells["maPhieuDat"].Value.ToString();
                frmPhieuDatHang frmPhieuDat = new frmPhieuDatHang(maPhieuDat);
                frmPhieuDat.ShowDialog();
            }
        }        
        private void dtgvDanhSachPhieuDat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0)
            {
                string maPhieuDat = dtgvDanhSachPhieuDat.Rows[e.RowIndex].Cells["maPhieuDat"].Value.ToString();
                if (e.ColumnIndex == dtgvDanhSachPhieuDat.Columns["ghiChuKhongDuyet"].Index)
                {
                    frmGhiChu frmGhiChu = new frmGhiChu(maPhieuDat);
                    frmGhiChu.ShowDialog();
                }
                string trangThai = dtgvDanhSachPhieuDat.Rows[e.RowIndex].Cells["trangThai"].Value.ToString();
                if (trangThai != "Đã duyệt")
                {
                    btnDuyetPhieuDat.Enabled = true;
                    btnKoDuyet.Enabled = true;
                    btnXacNhan.Enabled = false;
                    btnKhongXacNhan.Enabled = false;
                }
                else if (trangThai == "Đã duyệt")
                {
                    btnDuyetPhieuDat.Enabled = false;
                    btnKoDuyet.Enabled = false;
                }
                string trangThaiXacNhan;
                if (dtgvDanhSachPhieuDat.Rows[e.RowIndex].Cells["trangThaiXacNhan"].Value != null)
                {
                    trangThaiXacNhan = dtgvDanhSachPhieuDat.Rows[e.RowIndex].Cells["trangThaiXacNhan"].Value.ToString();
                }
                else
                {
                    trangThaiXacNhan = null;
                }
                if (trangThaiXacNhan == "Không chấp thuận")
                {
                    btnXacNhan.Enabled = false;
                    btnKhongXacNhan.Enabled = false;
                }
                else if (trangThaiXacNhan == null && trangThai == "Đã duyệt")
                {
                    btnXacNhan.Enabled = true;
                    btnKhongXacNhan.Enabled = true;
                }
                else if (trangThaiXacNhan == null && trangThai != "Đã duyệt")
                {
                    btnXacNhan.Enabled = false;
                    btnKhongXacNhan.Enabled = false;
                }
                else if (trangThaiXacNhan == "Đã chấp thuận")
                {
                    btnXacNhan.Enabled = false;
                    btnKhongXacNhan.Enabled = false;
                    btnKoDuyet.Enabled = false;
                }
            }
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (dtgvDanhSachPhieuDat.SelectedRows.Count > 0)
            {
                string maPhieuDat = dtgvDanhSachPhieuDat.SelectedRows[0].Cells["maPhieuDat"].Value.ToString();
                bool result = phieuDatBLL.XacNhanPhieuDat(maPhieuDat,"Đã chấp thuận");
                if (result)
                {
                    MessageBox.Show(this, "Xác nhận phiếu đặt thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show(this, "Xác nhận phiếu đặt thất bại", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
        }
        private void btnKhongXacNhan_Click(object sender, EventArgs e)
        {
            if (dtgvDanhSachPhieuDat.SelectedRows.Count > 0)
            {
                
                string maPhieuDat = dtgvDanhSachPhieuDat.SelectedRows[0].Cells["maPhieuDat"].Value.ToString();
                bool resultKoDuyet = phieuDatBLL.DuyetPhieuDat(maPhieuDat,"Chưa duyệt");
                bool result = phieuDatBLL.XacNhanPhieuDat(maPhieuDat, "Không chấp thuận");                
                if (result)
                {
                    MessageBox.Show(this, "Xác nhận phiếu đặt thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show(this, "Xác nhận phiếu đặt thất bại", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
        }
        private void btnDuyetPhieuDat_Click(object sender, EventArgs e)
        {
            if (dtgvDanhSachPhieuDat.SelectedRows.Count > 0)
            {
                string maPhieuDat = dtgvDanhSachPhieuDat.SelectedRows[0].Cells["maPhieuDat"].Value.ToString();
                bool result = phieuDatBLL.DuyetPhieuDat(maPhieuDat,"Đã duyệt");
                if (result)
                {
                    MessageBox.Show(this, "Duyệt phiếu đặt thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show(this, "Duyệt phiếu đặt thất bại", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
        }
        private void btnKoDuyet_Click(object sender, EventArgs e)
        {
            if (dtgvDanhSachPhieuDat.SelectedRows.Count > 0)
            {
                string maPhieuDat = dtgvDanhSachPhieuDat.SelectedRows[0].Cells["maPhieuDat"].Value.ToString();
                bool result = phieuDatBLL.DuyetPhieuDat(maPhieuDat,"Không duyệt");
                if (result)
                {
                    MessageBox.Show(this, "Không duyệt phiếu đặt thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    frmGhiChu frmGhiChu = new frmGhiChu(maPhieuDat);
                    frmGhiChu.ShowDialog();
                    LoadData();
                }
                else
                {
                    MessageBox.Show(this, "Không duyệt phiếu đặt thất bại", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
        }
        private void dtgvDanhSachPhieuDat_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgvDanhSachPhieuDat.Columns[e.ColumnIndex].Name == "ghiChuKhongDuyet" && e.Value != null)
            {
                e.CellStyle.BackColor = Color.Green;   // Màu nền nút
                e.CellStyle.ForeColor = Color.White;  // Màu chữ nút

                e.CellStyle.SelectionBackColor = Color.Green;
                e.CellStyle.SelectionForeColor = Color.White;
            }
        }
        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                txtTimKiem.Text = "Nhập mã phiếu đặt để tìm";
                txtTimKiem.ForeColor = Color.Silver;
                txtTimKiem.Font = new Font(txtTimKiem.Font, FontStyle.Italic);
                LoadData();
            }
        }
        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập mã phiếu đặt để tìm")
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
