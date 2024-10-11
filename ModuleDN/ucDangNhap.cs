using Microsoft.AspNet.Identity;
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

namespace ModuleDN
{
    public partial class ucDangNhap : UserControl
    {
        private UserBLL ubll;
        private RoleBLL rbll;
        private UserRoleBLL urbll;

        public event EventHandler<LoginEventArgs> LoginSuccess;
        public event EventHandler<LoginFailedEventArgs> LoginFailed;

        public ucDangNhap()
        {
            InitializeComponent();

            ubll = new UserBLL();
            rbll = new RoleBLL();
            urbll = new UserRoleBLL();

            this.btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Texts.Trim()))
            {
                MessageBox.Show("Không được bỏ trống Username");
                this.txtUsername.Focus();
                return;
            }
            if(string.IsNullOrEmpty(txtPassword.Texts))
            {
                MessageBox.Show("Không được bỏ trống Password");
                this.txtPassword.Focus();
                return;
            }

            try
            {
                AspNetUser user = ubll.GetUserByUsername(txtUsername.Texts.Trim());
                if(user == null)
                {
                    LoginFailed?.Invoke(this, new LoginFailedEventArgs("Tên đăng nhập không tồn tại."));
                    return;
                }
                var passwordHasher = new PasswordHasher();
                var verificationResult = passwordHasher.VerifyHashedPassword(user.PasswordHash, txtPassword.Texts);
                if (verificationResult != PasswordVerificationResult.Success)
                {
                    // Mật khẩu ko đúng
                    LoginFailed?.Invoke(this, new LoginFailedEventArgs("Mật khẩu không đúng."));
                    return;
                }

                List<AspNetRole> roles = rbll.GetRolesByUserId(user.Id);
                if (roles == null || roles.Count == 0)
                {
                    LoginFailed?.Invoke(this, new LoginFailedEventArgs("Người dùng không có vai trò nào."));
                    return; 
                }

                LoginSuccess?.Invoke(this, new LoginEventArgs(user, roles));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    // Lớp chứa thông tin kết quả đăng nhập
    public class LoginEventArgs : EventArgs
    {
        public AspNetUser LoggedInUser { get; private set; }
        public List<AspNetRole> UserRoles { get; private set; }
    
        public LoginEventArgs(AspNetUser loggedInUser, List<AspNetRole> userRoles)
        {
            LoggedInUser = loggedInUser;
            UserRoles = userRoles;
        }
    }

    // Báo fail nếu sai thông tin đăng nhập
    public class LoginFailedEventArgs : EventArgs
    {
        public string ErrorMessage { get; private set; }
    
        public LoginFailedEventArgs(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
    }
}
