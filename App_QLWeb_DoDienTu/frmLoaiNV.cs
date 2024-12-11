using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_QLWeb_DoDienTu
{
    public partial class frmLoaiNV : Form
    {
        RoleBLL lnvbll = new RoleBLL();
        UserBLL nvBLL = new UserBLL();
        public frmLoaiNV()
        {
            InitializeComponent();
            SetForm();
            this.Load += frmLoaiNV_Load;
            dgvLoaiNhanVien.Columns["maLoaiNhanVien"].ReadOnly = true;
            dgvLoaiNhanVien.Columns["tenLoaiNhanVien"].ReadOnly = false;
        }
        public void SetForm()
        {
            txtMaLoaiNV.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        public void SetDataGirdView()
        {
            //không cho phép thêm
            btnThem.Enabled = false;
            //cho phép sửa, xóa 
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void ClearForm()
        {
            txtTenLoaiNV.Text = "";
            txtMaLoaiNV.Text = "";
        }
        private bool ValidateInput()
        {
            //if (string.IsNullOrWhiteSpace(txtMaNCC.Text))
            //{
            //    MessageBox.Show("Vui lòng nhập mã nhà cung cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            if (string.IsNullOrWhiteSpace(txtTenLoaiNV.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại nhân viên .", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public void LoadLoaiNhanVien()
        {
            try
            {
                // Tải dữ liệu từ BLL
                List<AspNetRole> loainhanViens = lnvbll.GetAllRole();
                dgvLoaiNhanVien.DataSource = loainhanViens;

                // Thiết lập chế độ tự động điều chỉnh kích thước cột
                dgvLoaiNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải lọai nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }              
        private void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }
            if (!ValidateInput())
            {
                return;
            }

            string maLNV = txtMaLoaiNV.Text;
            string OldTen = lnvbll.GetRolesByUserId(maLNV).Name;
            string NewTen = txtTenLoaiNV.Text.Trim();
            if (OldTen != NewTen && lnvbll.IsTenLoaiNhanVienExit(NewTen))
            {
                MessageBox.Show("Tên loại đã tồn tại trong hệ thống. Vui lòng nhập tên loại khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var lnv = new AspNetRole
            {
                Id = txtMaLoaiNV.Text.Trim(),
                Name = txtTenLoaiNV.Text.Trim()
            };
            try
            {
                bool kq = lnvbll.UpdateLoaiNhanVien(lnv);
                if (kq)
                {

                    MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadLoaiNhanVien();
                    SetForm();
                }
            }
            catch
            {
                MessageBox.Show("Tên loại nhân viên đã tồn tại. Vui lòng nhập tên khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Lấy kết quả tìm kiếm từ BLL
            var results = lnvbll.SearchLoaiNhanVien(txtSearch.Text.Trim());

            // Kiểm tra kết quả trả về có hợp lệ không
            if (results != null && results.Count > 0)
            {
                // Cập nhật DataGridView với kết quả tìm kiếm
                dgvLoaiNhanVien.DataSource = results;
            }
            else
            {
                // Nếu không tìm thấy kết quả, thông báo cho người dùng
                MessageBox.Show("Không tìm thấy loại nhân viên nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Xóa hết dữ liệu trong DataGridView nếu không có kết quả tìm kiếm
                LoadLoaiNhanVien();
            }
            SetForm();
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
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập tên loại nhân viên ")
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
                txtSearch.Text = "Nhập tên loại nhân viên ";
                txtSearch.ForeColor = Color.Silver;
                txtSearch.Font = new Font(txtSearch.Font, FontStyle.Italic);
                LoadLoaiNhanVien();
            }
        }
        private void uiSymbolButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgvLoaiNhanVien_SelectionChanged(object sender, EventArgs e)
        {

            //if (dgvLoaiNhanVien.SelectedRows.Count > 0)
            //{
            //    // Lấy hàng đang được chọn
            //    DataGridViewRow selectedRow = dgvLoaiNhanVien.SelectedRows[0];
            //    // Lấy giá trị từ các ô trong hàng đã chọn
            //    string maLoaiNhanVien = selectedRow.Cells["maLoaiNhanVien"].Value.ToString();
            //    string tenLoaiNhanVien = selectedRow.Cells["tenLoaiNhanVien"].Value.ToString();
            //    // Cập nhật giá trị vào các TextBox
            //    txtMaLoaiNV.Text = maLoaiNhanVien;
            //    txtTenLoaiNV.Text = tenLoaiNhanVien;
            //    SetDataGirdView();
            //}

        }
        private void dgvLoaiNhanVien_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có phải đang chỉnh sửa cột "tenLoaiNhanVien" không
            if (e.ColumnIndex == dgvLoaiNhanVien.Columns["tenLoaiNhanVien"].Index)
            {
               
                // Lấy giá trị mới sau khi chỉnh sửa và mã loại nhân viên của hàng hiện tại
                string newTenLoaiNhanVien = dgvLoaiNhanVien.Rows[e.RowIndex].Cells["tenLoaiNhanVien"].Value?.ToString().Trim();
                string maLoaiNhanVien = dgvLoaiNhanVien.Rows[e.RowIndex].Cells["maLoaiNhanVien"].Value?.ToString();

                // so sánh tên loại nếu trùng loại cũ 
                string oldTenLoaiNhanVien = lnvbll.GetRolesByUserId(maLoaiNhanVien).Name;

                // thoát nếu không thay đổi tên
                if (newTenLoaiNhanVien == oldTenLoaiNhanVien)
                {
                    return;
                }

                // Kiểm tra nếu tên loại nhân viên bị để trống
                if (string.IsNullOrWhiteSpace(newTenLoaiNhanVien))
                {
                    MessageBox.Show("Tên loại nhân viên không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvLoaiNhanVien.Rows[e.RowIndex].Cells["tenLoaiNhanVien"].Value = oldTenLoaiNhanVien;
                    // Đặt lại giá trị về tên cũ (hoặc hủy chỉnh sửa)
                    LoadLoaiNhanVien();  // Tải lại dữ liệu từ database để hủy thay đổi
                    return;
                }

                // Hiển thị thông báo yêu cầu xác nhận sửa đổi
                var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn sửa tên loại nhân viên?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Nếu người dùng chọn Yes, tiếp tục cập nhật
                if (confirmResult == DialogResult.Yes)
                {
                    // Tạo đối tượng LoaiNhanVien với dữ liệu mới
                    var lnv = new AspNetRole
                    {
                        Id = maLoaiNhanVien,
                        Name = newTenLoaiNhanVien
                    };
                    try
                    {
                        // Gọi phương thức cập nhật từ BLL
                        bool kq = lnvbll.UpdateLoaiNhanVien(lnv);

                        if (kq)
                        {
                            MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvLoaiNhanVien.Rows[e.RowIndex].Cells["tenLoaiNhanVien"].Value = newTenLoaiNhanVien;
                            LoadLoaiNhanVien();  // Tải lại dữ liệu sau khi sửa
                        }
                        else
                        {
                            MessageBox.Show("Tên loại nhân viên đã tồn tại. Vui lòng nhập tên khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dgvLoaiNhanVien.Rows[e.RowIndex].Cells["tenLoaiNhanVien"].Value = oldTenLoaiNhanVien;
                            LoadLoaiNhanVien();  // Tải lại dữ liệu để hủy thay đổi nếu trùng lặp
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi sửa loại nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }
                }
                else
                {
                    // Nếu người dùng chọn No, hủy hành động và không thay đổi dữ liệu
                    dgvLoaiNhanVien.Rows[e.RowIndex].Cells["tenLoaiNhanVien"].Value = oldTenLoaiNhanVien;
                }
            }

        }

        private void dgvLoaiNhanVien_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Kiểm tra xem có phải đang chỉnh sửa cột "tenLoaiNhanVien" không
            if (e.ColumnIndex == dgvLoaiNhanVien.Columns["tenLoaiNhanVien"].Index)
            {
                //  các nút "Thêm", "Sửa"
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaLoaiNV.Text!=null)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
                else
                {
                    string malnv = txtMaLoaiNV.Text;
                    if (nvBLL.DemSoNhanVienThuocLoai(malnv)==0)
                    {
                        if (lnvbll.DeleteLoaiNhanVien(malnv))
                        {
                            MessageBox.Show("Xóa thành công!");
                            LoadLoaiNhanVien();
                            SetForm();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại!");
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Không thể xóa loại nhân viên do có nhân viên thuộc loại này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }  
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            txtTenLoaiNV.Focus();
            //
            btnThem.Enabled = true;

            //
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            //load db 
            LoadLoaiNhanVien();
        }
        private void frmLoaiNV_Load(object sender, EventArgs e)
        {
            LoadLoaiNhanVien();
        }

        private void dgvLoaiNhanVien_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow selectedRow = dgvLoaiNhanVien.Rows[e.RowIndex];
                // Lấy giá trị từ các ô trong hàng đã chọn (note: đặt tên name cho cột)
                string maLoaiNhanVien = selectedRow.Cells["maLoaiNhanVien"].Value.ToString();
                string tenLoaiNhanVien = selectedRow.Cells["tenLoaiNhanVien"].Value.ToString();

                // Cập nhật giá trị vào các TextBox
                txtMaLoaiNV.Text = maLoaiNhanVien;
                txtTenLoaiNV.Text = tenLoaiNhanVien;

                // Kiểm tra xem cột hiện tại có phải là "tenLoaiNhanVien" 
                if (e.ColumnIndex != dgvLoaiNhanVien.Columns["tenLoaiNhanVien"].Index)
                {
                    SetDataGirdView();
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }
            if (!ValidateInput())
            {
                return;
            }

            var lnv = new AspNetRole
            {
                Name = txtTenLoaiNV.Text.Trim()
            };

            //Debug.WriteLine($"Attempting to insert LoaiNhanVien with name: {lnv.tenLoaiNhanVien}");

            try
            {
                bool kq = lnvbll.InsertLoaiNhanVien(lnv);

                if (kq)
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadLoaiNhanVien();
                    SetForm();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.WriteLine("Failed to add LoaiNhanVien");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm loại nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Debug.WriteLine($"Error while adding LoaiNhanVien: {ex.Message}");
            }

            btnThem.Enabled = false;
        }
        private void btnPhanQuyen_Click(object sender, EventArgs e)
        {
            if (txtMaLoaiNV.Text!=string.Empty)
            {
                string maLoai = txtMaLoaiNV.Text;
                //frmQLPhanQuyen frmQLPhanQuyen = new frmQLPhanQuyen(maLoai);
                //frmQLPhanQuyen.ShowDialog();
            }
        }
    }
}
