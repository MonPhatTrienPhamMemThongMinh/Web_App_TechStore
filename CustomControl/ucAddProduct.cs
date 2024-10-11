using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DTO;
using BLL;

namespace CustomControl
{
    public partial class ucAddProduct : UserControl
    {
        private ProductBLL pbll = new ProductBLL();

        public event EventHandler ProductAdded;

        private string productImagePath;

        public ucAddProduct()
        {
            InitializeComponent();
            this.Load += UcAddProduct_Load;
            this.btnAccept.Click += BtnAccept_Click;
            this.btnClose.Click += BtnClose_Click;
            this.btnImportImage.Click += BtnImportImage_Click;
        }
        private void UcAddProduct_Load(object sender, EventArgs e)
        {
            LoadBrands();
            LoadCategories();
            LoadSuppliers();
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                string productId = txtProductId.Text.Trim();
                string productName = txtProductName.Text.Trim();
                string description = txtProductDescription.Text.Trim();
                string warranty = txtWarranty.Text.Trim();
                string brandId = cboBrand.SelectedValue.ToString();
                string categoryId = cboCategory.SelectedValue.ToString();
                string supplierId = cboSupplier.SelectedValue.ToString();

                if (!int.TryParse(txtPrice.Text.Trim(), out int price))
                {
                    MessageBox.Show("Vui lòng nhập giá hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtQuantity.Text.Trim(), out int quantity))
                {
                    MessageBox.Show("Vui lòng nhập số lượng hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string availabilityStatus = quantity > 0 ? "In Stock" : "Out of Stock";

                Product newProduct = new Product
                {
                    ProductID = productId,
                    ProductName = productName,
                    Price = price,
                    Quantity = quantity,
                    ProductDescription = description,
                    BrandID = brandId,
                    CategoryID = categoryId,
                    AvailabilityStatus = availabilityStatus,
                    BaoHanh = warranty,
                    ProductPic = productImagePath,
                    maNhaCungCap = supplierId
                };

                try
                {
                    bool isSuccess = pbll.AddProduct(newProduct);
                    if (isSuccess)
                    {
                        MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ProductAdded?.Invoke(this, EventArgs.Empty);
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Thêm sản phẩm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi thêm sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtProductId.Text))
            {
                MessageBox.Show("Vui lòng nhập Product ID.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập Product Name.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Vui lòng nhập Price.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show("Vui lòng nhập Quantity.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtWarranty.Text))
            {
                MessageBox.Show("Vui lòng nhập Warranty.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(productImagePath))
            {
                MessageBox.Show("Vui lòng nhập hình ảnh sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboBrand.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn thương hiệu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn danh mục.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboSupplier.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public void ClearForm()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            txtWarranty.Clear();
            cboBrand.SelectedIndex = -1;
            cboCategory.SelectedIndex = -1;
            cboSupplier.SelectedIndex = -1;
            /*cboStatus.SelectedIndex = -1*/
            txtProductDescription.Clear();
            pbProductImage.Image = null;
            productImagePath = null;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Form form = this.FindForm();
            if (form != null)
            {
                form.Close();
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

        private void BtnImportImage_Click(object sender, EventArgs e)
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
                    string destinationPath = Path.Combine(picFolder, fileName); // Đường dẫn đích tới thư mục Pic của web

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
                        if (pbProductImage.Image != null)
                        {
                            pbProductImage.Image.Dispose();
                            pbProductImage.Image = null;
                        }

                        // Tải hình ảnh từ Pic folder
                        using (FileStream fs = new FileStream(finalImagePath, FileMode.Open, FileAccess.Read))
                        {
                            Image img = Image.FromStream(fs);
                            pbProductImage.Image = new Bitmap(img);
                        }

                        // Set productImagePath sau khi tải thành công
                        productImagePath = Path.Combine("Pic", Path.GetFileName(finalImagePath)).Replace("\\", "/");
                    }
                    catch (OutOfMemoryException)
                    {
                        MessageBox.Show("Định dạng hình ảnh không hợp lệ hoặc hình ảnh quá lớn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        productImagePath = null;
                        return;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Hình ảnh không hợp lệ hoặc bị hỏng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        productImagePath = null;
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi tải hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        productImagePath = null;
                        return;
                    }
                }
            }
        }

        private void LoadBrands()
        {
            
        }

        private void LoadCategories()
        {
            
        }
        private void LoadSuppliers()
        {
            
        }
    }
}
