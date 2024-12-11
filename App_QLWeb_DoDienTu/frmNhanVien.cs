using BLL;
using DTO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace App_QLWeb_DoDienTu
{
    public partial class frmNhanVien : Form
    {
        UserBLL nvbll = new UserBLL();
        RoleBLL lnvbll = new RoleBLL();
        private AspNetUser nhanVien;
        public frmNhanVien(AspNetUser nhanVien)
        {
            InitializeComponent();
            this.Load += FrmNhanVien_Load;
            SetForm();
            txtSearch.ForeColor = Color.Silver;
            txtTrangThaiHD.Enabled = false;
            this.txtSDT.KeyPress += txtSDT_KeyPress;
            txtTenDN.Enabled = false;
            this.nhanVien = nhanVien;
        }
        public void SetForm()
        {
            txtMaNV.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnKhoaTK.Enabled = false;
            txtMatKhau.Enabled = false;
        }
        public void SetDataGirdView()
        {
            //không cho phép thêm
            btnThem.Enabled = false;
            //cho phép sửa, xóa 
            btnSua.Enabled = true;
            //btnKhoaTK.Enabled = true;
            //
            txtTenDN.Enabled = false;
            txtMatKhau.Enabled = false;
            //
        }
        private void EnableControls()
        {
            //không cho phép thêm
            btnThem.Enabled = false;
            //cho phép sửa, làm mới
            btnSua.Enabled = true;
            btnLamMoi.Enabled = true;
            //các text box sẽ mở lại 
            txtLuong.Enabled = true;
            txtDiaChi.Enabled = true;
            txtNgayVaoLam.Enabled = true;
            cboLoaiNV.Enabled = true;
            txtSDT.Enabled = true;
            txtNgaySinh.Enabled = true;
            txtTenNV.Enabled = true;

        }
        ///Vô hiệu hóa tất cả 
        private void DisableControls()
        {
            //Text box ẩn
            txtMaNV.Enabled = false;
            txtTenDN.Enabled = false;
            txtMatKhau.Enabled = false;
            txtLuong.Enabled = false;
            txtDiaChi.Enabled = false;
            txtNgayVaoLam.Enabled = false;
            txtTenNV.Enabled = false;
            txtTrangThaiHD.Enabled = false;
            cboLoaiNV.Enabled = false;
            txtSDT.Enabled = false;
            txtNgaySinh.Enabled = false;
            ///Nút ẩn
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnKhoaTK.Enabled = false;
            btnLamMoi.Enabled = false;
        }
        private void ClearForm()
        {
            txtTenNV.Text = "";
            txtMaNV.Text = "";
            txtLuong.Text = "";
            txtSDT.Text = "";
            txtTenDN.Text = "";
            txtMatKhau.Text = "";
            txtDiaChi.Text = "";
            cboLoaiNV.SelectedIndex = 0;
        }
        private void ExportToExcelNhanVien(string filePath)
        {
            IWorkbook wb = new XSSFWorkbook();  // Khởi tạo workbook cho định dạng .xlsx
            ISheet sheet = wb.CreateSheet("NhanVien");  // Tạo sheet có tên "NhanVien"
            IRow rowhead = sheet.CreateRow(0);
            int colIndex = 0;
            for (int i = 0; i < dgvNhanVien.Columns.Count; i++)
            {

                if (dgvNhanVien.Columns[i].HeaderText != "LoaiNhanVien")
                {
                    rowhead.CreateCell(colIndex).SetCellValue(dgvNhanVien.Columns[i].HeaderText);
                    colIndex++;
                }
            }
            for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                colIndex = 0;
                for (int j = 0; j < dgvNhanVien.Columns.Count; j++)
                {

                    if (dgvNhanVien.Columns[j].HeaderText != "LoaiNhanVien")
                    {
                        row.CreateCell(colIndex).SetCellValue(dgvNhanVien.Rows[i].Cells[j].Value?.ToString() ?? "");  // Gán giá trị cho từng ô
                        colIndex++;
                    }
                }
            }
            using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                wb.Write(stream);
            }
        }
        private bool ValidateInput_Them()
        {
            // Kiểm tra tên nhân viên
            if (string.IsNullOrWhiteSpace(txtTenNV.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            // Kiểm tra ngày sinh và độ tuổi
            DateTime selectedDateOfBirth = txtNgaySinh.Value;
            int age = DateTime.Now.Year - selectedDateOfBirth.Year;
            if (selectedDateOfBirth > DateTime.Now.AddYears(-age)) age--;
            if (age < 18)
            {
                MessageBox.Show("Nhân viên phải có tuổi từ 18 trở lên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra ngày vào làm
            DateTime today = DateTime.Today;
            DateTime minHireDate = selectedDateOfBirth.AddYears(18); // Ngày vào làm phải sau sinh nhật 18 tuổi

            if (txtNgayVaoLam.Value <= new DateTime(2003, 1, 19))
            {
                MessageBox.Show("Ngày vào làm phải lớn hơn ngày 19/01/2003 (ngày thành lập công ty).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtNgayVaoLam.Value > today)
            {
                MessageBox.Show("Ngày vào làm không được vượt quá ngày hôm nay.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtNgayVaoLam.Value < minHireDate)
            {
                MessageBox.Show($"Ngày vào làm không được nhỏ hơn ngày {minHireDate.ToString("dd/MM/yyyy")}, khi nhân viên đủ 18 tuổi.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string inputLuong = txtLuong.Text.Trim();

            // Kiểm tra nếu rỗng lương
            if (string.IsNullOrWhiteSpace(inputLuong))
            {
                MessageBox.Show("Lương cơ bản không được để trống. Vui lòng nhập một số nguyên dương.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //// Chuyển đổi sang số nguyên và kiểm tra giá trị
            //if (!int.TryParse(inputLuong, out int luongCoBan) || luongCoBan <= 0)
            //{
            //    MessageBox.Show("Lương cơ bản không hợp lệ. Vui lòng nhập một số nguyên dương.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            return true;
        }
        private bool ValidateInput_Sua(string oldPhoneNumber = "")
        {
            if (string.IsNullOrWhiteSpace(txtTenNV.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra mật khẩu và loại bỏ tất cả khoảng trắng
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

            // Kiểm tra định dạng số điện thoại
            if (txtSDT.Text.Length < 10)
            {
                MessageBox.Show("Số điện thoại phải đúng 10 chữ số.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            // Kiểm tra số điện thoại mới có trùng với số điện thoại của nhân viên khác không
            string newPhoneNumber = txtSDT.Text.Trim();
            if (oldPhoneNumber != newPhoneNumber && nvbll.IsSDTDuplicate(newPhoneNumber)) // Kiểm tra số điện thoại mới
            {
                MessageBox.Show("Số điện thoại này đã tồn tại trong hệ thống. Vui lòng nhập số điện thoại khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            // Kiểm tra địa chỉ
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra ngày sinh và độ tuổi
            DateTime selectedDateOfBirth = txtNgaySinh.Value;
            int age = DateTime.Now.Year - selectedDateOfBirth.Year;
            if (selectedDateOfBirth > DateTime.Now.AddYears(-age)) age--;
            if (age < 18)
            {
                MessageBox.Show("Nhân viên phải có tuổi từ 18 trở lên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra ngày vào làm
            DateTime today = DateTime.Today;
            DateTime minHireDate = selectedDateOfBirth.AddYears(18); // Ngày vào làm phải sau sinh nhật 18 tuổi

            if (txtNgayVaoLam.Value <= new DateTime(2003, 1, 19))
            {
                MessageBox.Show("Ngày vào làm phải lớn hơn ngày 19/01/2003 (ngày thành lập công ty).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtNgayVaoLam.Value > today)
            {
                MessageBox.Show("Ngày vào làm không được vượt quá ngày hôm nay.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtNgayVaoLam.Value < minHireDate)
            {
                MessageBox.Show($"Ngày vào làm không được nhỏ hơn ngày {minHireDate.ToString("dd/MM/yyyy")}, khi nhân viên đủ 18 tuổi.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra nếu rỗng lương
            if (string.IsNullOrWhiteSpace(txtLuong.Text))
            {
                MessageBox.Show("Lương cơ bản không được để trống. Vui lòng nhập một số nguyên dương.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        
        }
        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            dgvNhanVien.ReadOnly = true;
            cboLoaiNV.DataSource = lnvbll.GetAllRole();
            cboLoaiNV.DisplayMember = "Name";
            cboLoaiNV.ValueMember = "Id";            
        }
        public void LoadNhanVien()
        {
            try
            {
                // Tải dữ liệu từ BLL
                List<AspNetUser> nhanViens = nvbll.LoadAllUsers();
                dgvNhanVien.DataSource = nhanViens;
                if (dgvNhanVien.Columns["AspNetRoles"] != null)
                {
                    dgvNhanVien.Columns["AspNetRoles"].Visible = false;
                }
                if (dgvNhanVien.Columns["maLoaiNhanVien"] != null)
                {
                    dgvNhanVien.Columns["maLoaiNhanVien"].Visible = false;
                }
                if (dgvNhanVien.Columns["City"] != null)
                {
                    dgvNhanVien.Columns["City"].Visible = false;
                }
                if (dgvNhanVien.Columns["Email"] != null)
                {
                    dgvNhanVien.Columns["Email"].Visible = false;
                }
                if (dgvNhanVien.Columns["EmailConfirmed"] != null)
                {
                    dgvNhanVien.Columns["EmailConfirmed"].Visible = false;
                }
                if (dgvNhanVien.Columns["SecurityStamp"] != null)
                {
                    dgvNhanVien.Columns["SecurityStamp"].Visible = false;
                }
                if (dgvNhanVien.Columns["PhoneNumberConfirmed"] != null)
                {
                    dgvNhanVien.Columns["PhoneNumberConfirmed"].Visible = false;
                }
                if (dgvNhanVien.Columns["TwoFactorEnabled"] != null)
                {
                    dgvNhanVien.Columns["TwoFactorEnabled"].Visible = false;
                }
                if (dgvNhanVien.Columns["LockoutEndDateUtc"] != null)
                {
                    dgvNhanVien.Columns["LockoutEndDateUtc"].Visible = false;
                }
                if (dgvNhanVien.Columns["LockoutEnabled"] != null)
                {
                    dgvNhanVien.Columns["LockoutEnabled"].Visible = false;
                }
                if (dgvNhanVien.Columns["AccessFailedCount"] != null)
                {
                    dgvNhanVien.Columns["AccessFailedCount"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm nhân viên?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }
            // Xử lý giá trị lương
            int luongchuyendoi = int.Parse(txtLuong.Text.Replace(".", "").Trim());
            string maLoaiNV = cboLoaiNV.SelectedValue.ToString();
            if (!ValidateInput_Them())
            {
                return;
            }
            var nv = new AspNetUser
            {
                FullName = txtTenNV.Text.Trim(),
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
                    LoadNhanVien();
                    btnThem.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                // Thông báo lỗi khi không thể thêm nhân viên
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            txtTenNV.Focus();
            //Enable true
            btnThem.Enabled = true;
            txtTenDN.Enabled = true;
            txtMatKhau.Enabled = true;
            //Enable false
            //btnXoa.Enabled = false;
            btnSua.Enabled = false;
            //load db 
            LoadNhanVien();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?",
        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                string manv = dgvNhanVien.SelectedRows[0].Cells["maNhanVien"].Value.ToString();

                if (nvbll.DeleteNhanVien(manv))
                {
                    MessageBox.Show("Xóa thành công!");
                }
                else
                {
                    MessageBox.Show("Nhân viên đã thực hiện giao dịch, không thể xóa!");
                }
                LoadNhanVien();
                //SetForm();
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                Id = txtMaNV.Text.Trim(),
                FullName = txtTenNV.Text.Trim(),
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
                LoadNhanVien();
            }
            else
            {
                MessageBox.Show("Sửa thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show($"Cập nhật không thành công cho nhân viên: {nv.FullName}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //dgvNhanVien.ReadOnly = false;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvNhanVien.Rows[e.RowIndex];
                string maNhanVien = selectedRow.Cells["maNhanVien"].Value.ToString();
                string tenNhanVien = selectedRow.Cells["tenNhanVien"].Value.ToString();
                DateTime ngaySinh = DateTime.Parse(selectedRow.Cells["ngaySinh"].Value.ToString());
                string diaChi = selectedRow.Cells["diaChi"].Value.ToString();
                string soDienThoai = selectedRow.Cells["soDienThoai"].Value.ToString();
                string tenDangNhap = selectedRow.Cells["tenDangNhap"].Value.ToString();
                string matKhau = selectedRow.Cells["matKhau"].Value.ToString();
                int luongCoBan = int.Parse(selectedRow.Cells["luongCoBan"].Value.ToString());
                DateTime ngayVaoLam = DateTime.Parse(selectedRow.Cells["ngayVaoLam"].Value.ToString());
                string maLoaiNhanVien = selectedRow.Cells["maLoaiNhanVien"].Value.ToString();
                string trangThai = selectedRow.Cells["trangThai"].Value.ToString();
                // Kiểm tra trạng thái để vô hiệu hóa/kích hoạt các điều khiển
                if (trangThai == "Ngưng hoạt động")
                {
                    DisableControls();
                    btnMoTK.Enabled = true;
                    btnOpen.Enabled = false;
                }
                else
                {
                    EnableControls();
                    btnMoTK.Enabled = false;
                    btnKhoaTK.Enabled = true;
                    btnOpen.Enabled = true;
                    txtTenDN.Enabled = false;
                    txtMatKhau.Enabled = false;
                }
                // Cập nhật giá trị vào các TextBox
                txtMaNV.Text = maNhanVien;
                txtTenNV.Text = tenNhanVien;
                txtNgaySinh.Value = ngaySinh;
                txtDiaChi.Text = diaChi;
                txtSDT.Text = soDienThoai;
                txtTenDN.Text = tenDangNhap;
                txtMatKhau.Text = matKhau;
                txtLuong.Text = luongCoBan.ToString("N0").Replace(",", ".");
                txtNgayVaoLam.Value = ngayVaoLam;
                txtTrangThaiHD.Text = trangThai;
                cboLoaiNV.SelectedValue = maLoaiNhanVien;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var results = nvbll.SearchNhanVien(txtSearch.Text.Trim());
            if (results != null && results.Count > 0)
            {
                dgvNhanVien.DataSource = results;
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
                LoadNhanVien();

            }
        }
        private void dgvNhanVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra nếu cột là "trangThai"
            if (dgvNhanVien.Columns[e.ColumnIndex].Name == "trangThai")
            {

                if (e.Value != null && e.Value.ToString() == "Ngưng hoạt động")
                {

                    e.CellStyle.ForeColor = Color.Red;
                }

                else if (e.Value != null && e.Value.ToString() == "Đang hoạt động")
                {

                    e.CellStyle.ForeColor = Color.Green;
                }
            }

            // Kiểm tra nếu cột là cột "luongCoBan"
            if (dgvNhanVien.Columns[e.ColumnIndex].Name == "luongCoBan")
            {
                // Kiểm tra nếu giá trị của ô là số
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int value))
                {
                    // Định dạng số với dấu chấm và thêm "VND"
                    e.Value = value.ToString("N0").Replace(",", ".") + " VND";
                    e.FormattingApplied = true; // Đảm bảo định dạng đã được áp dụng
                }
            }
        }
        private void btnMoTK_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn mở khóa tài khoản này ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //if (result == DialogResult.No)
            //{
            //    return;
            //}
            //if (dgvNhanVien.SelectedRows.Count > 0)
            //{
            //    string manv = dgvNhanVien.SelectedRows[0].Cells["maNhanVien"].Value.ToString();
            //    string trangThaiCu = dgvNhanVien.SelectedRows[0].Cells["trangThai"].Value.ToString(); // Lấy trạng thái cũ

            //    Debug.WriteLine($"Attempting to deactivate employee with ID: {manv}");


            //    if (trangThaiCu == "Ngưng hoạt động")
            //    {
            //        // Cập nhật trạng thái và màu sắc
            //        if (nvbll.UpdateTrangThai(manv, "Đang hoạt động"))
            //        {
            //            MessageBox.Show("Mở tài khoản thành công!");

            //            // Cập nhật màu chữ trong DataGridView
            //            foreach (DataGridViewRow row in dgvNhanVien.Rows)
            //            {
            //                if (row.Cells["maNhanVien"].Value.ToString() == manv)
            //                {
            //                    // Cập nhật trạng thái trong DataGridView
            //                    row.Cells["trangThai"].Value = "Ngưng hoạt động";
            //                    row.Cells["trangThai"].Style.ForeColor = Color.Black;
            //                }
            //            }

            //        }
            //        else
            //        {
            //            MessageBox.Show("Khóa tài khoản thất bại!");
            //            Debug.WriteLine($"Failed to deactivate employee with ID: {manv}");
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Tài khoản đã bị khóa hoặc không có trạng thái 'Đang hoạt động'.");
            //    }

            //    // Cập nhật lại danh sách nhân viên
            //    LoadNhanVien();
            //    SetForm();
            //}
        }
        private void txtTimNVOFF_Click(object sender, EventArgs e)
        {
            ////Ngung hoat dong
            //if (rdNgungHD.Checked)
            //{
            //    var results = nvbll.GetNhanVienBiKhoa();

            //    if (results != null && results.Count > 0)
            //    {
            //        dgvNhanVien.DataSource = results;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Không tìm thấy nhân viên nào có tài khoản bị khóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            ////Hoat dong
            //else if (rdDangHD.Checked)
            //{
            //    var results = nvbll.GetNhanVienHoatDong();

            //    if (results != null && results.Count > 0)
            //    {
            //        dgvNhanVien.DataSource = results;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Không tìm thấy nhân viên nào đang hoạt động!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //else
            //{
            //    // Nếu không có radio được chọn
            //    MessageBox.Show("Vui lòng chọn trạng thái nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}            ////Ngung hoat dong
            //if (rdNgungHD.Checked)
            //{
            //    var results = nvbll.GetNhanVienBiKhoa();

            //    if (results != null && results.Count > 0)
            //    {
            //        dgvNhanVien.DataSource = results;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Không tìm thấy nhân viên nào có tài khoản bị khóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            ////Hoat dong
            //else if (rdDangHD.Checked)
            //{
            //    var results = nvbll.GetNhanVienHoatDong();

            //    if (results != null && results.Count > 0)
            //    {
            //        dgvNhanVien.DataSource = results;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Không tìm thấy nhân viên nào đang hoạt động!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //else
            //{
            //    // Nếu không có radio được chọn
            //    MessageBox.Show("Vui lòng chọn trạng thái nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }
        private void btnKhoaTK_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn khóa tài khoản này ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //if (result == DialogResult.No)
            //{
            //    return;
            //}

            //if (dgvNhanVien.SelectedRows.Count > 0)
            //{
            //    string manv = dgvNhanVien.SelectedRows[0].Cells["maNhanVien"].Value.ToString();
            //    string trangThaiCu = dgvNhanVien.SelectedRows[0].Cells["trangThai"].Value.ToString(); // Lấy trạng thái cũ

            //    Debug.WriteLine($"Attempting to deactivate employee with ID: {manv}");

            //    // Nếu trạng thái là "Đang hoạt động", tiến hành cập nhật thành "Ngưng hoạt động"
            //    if (trangThaiCu == "Đang hoạt động")
            //    {
            //        // Cập nhật trạng thái và màu sắc
            //        if (nvbll.UpdateTrangThai(manv, "Ngưng hoạt động")) // Giả sử có phương thức UpdateTrangThai để cập nhật trạng thái
            //        {
            //            MessageBox.Show("Khóa tài khoản thành công!");

            //            // Cập nhật màu chữ trong DataGridView
            //            foreach (DataGridViewRow row in dgvNhanVien.Rows)
            //            {
            //                if (row.Cells["maNhanVien"].Value.ToString() == manv)
            //                {
            //                    // Cập nhật trạng thái trong DataGridView
            //                    row.Cells["trangThai"].Value = "Ngưng hoạt động";
            //                    row.Cells["trangThai"].Style.ForeColor = Color.Red;
            //                }
            //            }
            //            LoadNhanVien();
            //            dgvNhanVien.Refresh();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Khóa tài khoản thất bại!");
            //            Debug.WriteLine($"Failed to deactivate employee with ID: {manv}");
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Tài khoản đã bị khóa hoặc không có trạng thái 'Đang hoạt động'.");
            //    }

            //    // Cập nhật lại danh sách nhân viên
            //    LoadNhanVien();
            //    SetForm();
            //}
        }
        private void btnHuyLoc_Click(object sender, EventArgs e)
        {
            ClearForm();
            rdDangHD.Checked = false;
            rdNgungHD.Checked = false;
            LoadNhanVien();
        }
        private void btnXuatFileExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xlsx)|*.xlsx";
            //sfd.FileName = "NhanVienFile";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string filePath = sfd.FileName;

                try
                {
                    ExportToExcelNhanVien(sfd.FileName);
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xuất file: {ex.Message}", "Lỗi", MessageBoxButtons.OK);
                }
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
        private void txtLuong_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (txtLuong.Text.Length >= 12 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }
        private string FormatLuong(string input)
        {
            // Loại bỏ tất cả dấu chấm hiện có
            input = input.Replace(".", "");

            // Nếu không phải là số thì trả về input ban đầu
            if (!long.TryParse(input, out long luong))
                return input;

            // Chia thành các nhóm ba chữ số và thêm dấu chấm vào giữa
            string formatted = "";
            int count = 0;

            // Duyệt ngược từ cuối chuỗi và chèn dấu chấm sau mỗi 3 chữ số
            for (int i = input.Length - 1; i >= 0; i--)
            {
                formatted = input[i] + formatted;
                count++;

                // Thêm dấu chấm sau mỗi ba chữ số nếu không phải là ký tự đầu tiên
                if (count % 3 == 0 && i > 0)
                {
                    formatted = "." + formatted;
                }
            }

            return formatted;
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
        private void txtLuong_TextChanged(object sender, EventArgs e)
        {
            // Lưu vị trí con trỏ hiện tại
            int cursorPosition = txtLuong.SelectionStart;

            // Loại bỏ các dấu chấm và số 0 ở đầu chuỗi nếu có
            string input = txtLuong.Text.Replace(".", "");
            if (input.StartsWith("0") && input.Length > 1)
            {
                input = input.TrimStart('0');
            }

            // Định dạng lại chuỗi với dấu chấm
            string formatted = FormatLuong(input);

            // Cập nhật lại giá trị vào textbox
            txtLuong.Text = formatted;

            // Đặt lại vị trí con trỏ
            txtLuong.SelectionStart = cursorPosition > txtLuong.Text.Length ? txtLuong.Text.Length : cursorPosition;
        }
        private void btnLoaiNV_Click(object sender, EventArgs e)
        {
            frmLoaiNV frmLoaiNV = new frmLoaiNV();
            this.Enabled = false;
            frmLoaiNV.ShowDialog();
            this.Enabled = true;
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
