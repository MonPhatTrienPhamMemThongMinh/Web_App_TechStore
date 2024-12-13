using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace App_QLWeb_DoDienTu
{
    public partial class frmQLTaiKhoan : Form
    {
        UserBLL nvbll = new UserBLL();
        RoleBLL lnvbll = new RoleBLL();
        private AspNetUser nguoiDung;
        public frmQLTaiKhoan(AspNetUser nguoiDung)
        {
            InitializeComponent();
            this.Load += FrmTaiKhoan_Load;
            SetForm();
            txtSearch.ForeColor = Color.Silver;
            this.txtSDT.KeyPress += txtSDT_KeyPress;
            txtTenDN.Enabled = false;
            this.nguoiDung = nguoiDung;
        }
        public void SetForm()
        {
            txtMaTK.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = false; 
            txtMatKhau.Enabled = false;
        }
        private void EnableControls()
        {
            //không cho phép thêm
            btnThem.Enabled = false;
            //cho phép sửa, làm mới
            btnSua.Enabled = true;
            btnLamMoi.Enabled = true;
            //các text box sẽ mở lại 
            txtDiaChi.Enabled = true;
            txtSDT.Enabled = true;
            txtNgaySinh.Enabled = true;
            txtTenNguoiDung.Enabled = true;
        }
        ///Vô hiệu hóa tất cả 
        private void DisableControls()
        {
            //Text box ẩn
            txtMaTK.Enabled = false;
            txtTenDN.Enabled = false;
            txtMatKhau.Enabled = false;
            txtDiaChi.Enabled = false;
            txtTenNguoiDung.Enabled = false;
            txtSDT.Enabled = false;
            txtNgaySinh.Enabled = false;
        }
        private void ClearForm()
        {
            txtTenNguoiDung.Text = "";
            txtMaTK.Text = "";          
            txtSDT.Text = "";
            txtTenDN.Text = "";
            txtMatKhau.Text = "";
            txtDiaChi.Text = "";
        }        
        private bool ValidateInput_Them()
        {
            // Kiểm tra tên người dùng
            if (string.IsNullOrWhiteSpace(txtTenNguoiDung.Text))
            {
                MessageBox.Show("Vui lòng nhập tên người dùng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra tên đăng nhập và loại bỏ tất cả khoảng trắng

            if (string.IsNullOrWhiteSpace(txtTenDN.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtTenDN.Text.Length < 6)
            {
                MessageBox.Show("Tên đăng nhập phải có độ dài trên 6 kí tự ", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra mật khẩu 

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtMatKhau.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có độ dài từ 6 kí tự.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra số điện thoại
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra độ dài số điện thoại
            if (txtSDT.Text.Length < 10)
            {
                MessageBox.Show("Số điện thoại phải đúng 10 chữ số", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra trùng số điện thoại
            if (nvbll.IsSDTDuplicate(txtSDT.Text))
            {
                MessageBox.Show("Số điện thoại đã tồn tại. Vui lòng nhập số điện thoại khác.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra trùng tên đăng nhập 
            if (nvbll.IsTaiKhoanDuplicate(txtTenDN.Text))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng nhập lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra địa chỉ
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }            
            return true;
        }        
        private void FrmTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadTaiKhoan();
            dgvTaiKhoan.ReadOnly = true;                
        }
        public void LoadTaiKhoan()
        {
            try
            {
                // Tải dữ liệu từ BLL
                List<AspNetUser> nhanViens = nvbll.LoadAllUsers();
                dgvTaiKhoan.DataSource = nhanViens;                
                if (dgvTaiKhoan.Columns["City"] != null)
                {
                    dgvTaiKhoan.Columns["City"].Visible = false;
                }
                if (dgvTaiKhoan.Columns["Email"] != null)
                {
                    dgvTaiKhoan.Columns["Email"].Visible = false;
                }
                if (dgvTaiKhoan.Columns["EmailConfirmed"] != null)
                {
                    dgvTaiKhoan.Columns["EmailConfirmed"].Visible = false;
                }
                if (dgvTaiKhoan.Columns["SecurityStamp"] != null)
                {
                    dgvTaiKhoan.Columns["SecurityStamp"].Visible = false;
                }
                if (dgvTaiKhoan.Columns["PhoneNumberConfirmed"] != null)
                {
                    dgvTaiKhoan.Columns["PhoneNumberConfirmed"].Visible = false;
                }
                if (dgvTaiKhoan.Columns["TwoFactorEnabled"] != null)
                {
                    dgvTaiKhoan.Columns["TwoFactorEnabled"].Visible = false;
                }
                if (dgvTaiKhoan.Columns["LockoutEndDateUtc"] != null)
                {
                    dgvTaiKhoan.Columns["LockoutEndDateUtc"].Visible = false;
                }
                if (dgvTaiKhoan.Columns["LockoutEnabled"] != null)
                {
                    dgvTaiKhoan.Columns["LockoutEnabled"].Visible = false;
                }
                if (dgvTaiKhoan.Columns["AccessFailedCount"] != null)
                {
                    dgvTaiKhoan.Columns["AccessFailedCount"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm tài khoản?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }           
            if (!ValidateInput_Them())
            {
                return;
            }
            var nv = new AspNetUser
            {
                FullName = txtTenNguoiDung.Text.Trim(),
                Birthday = txtNgaySinh.Value.Date,
                UserName = txtTenDN.Text.Trim(),
                PasswordHash = nvbll.MaHoaMatKhauKieuSha256Hash(txtMatKhau.Text.Trim()),                
                PhoneNumber = txtSDT.Text.Trim(),
                Address = txtDiaChi.Text.Trim()
            };
            try
            {
                bool kq = nvbll.InsertNhanVien(nv);
                if (kq)
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadTaiKhoan();
                    btnThem.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            txtTenNguoiDung.Focus();
            //Enable true
            btnThem.Enabled = true;
            txtTenNguoiDung.Enabled = true;
            txtNgaySinh.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSDT.Enabled=true;            
            txtTenDN.Enabled = true;
            txtMatKhau.Enabled = true;
            //Enable false
            //btnXoa.Enabled = false;
            btnSua.Enabled = false;
            //load db 
            LoadTaiKhoan();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này không?",
        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }
            if (dgvTaiKhoan.SelectedRows.Count > 0)
            {
                string manv = dgvTaiKhoan.SelectedRows[0].Cells["maTaiKhoan"].Value.ToString();

                if (nvbll.DeleteNhanVien(manv))
                {
                    MessageBox.Show("Xóa thành công!");
                }
                else
                {
                    MessageBox.Show("Tài khoản này đã thực hiện giao dịch, không thể xóa!");
                }
                LoadTaiKhoan();                
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa tài khoản người dùng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }            
            string newPassword = txtMatKhau.Text.Trim();
            // Nếu mật khẩu không thay đổi (trường mật khẩu để trống hoặc giống với mật khẩu cũ), sử dụng mật khẩu cũ
            string passwordToSave = nvbll.MaHoaMatKhauKieuSha256Hash(newPassword); //Chỉ Mã hóa lại khi mật khẩu thay đổi
            // PhieuDat pPhieuDat = new PhieuDat()
            var nv = new AspNetUser
            {
                Id = txtMaTK.Text.Trim(),
                FullName = txtTenNguoiDung.Text.Trim(),
                Birthday = txtNgaySinh.Value.Date,
                UserName = txtTenDN.Text.Trim(),
                PasswordHash = passwordToSave,
                PhoneNumber = txtSDT.Text.Trim(),
                Address = txtDiaChi.Text.Trim(),
            };

            // Cập nhật cơ sở dữ liệu
            bool kq = nvbll.UpdateNhanVien(nv);
            if (kq)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadTaiKhoan();
            }
            else
            {
                MessageBox.Show("Sửa thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);               
            }
        }
        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //dgvNhanVien.ReadOnly = false;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvTaiKhoan.Rows[e.RowIndex];
                string maTaiKhoan = selectedRow.Cells["maTaiKhoan"].Value.ToString();
                string tenNguoiDung = selectedRow.Cells["tenNguoiDung"].Value.ToString();
                DateTime ngaySinh = DateTime.Parse(selectedRow.Cells["ngaySinh"].Value.ToString());
                string diaChi = selectedRow.Cells["diaChi"].Value.ToString();
                string soDienThoai = selectedRow.Cells["soDienThoai"].Value.ToString();
                string tenDangNhap = selectedRow.Cells["tenDangNhap"].Value.ToString();
                string matKhau = selectedRow.Cells["matKhau"].Value.ToString();                               
                // Cập nhật giá trị vào các TextBox
                txtMaTK.Text = maTaiKhoan;
                txtTenNguoiDung.Text = tenNguoiDung;
                txtNgaySinh.Value = ngaySinh;
                txtDiaChi.Text = diaChi;
                txtSDT.Text = soDienThoai;
                txtTenDN.Text = tenDangNhap;
                txtMatKhau.Text = matKhau;
                EnableControls();
                DisableControls();
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var results = nvbll.SearchNhanVien(txtSearch.Text.Trim());
            if (results != null && results.Count > 0)
            {
                dgvTaiKhoan.DataSource = results;
            }
            else
            {
                MessageBox.Show("Không tìm thấy nhân viên nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);  
            }
        }
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra xem phím nhấn có phải là Enter không
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Tự động click vào nút tìm kiếm
                btnSearch.PerformClick();

                // Ngăn chặn âm thanh bíp khi nhấn Enter
                e.Handled = true;
            }
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            txtMatKhau.Enabled = !txtMatKhau.Enabled;
        }
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập tên, địa chỉ, hoặc số điện thoại để tìm kiếm")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
                txtSearch.Font = new Font(txtSearch.Font, FontStyle.Regular);
            }
        }
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "Nhập tên, địa chỉ, hoặc số điện thoại để tìm kiếm";
                txtSearch.ForeColor = Color.Silver;
                txtSearch.Font = new Font(txtSearch.Font, FontStyle.Italic);
                LoadTaiKhoan();

            }
        }               
        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtSDT.Text.Length >= 10 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }               
        private void txtTenDN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtTenDN.Text.Length >= 30 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }               
        private void txtMatKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }
    }
}
