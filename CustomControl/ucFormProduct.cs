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
using ThuVien;
using ThuVien.DataAccess;
using ThuVien.Models;

namespace CustomControl
{
    public partial class ucFormProduct : UserControl
    {
        string cnn;
        private ProductRepository productRepository;

        public event EventHandler AddProductClicked;
        public event EventHandler EditProductClicked;
        public event EventHandler BrandFormClicked;

        [Category("CustomControl")]
        public string CNN
        {
            get { return cnn; }
            set
            {
                cnn = value;
            }
        }
        public ucFormProduct()
        {
            InitializeComponent();
            this.Load += UcFormProduct_Load;
            this.dgvProducts.CellClick += DgvProducts_CellClick; ;
            this.btnAddProduct.Click += BtnAddProduct_Click;
            this.btnRemoveProduct.Click += BtnRemoveProduct_Click;
            this.btnEditProduct.Click += BtnEditProduct_Click;
            this.btnSearch.Click += BtnSearch_Click;
            this.btnBrands.Click += BtnBrands_Click;
        }

        private void BtnBrands_Click(object sender, EventArgs e)
        {
            BrandFormClicked?.Invoke(this, EventArgs.Empty);
        }

        private void DgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string selectedProductId = dgvProducts.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
                LoadProductDetail(selectedProductId);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchItem = txtSearch.Text.Trim();

            if (!string.IsNullOrEmpty(searchItem))
            {
                productRepository = new ProductRepository(cnn);
                List<Product> searchResults = productRepository.SearchProducts(searchItem);

                dgvProducts.DataSource = searchResults.Select(p => new
                {
                    p.ProductID,
                    p.ProductName,
                    p.AvailabilityStatus,
                    p.Quantity,
                    p.BaoHanh,
                    p.ProductPic,
                    p.Price,
                    p.BrandName,
                    p.CategoryName,
                    p.ProductDescription,
                    p.SupplierName
                }).ToList();

                dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEditProduct_Click(object sender, EventArgs e)
        {
            EditProductClicked?.Invoke(this, EventArgs.Empty);
        }

        private void BtnRemoveProduct_Click(object sender, EventArgs e)
        {
            // Ktra xem có ít nhất 1 sản phẩm dudoc chọn ko
            if (dgvProducts.SelectedRows.Count > 0)
            {
                List<string> selectedProductIds = new List<string>();

                // Lấy danh sách các ProductID của những sản phẩm được chọn]
                foreach (DataGridViewRow row in dgvProducts.SelectedRows)
                {
                    selectedProductIds.Add(row.Cells["ProductID"].Value.ToString());
                }

                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa {selectedProductIds.Count} sản phẩm được chọn?",
                                              "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    bool allDeleted = true;
                    foreach (string productId in selectedProductIds)
                    {
                        Product product1 = productRepository.GetProductById(productId);
                        if (product1 != null)
                        {
                            bool isDeleted = productRepository.DeleteProduct(product1);
                            if (!isDeleted)
                            {
                                allDeleted = false;
                            }
                        }
                    }
                    if (allDeleted)
                    {
                        MessageBox.Show("Xóa tất cả sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Một số sản phẩm không thể xóa được.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            AddProductClicked?.Invoke(this, EventArgs.Empty);
        }

        private void UcFormProduct_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cnn))
            {
                LoadProducts();
            }
        }

        public void LoadProducts()
        {
            productRepository = new ProductRepository(cnn);
            try
            {
                List<Product> products = productRepository.GetAllProducts();
                dgvProducts.DataSource = products.Select(p => new
                {
                    p.ProductID,
                    p.ProductName,
                    p.AvailabilityStatus,
                    p.Quantity,
                    p.BaoHanh,
                    p.ProductPic,
                    p.Price,
                    p.BrandName,
                    p.CategoryName,
                    p.ProductDescription,
                    p.SupplierName
                }).ToList();

                dgvProducts.Columns["ProductID"].HeaderText = "Product ID";
                dgvProducts.Columns["ProductName"].HeaderText = "Product Name";
                dgvProducts.Columns["AvailabilityStatus"].HeaderText = "Availability Status";
                dgvProducts.Columns["Quantity"].HeaderText = "Quantity";
                dgvProducts.Columns["BaoHanh"].HeaderText = "Warranty";
                dgvProducts.Columns["ProductPic"].HeaderText = "Product Pic";
                dgvProducts.Columns["Price"].HeaderText = "Price";
                dgvProducts.Columns["BrandName"].HeaderText = "Brand Name";
                dgvProducts.Columns["CategoryName"].HeaderText = "Category Name";
                dgvProducts.Columns["ProductDescription"].HeaderText = "Description";
                dgvProducts.Columns["SupplierName"].HeaderText = "Supplier Name";

                dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductDetail(string productId)
        {
            try
            {
                Product product = productRepository.GetProductById(productId);
                if (product != null)
                {
                    lblProductId.Text = product.ProductID;
                    lblProductName.Text = product.ProductName;
                    lblPrice.Text = product.Price.ToString("C");
                    lblQuantity.Text = product.Quantity.ToString();
                    lblBrandName.Text = product.BrandName;
                    lblCategoryName.Text = product.CategoryName;
                    lblWarranty.Text = product.BaoHanh;

                    string picFolder = GetWebProjectPicFolderPath();

                    if (!string.IsNullOrEmpty(product.ProductPic))
                    {
                        string imagePath = Path.Combine(picFolder, Path.GetFileName(product.ProductPic)).Replace("/", "\\");

                        if (File.Exists(imagePath))
                        {
                            try
                            {
                                if (pbProductImage.Image != null)
                                {
                                    pbProductImage.Image.Dispose();
                                    pbProductImage.Image = null;
                                }

                                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                                {
                                    Image img = Image.FromStream(fs);
                                    pbProductImage.Image = new Bitmap(img);
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
                MessageBox.Show($"Lỗi tải chi tiết sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public Product SelectedProduct
        {
            get
            {
                if (dgvProducts.SelectedRows.Count > 0)
                {
                    string selectedProductId = dgvProducts.SelectedRows[0].Cells["ProductID"].Value.ToString();
                    return productRepository.GetProductById(selectedProductId);
                }
                return null;
            }
        }
    }
}
