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
    public partial class ucFormBrand : UserControl
    {
        private BrandBLL bbll = new BrandBLL();
        string brandImageLogo;
        string brandImageBackground;
        public event EventHandler BrandDeleted;

        public ucFormBrand()
        {
            InitializeComponent();
            this.Load += UcFormBrand_Load;
            this.dgvBrand.SelectionChanged += DgvBrand_SelectionChanged;
            this.btnAddBrand.Click += BtnAddBrand_Click;
            this.btnEditBrand.Click += BtnEditBrand_Click;
            this.btnRemoveBrand.Click += BtnRemoveBrand_Click;
            this.btnCancel.Click += BtnCancel_Click;
            this.btnBrandBackground.Click += BtnBrandBackground_Click;
            this.btnBrandLogo.Click += BtnBrandLogo_Click;
        }

        private void DgvBrand_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBrand.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvBrand.SelectedRows[0];
                if (selectedRow.Cells["BrandID"].Value != null)
                {
                    string brandId = selectedRow.Cells["BrandID"].Value.ToString();
                    LoadBrandDetail(brandId);
                }
            }
        }

        private void BtnBrandLogo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                ofd.Title = "Chọn Hình Ảnh Logo";

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

                    // Đường dẫn thư mục Pic của dự án web
                    string picFolder = GetWebProjectPicFolderPath();

                    if (!Directory.Exists(picFolder)) // Kiểm tra và tạo thư mục PicBrand nếu chưa tồn tại
                    {
                        Directory.CreateDirectory(picFolder);
                    }

                    string fileName = Path.GetFileName(selectedFilePath); // Lấy tên file từ đường dẫn
                    string destinationPath = Path.Combine(picFolder, fileName); // Đường dẫn đích tới thư mục PicBrand của web

                    // Sao chép hình ảnh vào thư mục PicBrand nếu không nằm trong thư mục PicBrand
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
                        // Nếu hình ảnh đã nằm trong thư mục Pic, sử dụng đường dẫn hiện tại
                        destinationPath = selectedFilePath;
                    }

                    string finalImagePath = destinationPath;

                    // Dùng Image.FromStream để tránh khóa file
                    try
                    {
                        if (pbBrandLogo.Image != null)
                        {
                            pbBrandLogo.Image.Dispose();
                            pbBrandLogo.Image = null;
                        }

                        // Tải hình ảnh từ Pic folder
                        using (FileStream fs = new FileStream(finalImagePath, FileMode.Open, FileAccess.Read))
                        {
                            Image img = Image.FromStream(fs);
                            pbBrandLogo.Image = new Bitmap(img);
                        }

                        // Set productImagePath sau khi tải thành công
                        brandImageLogo = Path.Combine("PicBrand", Path.GetFileName(finalImagePath)).Replace("\\", "/");
                    }
                    catch (OutOfMemoryException)
                    {
                        MessageBox.Show("Định dạng hình ảnh không hợp lệ hoặc hình ảnh quá lớn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        brandImageLogo = null;
                        return;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Hình ảnh không hợp lệ hoặc bị hỏng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        brandImageLogo = null;
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi tải hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        brandImageLogo = null;
                        return;
                    }
                }
            }
        }

        private void BtnBrandBackground_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                ofd.Title = "Chọn Hình Ảnh Sản Phẩm";

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

                    // Đường dẫn thư mục Pic của dự án web
                    string picFolder = GetWebProjectPicFolderPath();

                    if (!Directory.Exists(picFolder)) // Kiểm tra và tạo thư mục Pic nếu chưa tồn tại
                    {
                        Directory.CreateDirectory(picFolder);
                    }

                    string fileName = Path.GetFileName(selectedFilePath); // Lấy tên file từ đường dẫn
                    string destinationPath = Path.Combine(picFolder, fileName); // Đường dẫn đích tới thư mục PicBrand của web

                    // Sao chép hình ảnh vào thư mục Pic nếu không nằm trong thư mục Pic
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
                        // Nếu hình ảnh đã nằm trong thư mục Pic, sử dụng đường dẫn hiện tại
                        destinationPath = selectedFilePath;
                    }

                    string finalImagePath = destinationPath;

                    // Dùng Image.FromStream để tránh khóa file
                    try
                    {
                        if (pbBrandBackground.Image != null)
                        {
                            pbBrandBackground.Image.Dispose();
                            pbBrandBackground.Image = null;
                        }

                        // Tải hình ảnh từ PicBrand folder
                        using (FileStream fs = new FileStream(finalImagePath, FileMode.Open, FileAccess.Read))
                        {
                            Image img = Image.FromStream(fs);
                            pbBrandBackground.Image = new Bitmap(img);
                        }

                        brandImageBackground = Path.Combine("PicBrand", Path.GetFileName(finalImagePath)).Replace("\\", "/");
                    }
                    catch (OutOfMemoryException)
                    {
                        MessageBox.Show("Định dạng hình ảnh không hợp lệ hoặc hình ảnh quá lớn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        brandImageBackground = null;
                        return;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Hình ảnh không hợp lệ hoặc bị hỏng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        brandImageBackground = null;
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi tải hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        brandImageBackground = null;
                        return;
                    }
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if(btnAddBrand.Text == "Accept")
            {
                DialogResult result = MessageBox.Show("Bạn muốn hủy thêm mới hãng sản phẩm?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ClearForm();
                    btnAddBrand.Text = "Add New";
                    btnEditBrand.Enabled = true;
                    btnEditBrand.BackColor = Color.FromArgb(32, 80, 189);
                    btnRemoveBrand.Enabled = true;
                    btnRemoveBrand.BackColor = Color.FromArgb(32, 80, 189);
                    btnCancel.Enabled = false;
                    btnCancel.BackColor = Color.DarkGray;

                    txtBrandID.Enabled = false;
                    txtBrandName.Enabled = false;
                    txtBrandDescription.Enabled = false;
                    btnBrandBackground.Enabled = false;
                    btnBrandLogo.Enabled = false;
                    btnBrandBackground.BackColor = Color.DarkGray;
                    btnBrandLogo.BackColor = Color.DarkGray;

                    // Kích hoạt lại DataGridView khi bấm Cancel
                    EnableDataGridView(true);
                }
            }
            
            if(btnEditBrand.Text == "Accept")
            {
                DialogResult result = MessageBox.Show("Bạn muốn hủy chỉnh sửa hả?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    LoadBrandDetail(txtBrandID.Text);

                    btnAddBrand.Enabled = true;
                    btnAddBrand.BackColor = Color.FromArgb(32, 80, 189);
                    btnRemoveBrand.Enabled = true;
                    btnRemoveBrand.BackColor = Color.FromArgb(32, 80, 189);
                    btnEditBrand.Text = "Edit Brand";
                    btnCancel.Enabled = false;
                    btnCancel.BackColor = Color.DarkGray;

                    txtBrandName.Enabled = false;
                    txtBrandDescription.Enabled = false;
                    btnBrandBackground.Enabled = false;
                    btnBrandLogo.Enabled = false;
                    btnBrandBackground.BackColor = Color.DarkGray;
                    btnBrandLogo.BackColor = Color.DarkGray;

                    EnableDataGridView(true);
                }
            }
        }

        private void BtnRemoveBrand_Click(object sender, EventArgs e)
        {
            if (dgvBrand.SelectedRows.Count > 0)
            {
                List<string> selectedBrandIds = new List<string>();

                foreach (DataGridViewRow row in dgvBrand.SelectedRows)
                {
                    selectedBrandIds.Add(row.Cells["BrandID"].Value.ToString());
                }

                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa {selectedBrandIds.Count} hãng sản phẩm được chọn?",
                                              "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    bool allDeleted = true;
                    foreach (string brandId in selectedBrandIds)
                    {
                        Brand brand = bbll.GetBrandById(brandId);
                        if (brand != null)
                        {
                            bool isDeleted = bbll.DeleteBrand(brand);
                            if (!isDeleted)
                            {
                                allDeleted = false;
                            }
                        }
                    }
                    if (allDeleted)
                    {
                        MessageBox.Show("Xóa tất cả hãng sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Load_BrandData();
                        BrandDeleted?.Invoke(this, EventArgs.Empty);
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

        private void BtnEditBrand_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtBrandID.Text))
            {
                if(btnEditBrand.Text == "Edit Brand")
                {
                    EnableDataGridView(false);
                    btnAddBrand.Enabled = false;
                    btnAddBrand.BackColor = Color.DarkGray;
                    btnEditBrand.Text = "Accept";
                    btnRemoveBrand.Enabled = false;
                    btnRemoveBrand.BackColor = Color.DarkGray;
                    btnCancel.Enabled = true;
                    btnCancel.BackColor = Color.IndianRed;

                    txtBrandName.Enabled = true;
                    txtBrandDescription.Enabled = true;
                    btnBrandBackground.Enabled = true;
                    btnBrandLogo.Enabled = true;
                    btnBrandLogo.BackColor = Color.FromArgb(32, 80, 189);
                    btnBrandBackground.BackColor = Color.FromArgb(32, 80, 189);
                }
                else if(btnEditBrand.Text == "Accept")
                {
                    if (ValidateInput())
                    {
                        string brandId = txtBrandID.Text;
                        string brandName = txtBrandName.Text.Trim();
                        string description = txtBrandDescription.Text.Trim();
                        string updatedBrandLogo = string.IsNullOrEmpty(brandImageLogo) ? pbBrandLogo.Tag?.ToString() : brandImageLogo;
                        string updatedBrandBackground = string.IsNullOrEmpty(brandImageBackground) ? pbBrandBackground.Tag?.ToString() : brandImageBackground;

                        Brand updatedBrand = new Brand
                        {
                            BrandID = brandId,
                            BrandName = brandName,
                            BrandDescription = description,
                            BrandBackground = updatedBrandBackground,
                            BrandPic = updatedBrandLogo
                        };

                        try
                        {
                            // Gọi repository để cập nhật thông tin
                            bool isSuccess = bbll.UpdateBrand(updatedBrand);

                            if (isSuccess)
                            {
                                MessageBox.Show("Cập nhật hãng sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnAddBrand.Enabled = true;
                                btnAddBrand.BackColor = Color.FromArgb(32, 80, 189);
                                btnRemoveBrand.Enabled = true;
                                btnRemoveBrand.BackColor = Color.FromArgb(32, 80, 189);
                                btnEditBrand.Text = "Edit Brand";
                                btnCancel.Enabled = false;
                                btnCancel.BackColor = Color.DarkGray;

                                txtBrandName.Enabled = false;
                                txtBrandDescription.Enabled = false;
                                btnBrandBackground.Enabled = false;
                                btnBrandLogo.Enabled = false;
                                btnBrandBackground.BackColor = Color.DarkGray;
                                btnBrandLogo.BackColor = Color.DarkGray;

                                Load_BrandData();
                                EnableDataGridView(true);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi cập nhật hãng sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Chọn Brand bạn muốn sửa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnAddBrand_Click(object sender, EventArgs e)
        {
            if(btnAddBrand.Text == "Add New")
            {
                ClearForm();
                EnableDataGridView(false);
                btnAddBrand.Text = "Accept";
                btnCancel.Enabled = true;
                btnCancel.BackColor = Color.IndianRed;
                btnEditBrand.Enabled = false;
                btnEditBrand.BackColor = Color.DarkGray;
                btnRemoveBrand.Enabled = false;
                btnRemoveBrand.BackColor = Color.DarkGray;

                txtBrandID.Enabled = true;
                /*txtBrandID.Text = brandRepository.GenerateBrandID();*/

                txtBrandName.Enabled = true;
                txtBrandDescription.Enabled = true;
                btnBrandBackground.Enabled = true;
                btnBrandLogo.Enabled = true;
                btnBrandLogo.BackColor = Color.FromArgb(32, 80, 189);
                btnBrandBackground.BackColor = Color.FromArgb(32, 80, 189);
            }
            else if (btnAddBrand.Text == "Accept")
            {
                if(ValidateInput())
                {
                    string brandId = txtBrandID.Text;
                    string brandName = txtBrandName.Text.Trim();
                    string description = txtBrandDescription.Text.Trim();

                    Brand newBrand = new Brand 
                    { 
                        BrandID = brandId,
                        BrandName = brandName,
                        BrandDescription = description,
                        BrandBackground = brandImageBackground,
                        BrandPic = brandImageLogo
                    };
                    try
                    {
                        bool isSuccess = bbll.AddBrand(newBrand);
                        if (isSuccess)
                        {
                            MessageBox.Show("Thêm hãng sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Load_BrandData();

                            ClearForm();
                            EnableDataGridView(true);
                            btnEditBrand.Enabled = true;
                            btnEditBrand.BackColor = Color.FromArgb(32, 80, 189);
                            btnRemoveBrand.Enabled = true;
                            btnRemoveBrand.BackColor = Color.FromArgb(32, 80, 189);
                            btnAddBrand.Text = "Add New";
                            btnCancel.Enabled = false;
                            btnCancel.BackColor = Color.DarkGray;

                            txtBrandID.Enabled = false;
                            txtBrandName.Enabled = false;
                            txtBrandDescription.Enabled = false;
                            btnBrandBackground.Enabled = false;
                            btnBrandLogo.Enabled = false;
                            btnBrandBackground.BackColor = Color.DarkGray;
                            btnBrandLogo.BackColor = Color.DarkGray;
                        }
                        else
                        {
                            MessageBox.Show("Thêm hãng sản phẩm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi thêm hãng sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void EnableDataGridView(bool enable)
        {
            dgvBrand.Enabled = enable;
            dgvBrand.DefaultCellStyle.BackColor = enable ? Color.White : Color.LightGray;  // Thay đổi màu nền khi vô hiệu hóa
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtBrandName.Text))
            {
                MessageBox.Show("Vui lòng nhập Brand Name.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(brandImageLogo))
            {
                MessageBox.Show("Vui lòng nhập hình ảnh logo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(brandImageBackground))
            {
                MessageBox.Show("Vui lòng nhập hình ảnh background.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            txtBrandID.Text = "";
            txtBrandName.Text = "";
            txtBrandDescription.Text = "";
            pbBrandLogo.Image = null;
            pbBrandBackground.Image = null;
        }

        private void UcFormBrand_Load(object sender, EventArgs e)
        {
            Load_BrandData();
        }

        private void Load_BrandData()
        {
            try
            {
                List<Brand> brands = bbll.GetAllBrands();
                dgvBrand.DataSource = brands;
                dgvBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải hãng sản phẩm: { ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBrandDetail(string brandId)
        {
            try
            {
                Brand brand = bbll.GetBrandById(brandId);
                if(brand != null)
                {
                    txtBrandID.Text = brand.BrandID;
                    txtBrandName.Text = brand.BrandName;
                    txtBrandDescription.Text = brand.BrandDescription;

                    string picBrandFolder = GetWebProjectPicFolderPath();

                    if (!string.IsNullOrEmpty(brand.BrandPic))
                    {
                        string imagePath = Path.Combine(picBrandFolder, Path.GetFileName(brand.BrandPic)).Replace("/", "\\");

                        if (File.Exists(imagePath))
                        {
                            try
                            {
                                if (pbBrandLogo.Image != null)
                                {
                                    pbBrandLogo.Image.Dispose();
                                    pbBrandLogo.Image = null;
                                }

                                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                                {
                                    Image img = Image.FromStream(fs);
                                    pbBrandLogo.Image = new Bitmap(img);
                                    pbBrandLogo.Tag = brand.BrandPic;

                                    brandImageLogo = brand.BrandPic;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi tải hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hình ảnh không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (!string.IsNullOrEmpty(brand.BrandBackground))
                    {
                        string imagePath = Path.Combine(picBrandFolder, Path.GetFileName(brand.BrandBackground)).Replace("/", "\\");

                        if (File.Exists(imagePath))
                        {
                            try
                            {
                                if (pbBrandBackground.Image != null)
                                {
                                    pbBrandBackground.Image.Dispose();
                                    pbBrandBackground.Image = null;
                                }

                                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                                {
                                    Image img = Image.FromStream(fs);
                                    pbBrandBackground.Image = new Bitmap(img);
                                    pbBrandBackground.Tag = brand.BrandBackground;

                                    brandImageBackground = brand.BrandBackground;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi tải hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hình ảnh không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string relativePath = @"..\..\..\PicBrand";

            // Kết hợp với đường dẫn gốc của ứng dụng để tạo đường dẫn tuyệt đối
            string absolutePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath));

            return absolutePath;
        }
    }
}
