using CustomControl;
using DTO;
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
using Sunny.UI;
using NPOI.SS.Formula.Functions;

namespace App_QLWeb_DoDienTu
{
    public partial class frmBrand : Form
    {
        BrandBLL bbll = new BrandBLL();
        string brandImageLogo;
        string brandImageBackground;
        private frmAdmin parentfrm;
        public frmBrand(frmAdmin parentfrm)
        {
            InitializeComponent();
            this.Load += FrmBrand_Load;
            this.parentfrm = parentfrm;
            this.btnBack.Click += BtnBack_Click;

            this.dgvBrand.SelectionChanged += DgvBrand_SelectionChanged;
            this.btnThem.Click += BtnAddBrand_Click;
            this.btnSua.Click += BtnEditBrand_Click;
            this.btnXoa.Click += BtnRemoveBrand_Click;
            this.btnHuyBo.Click += BtnCancel_Click;
            this.btnChonAnhBack.Click += BtnBrandBackground_Click;
            this.btnChonAnhLogo.Click += BtnBrandLogo_Click;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct(parentfrm);
            parentfrm.OpenChildForm(frm);
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
                        if (pbBackgroundBrand.Image != null)
                        {
                            pbBackgroundBrand.Image.Dispose();
                            pbBackgroundBrand.Image = null;
                        }

                        // Tải hình ảnh từ PicBrand folder
                        using (FileStream fs = new FileStream(finalImagePath, FileMode.Open, FileAccess.Read))
                        {
                            Image img = Image.FromStream(fs);
                            pbBackgroundBrand.Image = new Bitmap(img);
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
        private void EnableTextBox(bool enable)
        {
            txtBrandDescription.Enabled = enable;
            txtBrandName.Enabled = enable;
            btnChonAnhBack.Enabled = enable;
            btnChonAnhLogo.Enabled = enable;
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

        private void BtnRemoveBrand_Click(object sender, EventArgs e)
        {
            if (txtBrandID.Text != string.Empty)
            {
                string maThuongHieu = txtBrandID.Text;
                string tenThuongHieu = txtBrandName.Text;
                DialogResult r = MessageBox.Show(this, "Bạn có chắc chắc muốn xóa thương hiệu " + tenThuongHieu + " này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    Brand thuongHieu = new Brand
                    {
                        BrandID = maThuongHieu,
                        BrandName = tenThuongHieu,

                    };
                    int dem = bbll.DemSoSanPhamThuocThuongHieu(thuongHieu.BrandID);
                    if (dem > 0)
                    {
                        MessageBox.Show(this, $"Không thể xóa thương hiệu {thuongHieu.BrandName} do còn sản phẩm thuộc thương hiệu này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        bool isSuccess = bbll.DeleteBrand(thuongHieu);
                        if (isSuccess)
                        {
                            MessageBox.Show(this, "Xóa thương hiệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Load_BrandData();
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

        private void BtnEditBrand_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBrandID.Text))
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
                        string brandId = txtBrandID.Text;
                        string brandName = txtBrandName.Text.Trim();
                        string description = txtBrandDescription.Text.Trim();
                        string updatedBrandLogo = string.IsNullOrEmpty(brandImageLogo) ? pbBrandLogo.Tag?.ToString() : brandImageLogo;
                        string updatedBrandBackground = string.IsNullOrEmpty(brandImageBackground) ? pbBackgroundBrand.Tag?.ToString() : brandImageBackground;

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
                                MessageBox.Show("Cập nhật thương hiệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Load_BrandData();

                                ClearForm();
                                EnableDataGridView(true);
                                btnThem.Enabled = true;
                                SetButtonStyle(btnThem, true);
                                btnXoa.Enabled = true;
                                SetButtonStyle(btnXoa, true);
                                btnSua.Text = "Sửa";
                                btnHuyBo.Enabled = false;
                                btnHuyBo.BackColor = Color.DarkGray;

                                EnableTextBox(false);
                            }
                            else
                            {
                                MessageBox.Show("Sửa thương hiệu thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi cập nhật thương hiệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Chọn thương hiệu bạn muốn sửa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnAddBrand_Click(object sender, EventArgs e)
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
                txtBrandID.Text = bbll.GenerateBrandID();
                EnableTextBox(true);
            }
            else if (btnThem.Text == "Xác nhận")
            {
                if (ValidateInput())
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
                            MessageBox.Show("Thêm thương hiệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Load_BrandData();

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
                            MessageBox.Show("Thêm thương hiệu thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi thêm thương hiệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            pbBackgroundBrand.Image = null;
        }

        private void FrmBrand_Load(object sender, EventArgs e)
        {
            Load_BrandData();
            EnableTextBox(false);
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
                MessageBox.Show($"Lỗi tải thương hiệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBrandDetail(string brandId)
        {
            try
            {
                Brand brand = bbll.GetBrandById(brandId);
                if (brand != null)
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
                                if (pbBackgroundBrand.Image != null)
                                {
                                    pbBackgroundBrand.Image.Dispose();
                                    pbBackgroundBrand.Image = null;
                                }

                                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                                {
                                    Image img = Image.FromStream(fs);
                                    pbBackgroundBrand.Image = new Bitmap(img);
                                    pbBackgroundBrand.Tag = brand.BrandBackground;

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
                MessageBox.Show($"Lỗi tải chi tiết thương hiệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
