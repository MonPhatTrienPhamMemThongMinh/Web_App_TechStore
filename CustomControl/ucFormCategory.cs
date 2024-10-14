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
using BLL;
using DTO;

namespace CustomControl
{
    public partial class ucFormCategory : UserControl
    {
        private CategoryBLL cbll = new CategoryBLL();
        string categoryImageLogo;
        string categoryImageBackground;
        string categoryImageTitle;
        public event EventHandler CategoryDeleted;

        public ucFormCategory()
        {
            InitializeComponent();
            this.Load += UcFormCategory_Load;
            this.dgvCategory.SelectionChanged += DgvCategory_SelectionChanged;
            this.btnAddCategory.Click += BtnAddCategory_Click;
            this.btnEditCategory.Click += BtnEditCategory_Click;
            this.btnRemoveCategory.Click += BtnRemoveCategory_Click;
            this.btnCancel.Click += BtnCancel_Click;
            this.btnCategoryBackground.Click += BtnCategoryBackground_Click;
            this.btnCategoryLogo.Click += BtnCategoryLogo_Click;
            this.btnCategoryTitle.Click += BtnCategoryPic_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (btnAddCategory.Text == "Accept")
            {
                DialogResult result = MessageBox.Show("Bạn muốn hủy thêm mới mặt hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ClearForm();
                    btnAddCategory.Text = "Add New";
                    btnEditCategory.Enabled = true;
                    btnEditCategory.BackColor = Color.FromArgb(32, 80, 189);
                    btnRemoveCategory.Enabled = true;
                    btnRemoveCategory.BackColor = Color.FromArgb(32, 80, 189);
                    btnCancel.Enabled = false;
                    btnCancel.BackColor = Color.DarkGray;

                    txtCategoryID.Enabled = false;
                    txtCategoryName.Enabled = false;
                    txtCategoryDescription.Enabled = false;
                    btnCategoryBackground.Enabled = false;
                    btnCategoryLogo.Enabled = false;
                    btnCategoryTitle.Enabled = false;

                    btnCategoryBackground.BackColor = Color.DarkGray;
                    btnCategoryLogo.BackColor = Color.DarkGray;
                    btnCategoryTitle.BackColor = Color.DarkGray;

                    // Kích hoạt lại DataGridView khi bấm Cancel
                    EnableDataGridView(true);
                }
            }

            if (btnEditCategory.Text == "Accept")
            {
                DialogResult result = MessageBox.Show("Bạn muốn hủy chỉnh sửa hả?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    LoadCategoryDetail(txtCategoryID.Text);

                    btnAddCategory.Enabled = true;
                    btnAddCategory.BackColor = Color.FromArgb(32, 80, 189);
                    btnRemoveCategory.Enabled = true;
                    btnRemoveCategory.BackColor = Color.FromArgb(32, 80, 189);
                    btnEditCategory.Text = "Edit Brand";
                    btnCancel.Enabled = false;
                    btnCancel.BackColor = Color.DarkGray;

                    txtCategoryName.Enabled = false;
                    txtCategoryDescription.Enabled = false;
                    btnCategoryBackground.Enabled = false;
                    btnCategoryLogo.Enabled = false;
                    btnCategoryTitle.Enabled = false;

                    btnCategoryBackground.BackColor = Color.DarkGray;
                    btnCategoryLogo.BackColor = Color.DarkGray;
                    btnCategoryTitle.BackColor = Color.DarkGray;
                    EnableDataGridView(true);
                }
            }
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
                if (selectedRow.Cells["CategoryID"].Value != null)
                {
                    string categoryId = selectedRow.Cells["CategoryID"].Value.ToString();
                    LoadCategoryDetail(categoryId);
                }
            }
        }

        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            if (btnAddCategory.Text == "Add New")
            {
                ClearForm();
                EnableDataGridView(false);
                btnAddCategory.Text = "Accept";
                btnCancel.Enabled = true;
                btnCancel.BackColor = Color.IndianRed;
                btnEditCategory.Enabled = false;
                btnEditCategory.BackColor = Color.DarkGray;
                btnRemoveCategory.Enabled = false;
                btnRemoveCategory.BackColor = Color.DarkGray;

                txtCategoryID.Enabled = true;
                /*txtBrandID.Text = brandRepository.GenerateBrandID();*/

                txtCategoryName.Enabled = true;
                txtCategoryDescription.Enabled = true;

                btnCategoryBackground.Enabled = true;
                btnCategoryLogo.Enabled = true;
                btnCategoryTitle.Enabled = true;

                btnCategoryLogo.BackColor = Color.FromArgb(32, 80, 189);
                btnCategoryBackground.BackColor = Color.FromArgb(32, 80, 189);
                btnCategoryTitle.BackColor = Color.FromArgb(32, 80, 189);
            }
            else if (btnAddCategory.Text == "Accept")
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
                            btnEditCategory.Enabled = true;
                            btnEditCategory.BackColor = Color.FromArgb(32, 80, 189);
                            btnRemoveCategory.Enabled = true;
                            btnRemoveCategory.BackColor = Color.FromArgb(32, 80, 189);
                            btnAddCategory.Text = "Add New";
                            btnCancel.Enabled = false;
                            btnCancel.BackColor = Color.DarkGray;

