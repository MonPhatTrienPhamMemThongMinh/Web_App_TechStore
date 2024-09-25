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
using ThuVien;
using ThuVien.DataAccess;
using ThuVien.Models;

namespace ModuleDN
{
    public partial class ucDangNhap : UserControl
    {
        SQLClass sql = new SQLClass();
        string _cnn;

        public string Cnn
        {
            get => _cnn;
            set
            {
                _cnn = value;
            }
        }

        public event EventHandler<LoginEventArgs> LoginSuccess;
        public event EventHandler<LoginFailedEventArgs> LoginFailed;

        public ucDangNhap()
        {
            InitializeComponent();
            this.btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            sql.createConnection(_cnn);
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
                UserRepository userRepository = new UserRepository(_cnn);
                RoleRepository roleRepository = new RoleRepository(_cnn);

                Users user = userRepository.GetUserByUsername(txtUsername.Texts.Trim());
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

                List<Role> roles = new List<Role>();
                roles = roleRepository.GetRoleByUserId(user.UserId);
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
        public Users LoggedInUser { get; private set; }
        public List<Role> UserRoles { get; private set; }
    
        public LoginEventArgs(Users loggedInUser, List<Role> userRoles)
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
