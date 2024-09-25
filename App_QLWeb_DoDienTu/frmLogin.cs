using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThuVien.Models;
using ModuleDN;

namespace App_QLWeb_DoDienTu
{
    public partial class frmLogin : Form
    {
        string _cnn = Properties.Settings.Default.CNN;
        public frmLogin()
        {
            InitializeComponent();
            InitializeLoginControl();
        }

        private void InitializeLoginControl()
        {
            ucDangNhap1.Cnn = _cnn;

            ucDangNhap1.LoginSuccess += LoginControl_LoginSuccess;
            ucDangNhap1.LoginFailed += LoginControl_LoginFailed;
        }

        private void LoginControl_LoginFailed(object sender, LoginFailedEventArgs e)
        {
            MessageBox.Show(e.ErrorMessage, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LoginControl_LoginSuccess(object sender, LoginEventArgs e)
        {
            List<Role> roles = e.UserRoles;
            if(roles.Exists(role => role.RoleName.Equals("Admin", StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Login successfully");
                frmAdmin frmTrangChu = new frmAdmin();
                frmTrangChu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập quản lý.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
