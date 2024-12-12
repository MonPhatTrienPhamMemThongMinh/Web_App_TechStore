using BLL;
using CustomControl;
using DTO;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_QLWeb_DoDienTu
{
    public partial class frmCategory : Form
    {
        CategoryBLL cbll = new CategoryBLL();
        string categoryImageLogo;
        string categoryImageBackground;
        string categoryImageTitle;
        private frmAdmin parentfrm;
        public frmCategory(frmAdmin parentfrm)
        {
            InitializeComponent();
            this.parentfrm = parentfrm;
            this.Load += UcFormCategory_Load;
            this.btnBack.Click += BtnBack_Click;
            this.dgvCategory.SelectionChanged += DgvCategory_SelectionChanged;
            this.btnThem.Click += BtnAddCategory_Click;
            this.btnSua.Click += BtnEditCategory_Click;
            this.btnXoa.Click += BtnRemoveCategory_Click;
            this.btnHuyBo.Click += BtnCancel_Click;
            this.btnChonAnhBack.Click += BtnCategoryBackground_Click;
            this.btnChonAnhLogo.Click += BtnCategoryLogo_Click;
            this.btnChonAnhTitle.Click += BtnCategoryPic_Click;
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct(parentfrm);
            parentfrm.OpenChildForm(frm);
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            EnableTextBox(false);

            EnableDataGridView(true);
            btnThem.Enabled = true;
            SetButtonStyle(btnThem, true);
            btnSua.Enabled = true;
            SetButtonStyle(btnSua, true);
            btnXoa.Enabled = true;
            SetButtonStyle(btnXoa, true);
            btnThem.Text = "Thêm";
            btnSua.Text = "Sửa";
            btnHuyBo.Enabled = false;
            btnHuyBo.BackColor = Color.DarkGray;
        }
        private void UcFormCategory_Load(object sender, EventArgs e)
        {
            Load_CategoryData();
        }
        private void DgvCategory_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategory.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvCategory.SelectedRows[0];
                if (selectedRow.Cells["maLoaiSanPham"].Value != null)
                {
                    string categoryId = selectedRow.Cells["maLoaiSanPham"].Value.ToString();
                    LoadCategoryDetail(categoryId);
                }
            }
        }
        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            if (btnThem.Text == "Thêm")
            {
                ClearForm();
                btnThem.Text = "Xác nhận";
                btnHuyBo.Enabled = true;
                btnHuyBo.BackColor = Color.IndianRed;
                btnSua.Enabled = false;
                SetButtonStyle(btnSua, false);
                btnXoa.Enabled = false;
                SetButtonStyle(btnXoa, false);
                txtCategoryID.Text = cbll.GenerateCategoryID();
                EnableTextBox(true);
            }
            else if (btnThem.Text == "Xác nhận")
            {
                if (ValidateInput())
                {
                    string categoryId = txtCategoryID.Text;
                    string categoryName = txtCategoryName.Text.Trim();
                    string description = txtCategoryDescription.Text.Trim();

                    Category newCategory = new Category
                    {
                        CategoryID = categoryId,
                        CategoryName = categoryName,
                        CategoryDescription = description,
                        CategoryBackground = categoryImageBackground,
                        CategoryAvatar = categoryImageLogo,
                        CategoryPic = categoryImageTitle
                    };
                    try
                    {
                        bool isSuccess = cbll.AddCategory(newCategory);
                        if (isSuccess)
                        {
                            MessageBox.Show("Thêm mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Load_CategoryData();

                            ClearForm();
                            EnableDataGridView(true);
                            btnSua.Enabled = true;
                            SetButtonStyle(btnSua, true);
                            btnXoa.Enabled = true;
                            SetButtonStyle(btnXoa, true);
                            btnThem.Text = "Thêm";
                            btnHuyBo.Enabled = false;
                            btnHuyBo.BackColor = Color.DarkGray;

                            EnableTextBox(false);
                        }
                        else
                        {
                            MessageBox.Show("Thêm mặt hàng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi thêm mặt hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void BtnEditCategory_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCategoryID.Text))
            {
                if (btnSua.Text == "Sửa")
                {
                    EnableDataGridView(false);
                    btnSua.Text = "Xác nhận";
                    btnHuyBo.Enabled = true;
                    btnHuyBo.BackColor = Color.IndianRed;
                    btnThem.Enabled = false;
                    SetButtonStyle(btnThem, false);
                    btnXoa.Enabled = false;
                    SetButtonStyle(btnXoa, false);
                    EnableTextBox(true);
                }
                else if (btnSua.Text == "Xác nhận")
                {
                    if (ValidateInput())
                    {
                        string categoryId = txtCategoryID.Text;
                        string categoryName = txtCategoryName.Text.Trim();
                        string description = txtCategoryDescription.Text.Trim();
                        string updatedCategoryLogo = string.IsNullOrEmpty(categoryImageLogo) ? pbCategoryLogo.Tag?.ToString() : categoryImageLogo;
                        string updatedCategoryBackground = string.IsNullOrEmpty(categoryImageBackground) ? pbCategoryBackground.Tag?.ToString() : categoryImageBackground;
                        string updatedCategoryTitle = string.IsNullOrEmpty(categoryImageTitle) ? pbCategoryTitle.Tag?.ToString() : categoryImageTitle;

                        Category updatedCategory = new Category
                        {
                            CategoryID = categoryId,
                            CategoryName = categoryName,
                            CategoryDescription = description,
                            CategoryBackground = updatedCategoryBackground,
                            CategoryAvatar = updatedCategoryLogo,
                            CategoryPic = updatedCategoryTitle
                        };
                        try
                        {
                            // Gọi repository để cập nhật thông tin
                            bool isSuccess = cbll.UpdateCategory(updatedCategory);

                            if (isSuccess)
                            {
                                MessageBox.Show("Cập nhật loại sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Load_CategoryData();
                                EnableDataGridView(true);

                                ClearForm();
                                btnThem.Enabled = true;
                                SetButtonStyle(btnThem, true);
                                btnXoa.Enabled = true;
                                SetButtonStyle(btnXoa, true);
                                btnSua.Text = "Sửa";
                                btnHuyBo.Enabled = false;
                                btnHuyBo.BackColor = Color.DarkGray;

                                EnableTextBox(false);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi cập nhật loại sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Chọn loại sản phẩm bạn muốn sửa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void BtnRemoveCategory_Click(object sender, EventArgs e)
        {
            if (txtCategoryID.Text != string.Empty)
            {
                string maLoaiSanPham = txtCategoryID.Text;
                string tenLoaiSanPham = txtCategoryName.Text;
                DialogResult r = MessageBox.Show(this, "Bạn có chắc chắc muốn xóa loại sản phẩm " + tenLoaiSanPham + " này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    Category loaiSP = new Category
                    {
                        CategoryID = maLoaiSanPham,
                        CategoryName = tenLoaiSanPham,

                    };
                    int dem = cbll.DemSoSanPhamThuocLoai(loaiSP.CategoryID);
                    if (dem > 0)
                    {
                        MessageBox.Show(this, $"Không thể xóa loại sản phẩm {loaiSP.CategoryName} do còn sản phẩm thuộc loại này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        bool isSuccess = cbll.DeleteCategory(loaiSP);
                        if (isSuccess)
                        {
                            MessageBox.Show(this, "Xóa loại sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Load_CategoryData();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show(this, "Xóa loại sản phẩm thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }
        private void BtnCategoryBackground_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                ofd.Title = "Chọn Hình Ảnh Mặt Hàng Background";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = ofd.FileName;

                    // Kiểm tra kích thước ảnh (<=5MB)
                    FileInfo fileInfo = new FileInfo(selectedFilePath);
                    const long maxFileSize = 5 * 1024 * 1024; // 5MB
                    if (fileInfo.Length > maxFileSize)
                    {
                        MessageBox.Show("Kích thước hình ảnh không được vượt quá 5MB.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string picFolder = GetWebProjectPicFolderPath();

                    if (!Directory.Exists(picFolder))
                    {
                        Directory.CreateDirectory(picFolder);
                    }

                    string fileName = Path.GetFileName(selectedFilePath);
                    string destinationPath = Path.Combine(picFolder, fileName);

                    if (!selectedFilePath.StartsWith(picFolder, StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            if (!File.Exists(destinationPath))
                            {
                                File.Copy(selectedFilePath, destinationPath);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi sao chép hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        destinationPath = selectedFilePath;
                    }

                    string finalImagePath = destinationPath;

                    try
                    {
                        if (pbCategoryBackground.Image != null)
                        {
                            pbCategoryBackground.Image.Dispose();
                            pbCategoryBackground.Image = null;
                        }

                        using (FileStream fs = new FileStream(finalImagePath, FileMode.Open, FileAccess.Read))
                        {
                            Image img = Image.FromStream(fs);
                            pbCategoryBackground.Image = new Bitmap(img);
                        }

                        categoryImageBackground = Path.Combine("Pic", Path.GetFileName(finalImagePath)).Replace("\\", "/");
                    }
                    catch (OutOfMemoryException)
                    {
                        MessageBox.Show("Định dạng hình ảnh không hợp lệ hoặc hình ảnh quá lớn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        categoryImageBackground = null;
                        return;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Hình ảnh không hợp lệ hoặc bị hỏng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        categoryImageBackground = null;
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi tải hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        categoryImageBackground = null;
                        return;
                    }
                }
            }
        }
        private void BtnCategoryLogo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                ofd.Title = "Chọn Hình Ảnh Mặt Hàng Logo";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = ofd.FileName;

                    // Kiểm tra kích thước ảnh (<=5MB)
                    FileInfo fileInfo = new FileInfo(selectedFilePath);
                    const long maxFileSize = 5 * 1024 * 1024; // 5MB
                    if (fileInfo.Length > maxFileSize)
                    {
                        MessageBox.Show("Kích thước hình ảnh không được vượt quá 5MB.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string picFolder = GetWebProjectPicFolderPath();

                    if (!Directory.Exists(picFolder))
                    {
                        Directory.CreateDirectory(picFolder);
                    }

                    string fileName = Path.GetFileName(selectedFilePath);
                    string destinationPath = Path.Combine(picFolder, fileName);

                    if (!selectedFilePath.StartsWith(picFolder, StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            if (!File.Exists(destinationPath))
                            {
                                File.Copy(selectedFilePath, destinationPath);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi sao chép hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        destinationPath = selectedFilePath;
                    }

                    string finalImagePath = destinationPath;

                    try
                    {
                        if (pbCategoryLogo.Image != null)
                        {
                            pbCategoryLogo.Image.Dispose();
                            pbCategoryLogo.Image = null;
                        }

                        using (FileStream fs = new FileStream(finalImagePath, FileMode.Open, FileAccess.Read))
                        {
                            Image img = Image.FromStream(fs);
                            pbCategoryLogo.Image = new Bitmap(img);
                        }

                        categoryImageLogo = Path.Combine("Pic", Path.GetFileName(finalImagePath)).Replace("\\", "/");
                    }
                    catch (OutOfMemoryException)
                    {
                        MessageBox.Show("Định dạng hình ảnh không hợp lệ hoặc hình ảnh quá lớn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        categoryImageLogo = null;
                        return;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Hình ảnh không hợp lệ hoặc bị hỏng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        categoryImageLogo = null;
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi tải hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        categoryImageLogo = null;
                        return;
                    }
                }
            }
        }
        private void BtnCategoryPic_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                ofd.Title = "Chọn Hình Ảnh Mặt Hàng Logo";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = ofd.FileName;

                    // Kiểm tra kích thước ảnh (<=5MB)
                    FileInfo fileInfo = new FileInfo(selectedFilePath);
                    const long maxFileSize = 5 * 1024 * 1024; // 5MB
                    if (fileInfo.Length > maxFileSize)
                    {
                        MessageBox.Show("Kích thước hình ảnh không được vượt quá 5MB.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string picFolder = GetWebProjectPicFolderPath();

                    if (!Directory.Exists(picFolder))
                    {
                        Directory.CreateDirectory(picFolder);
                    }

                    string fileName = Path.GetFileName(selectedFilePath);
                    string destinationPath = Path.Combine(picFolder, fileName);

                    if (!selectedFilePath.StartsWith(picFolder, StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            if (!File.Exists(destinationPath))
                            {
                                File.Copy(selectedFilePath, destinationPath);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi sao chép hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        destinationPath = selectedFilePath;
                    }

                    string finalImagePath = destinationPath;

                    try
                    {
                        if (pbCategoryTitle.Image != null)
                        {
                            pbCategoryTitle.Image.Dispose();
                            pbCategoryTitle.Image = null;
                        }

                        using (FileStream fs = new FileStream(finalImagePath, FileMode.Open, FileAccess.Read))
                        {
                            Image img = Image.FromStream(fs);
                            pbCategoryTitle.Image = new Bitmap(img);
                        }

                        categoryImageTitle = Path.Combine("Pic", Path.GetFileName(finalImagePath)).Replace("\\", "/");
                    }
                    catch (OutOfMemoryException)
                    {
                        MessageBox.Show("Định dạng hình ảnh không hợp lệ hoặc hình ảnh quá lớn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        categoryImageTitle = null;
                        return;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Hình ảnh không hợp lệ hoặc bị hỏng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        categoryImageTitle = null;
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi tải hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        categoryImageTitle = null;
                        return;
                    }
                }
            }
        }
        private void EnableTextBox(bool enable)
        {
            txtCategoryDescription.Enabled = enable;
            txtCategoryName.Enabled = enable;
            btnChonAnhBack.Enabled = enable;
            btnChonAnhLogo.Enabled = enable;
            btnChonAnhTitle.Enabled = enable;
        }
        private void SetButtonStyle(UIButton button, bool isEnabled)
        {
            if (isEnabled)
            {
                button.RectColor = System.Drawing.Color.FromArgb(84, 160, 255);
                button.RectDisableColor = System.Drawing.Color.FromArgb(84, 160, 255);
                button.RectHoverColor = System.Drawing.Color.FromArgb(46, 134, 222);
                button.RectPressColor = System.Drawing.Color.FromArgb(46, 134, 222);
                button.RectSelectedColor = System.Drawing.Color.FromArgb(46, 134, 222);
                button.FillColor = System.Drawing.Color.FromArgb(84, 160, 255);
                button.FillHoverColor = System.Drawing.Color.FromArgb(46, 134, 222);
                button.FillPressColor = System.Drawing.Color.FromArgb(46, 134, 222);
                button.ForeColor = System.Drawing.Color.White; // Màu chữ khi nút được kích hoạt
            }
            else
            {
                button.RectColor = System.Drawing.Color.DarkGray;
                button.RectDisableColor = System.Drawing.Color.DarkGray;
                button.RectHoverColor = System.Drawing.Color.DarkGray;
                button.RectPressColor = System.Drawing.Color.DarkGray;
                button.RectSelectedColor = System.Drawing.Color.DarkGray;
                button.FillColor = System.Drawing.Color.DarkGray;
                button.FillHoverColor = System.Drawing.Color.DarkGray;
                button.FillPressColor = System.Drawing.Color.DarkGray;
                button.ForeColor = System.Drawing.Color.Gray;
            }
        }
        private void ClearForm()
        {
            txtCategoryID.Text = "";
            txtCategoryName.Text = "";
            txtCategoryDescription.Text = "";
            pbCategoryBackground.Image = null;
            pbCategoryLogo.Image = null;
            pbCategoryTitle.Image = null;
        }
        private void Load_CategoryData()
        {
            try
            {
                List<Category> categories = cbll.GetAllCategories();
                dgvCategory.DataSource = categories;
                dgvCategory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải mặt hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EnableDataGridView(bool enable)
        {
            dgvCategory.Enabled = enable;
            dgvCategory.DefaultCellStyle.BackColor = enable ? Color.White : Color.LightGray;  // Thay đổi màu nền khi vô hiệu hóa
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Vui lòng nhập Brand Name.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(categoryImageLogo))
            {
                MessageBox.Show("Vui lòng nhập hình ảnh logo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(categoryImageBackground))
            {
                MessageBox.Show("Vui lòng nhập hình ảnh background.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(categoryImageTitle))
            {
                MessageBox.Show("Vui lòng nhập hình ảnh title.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void LoadCategoryDetail(string categoryId)
        {
            try
            {
                Category category = cbll.GetCategoryById(categoryId);
                if (category != null)
                {
                    txtCategoryID.Text = category.CategoryID;
                    txtCategoryName.Text = category.CategoryName;
                    txtCategoryDescription.Text = category.CategoryDescription;

                    string picFolder = GetWebProjectPicFolderPath();

                    if (!string.IsNullOrEmpty(category.CategoryAvatar))
                    {
                        string imagePath = Path.Combine(picFolder, Path.GetFileName(category.CategoryAvatar)).Replace("/", "\\");

                        if (File.Exists(imagePath))
                        {
                            try
                            {
                                if (pbCategoryLogo.Image != null)
                                {
                                    pbCategoryLogo.Image.Dispose();
                                    pbCategoryLogo.Image = null;
                                }

                                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                                {
                                    Image img = Image.FromStream(fs);
                                    pbCategoryLogo.Image = new Bitmap(img);
                                    pbCategoryLogo.Tag = category.CategoryAvatar;
                                    categoryImageLogo = category.CategoryAvatar;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi tải hình ảnh logo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hình ảnh logo không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (!string.IsNullOrEmpty(category.CategoryBackground))
                    {
                        string imagePath = Path.Combine(picFolder, Path.GetFileName(category.CategoryBackground)).Replace("/", "\\");

                        if (File.Exists(imagePath))
                        {
                            try
                            {
                                if (pbCategoryBackground.Image != null)
                                {
                                    pbCategoryBackground.Image.Dispose();
                                    pbCategoryBackground.Image = null;
                                }

                                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                                {
                                    Image img = Image.FromStream(fs);
                                    pbCategoryBackground.Image = new Bitmap(img);
                                    pbCategoryBackground.Tag = category.CategoryBackground;

                                    categoryImageBackground = category.CategoryBackground;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi tải hình ảnh background: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hình ảnh background không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (!string.IsNullOrEmpty(category.CategoryPic))
                    {
                        string imagePath = Path.Combine(picFolder, Path.GetFileName(category.CategoryPic)).Replace("/", "\\");

                        if (File.Exists(imagePath))
                        {
                            try
                            {
                                if (pbCategoryTitle.Image != null)
                                {
                                    pbCategoryTitle.Image.Dispose();
                                    pbCategoryTitle.Image = null;
                                }

                                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                                {
                                    Image img = Image.FromStream(fs);
                                    pbCategoryTitle.Image = new Bitmap(img);
                                    pbCategoryTitle.Tag = category.CategoryPic;

                                    categoryImageTitle = category.CategoryPic;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi tải hình ảnh tiêu đề: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hình ảnh tiêu đề không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chi tiết hãng sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetWebProjectPicFolderPath()
        {
            // Đường dẫn tương đối từ thư mục gốc của dự án Windows Forms tới thư mục Pic của dự án web
            string relativePath = @"..\..\..\Pic";

            // Kết hợp với đường dẫn gốc của ứng dụng để tạo đường dẫn tuyệt đối
            string absolutePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath));

            return absolutePath;
        }
    }
}
