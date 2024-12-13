using BLL;
using DTO;
using Sunny.UI;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace App_QLWeb_DoDienTu
{
    public partial class frmThongTinTaiKhoanAdmin : Form
    {
        UserBLL nvbll = new UserBLL();
        private AspNetUser user;
        public frmThongTinTaiKhoanAdmin(AspNetUser user)
        {
            InitializeComponent();
            this.Load += frmThongTinTaiKhoanAdmin_Load;
            this.user = user;
            SetEnableFalse();
            btnDoi.Enabled = false;
            btnHuy.Enabled = false;
            this.btnDong.Click += BtnDong_Click;
        }
        public void SetEnableTrue()
        {
            txtMatKhauCu.Enabled = true;
            txtMatKhauMoi.Enabled = true;
            btnDoi.Enabled = true;
            btnHuy.Enabled = true;
            btnChon.Enabled = false;
            txtTaiKhoan.Focus();
        }
        public void SetEnableFalse()
        {
            txtMatKhauCu.Enabled = false;
            txtMatKhauMoi.Enabled = false;
            btnDoi.Enabled = false;
            btnHuy.Enabled = false;
            btnChon.Enabled = true;
        }
        public void loadThongTinNhanVien()
        {

            txtTaiKhoan.Text = user.UserName;            
        }
        private void frmThongTinTaiKhoanAdmin_Load(object sender, System.EventArgs e)
        {
            loadThongTinNhanVien();
        }
        private void BtnDong_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void btnChon_Click_1(object sender, EventArgs e)
        {
            SetEnableTrue();
        }
        private bool ValidateInput_Sua_MK()
        {            
            //MK cũ
            if (string.IsNullOrWhiteSpace(txtMatKhauCu.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu cũ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //MK mới
            if (txtMatKhauMoi.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có độ dài từ 6 kí tự.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }            
            return true;
        } 
        private void txtTaiKhoan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtTaiKhoan.Text.Length >= 30 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void btnDoi_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đổi mật khẩu ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }
            if (!ValidateInput_Sua_MK())
            {
                return;
            }
            string matKhauCuHashed = nvbll.MaHoaMKMoi(txtMatKhauCu.Text);
            string matKhauMoiHashed = nvbll.MaHoaMKMoi(txtMatKhauMoi.Text);
            Debug.WriteLine("Mật khẩu cũ đã băm: " + matKhauCuHashed);
            Debug.WriteLine("Mật khẩu mới đã băm: " + matKhauMoiHashed);
            Debug.WriteLine("------------------------");           
            var nv = new AspNetUser
            {
                Id = user.Id,
                PasswordHash = matKhauMoiHashed,
            };

            // Cập nhật cơ sở dữ liệu
            bool kq = nvbll.UpdateMatKhauMoi(nv);
            if (kq)
            {
                MessageBox.Show($"Cập nhật thành công thành công :\n {nv.FullName}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadThongTinNhanVien();
                SetEnableFalse();
                txtMatKhauCu.Text = "";
                txtMatKhauMoi.Text = "";
            }
            else
            {
                MessageBox.Show($"Lỗi cập nhật: {nv.FullName}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetEnableFalse();
            txtMatKhauCu.Text = "";
            txtMatKhauMoi.Text = "";
        }
        private void btnAnMatKhau_Click(object sender, EventArgs e)
        {
            if (btnAnMatKhau.Symbol == 559636)
            {
                txtMatKhauMoi.PasswordChar = '\0';
                btnAnMatKhau.Symbol = 559637;
            }
            else
            {
                txtMatKhauMoi.PasswordChar = '*';
                btnAnMatKhau.Symbol = 559636;
            }
        }
    }
}
