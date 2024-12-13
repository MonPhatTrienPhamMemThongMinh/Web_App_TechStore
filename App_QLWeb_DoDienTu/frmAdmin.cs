using DTO;
using Sunny.UI;
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
    public partial class frmAdmin : Form
    {
        private AspNetUser user;
        public frmAdmin(AspNetUser user)
        {
            InitializeComponent();
            this.user = user;
            this.btnClose.Click += LblX_Click;
            this.btnSanPham.Click += BtnQLProducts_Click;
            this.btnDonHang.Click += BtnQLOrders_Click;
            this.btnQLNhaCungCap.Click += BtnQLNhaCungCap_Click;
            this.btnUser.Click += BtnUser_Click;
            this.btnDashboard.Click += BtnDashboard_Click;
            this.btnDatHang.Click += BtnDatHang_Click;
            this.btnNhapHang.Click += BtnNhapHang_Click;
            this.btnThongTinNhanVien.Click += BtnThongTinNhanVien_Click;
        }

        private void BtnThongTinNhanVien_Click(object sender, EventArgs e)
        {
            frmThongTinTaiKhoanAdmin frmThongTin = new frmThongTinTaiKhoanAdmin(user);
            frmThongTin.ShowDialog();
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDashboard(user.Id));
        }
        private void BtnNhapHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQLNhapHang());
        }

        private void BtnDatHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQLDatHang());
        }

        private void BtnUser_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQLTaiKhoan(user));
        }

        private void BtnQLNhaCungCap_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmSupplier());
        }

        private void BtnQLOrders_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmOrder());
        }

        private void BtnQLProducts_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmProduct(this));
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Bạn có chắc là muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                frmLogin frmLogin = new frmLogin();
                frmLogin.Show();
                this.Hide();
            }
        }

        private void LblX_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        public void OpenChildForm(Form childForm)
        {
            foreach (Form form in this.MdiChildren)
            {
                form.Close();
            }

            childForm.MdiParent = this;
            childForm.Dock = DockStyle.Fill;
            childForm.Show();
        }
    }
}