                            txtCategoryID.Enabled = false;
                            txtCategoryName.Enabled = false;
                            txtCategoryDescription.Enabled = false;
                            btnCategoryBackground.Enabled = false;
                            btnCategoryLogo.Enabled = false;
                            btnCategoryTitle.Enabled = false;

                            btnCategoryTitle.BackColor = Color.DarkGray;
                            btnCategoryLogo.BackColor = Color.DarkGray;
                            btnCategoryBackground.BackColor = Color.DarkGray;
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
                if (btnEditCategory.Text == "Edit Category")
                {
                    EnableDataGridView(false);
                    btnEditCategory.Text = "Accept";
                    btnCancel.Enabled = true;
                    btnCancel.BackColor = Color.IndianRed;
                    btnAddCategory.Enabled = false;
                    btnAddCategory.BackColor = Color.DarkGray;
                    btnRemoveCategory.Enabled = false;
                    btnRemoveCategory.BackColor = Color.DarkGray;

                    txtCategoryName.Enabled = true;
                    txtCategoryDescription.Enabled = true;

                    btnCategoryBackground.Enabled = true;
                    btnCategoryLogo.Enabled = true;
                    btnCategoryTitle.Enabled = true;

                    btnCategoryLogo.BackColor = Color.FromArgb(32, 80, 189);
                    btnCategoryBackground.BackColor = Color.FromArgb(32, 80, 189);
                    btnCategoryTitle.BackColor = Color.FromArgb(32, 80, 189);
                }
                else if (btnEditCategory.Text == "Accept")
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
                                MessageBox.Show("Cập nhật mặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnAddCategory.Enabled = true;
                                btnAddCategory.BackColor = Color.FromArgb(32, 80, 189);
                                btnRemoveCategory.Enabled = true;
                                btnRemoveCategory.BackColor = Color.FromArgb(32, 80, 189);
                                btnEditCategory.Text = "Edit Category";
                                btnCancel.Enabled = false;
                                btnCancel.BackColor = Color.DarkGray;

                                txtCategoryID.Enabled = false;
                                txtCategoryName.Enabled = false;
                                txtCategoryDescription.Enabled = false;
                                btnCategoryBackground.Enabled = false;
                                btnCategoryLogo.Enabled = false;
                                btnCategoryTitle.Enabled = false;

                                btnCategoryTitle.BackColor = Color.DarkGray;
                                btnCategoryLogo.BackColor = Color.DarkGray;
                                btnCategoryBackground.BackColor = Color.DarkGray;

                                Load_CategoryData();
                                EnableDataGridView(true);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi cập nhật mặt hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Chọn mặt hàng bạn muốn sửa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRemoveCategory_Click(object sender, EventArgs e)
        {
            // Ktra xem có ít nhất 1 sản phẩm dudoc chọn ko
            if (dgvCategory.SelectedRows.Count > 0)
            {
                List<string> selectedCategoryIds = new List<string>();

                foreach (DataGridViewRow row in dgvCategory.SelectedRows)
                {
                    selectedCategoryIds.Add(row.Cells["CategoryID"].Value.ToString());
                }

                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa {selectedCategoryIds.Count} mặt hàng được chọn?",
                                              "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    bool allDeleted = true;
                    foreach (string brandId in selectedCategoryIds)
                    {
                        Category category = cbll.GetCategoryById(brandId);
                        if (category != null)
                        {
                            bool isDeleted = cbll.DeleteCategory(category);
                            if (!isDeleted)
                            {
                                allDeleted = false;
                            }
                        }
                    }
                    if (allDeleted)
                    {
                        MessageBox.Show("Xóa tất cả hãng sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Load_CategoryData();
                        CategoryDeleted?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show("Một số hãng sản phẩm không thể xóa được.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hãng sản phẩm để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
