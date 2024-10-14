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
using DTO;
using BLL;

namespace CustomControl
{
    public partial class ucFormProduct : UserControl
    {
        private ProductBLL pbll = new ProductBLL();

        public event EventHandler AddProductClicked;
        public event EventHandler EditProductClicked;
        public event EventHandler BrandFormClicked;
        public event EventHandler CategoryFormClicked;
        public event EventHandler SupplierFormClicked;
        public ucFormProduct()
        {
            InitializeComponent();
            this.Load += UcFormProduct_Load;
            this.dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;
            this.btnAddProduct.Click += BtnAddProduct_Click;
            this.btnRemoveProduct.Click += BtnRemoveProduct_Click;
            this.btnEditProduct.Click += BtnEditProduct_Click;
            this.btnSearch.Click += BtnSearch_Click;
            this.btnBrands.Click += BtnBrands_Click;
            this.btnRefresh.Click += BtnRefresh_Click;
            this.btnCategories.Click += BtnCategories_Click;
            this.btnSuppliers.Click += BtnSuppliers_Click;
        }

        private void BtnSuppliers_Click(object sender, EventArgs e)
        {
            SupplierFormClicked?.Invoke(this, new EventArgs());
        }

        private void BtnCategories_Click(object sender, EventArgs e)
        {
            CategoryFormClicked?.Invoke(this, EventArgs.Empty);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void DgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvProducts.SelectedRows[0];
                if (selectedRow.Cells["ProductID"].Value != null)
                {
                    string productId = selectedRow.Cells["ProductID"].Value.ToString();
                    LoadProductDetail(productId); 
                }
            }
        }

        private void BtnBrands_Click(object sender, EventArgs e)
        {
            BrandFormClicked?.Invoke(this, EventArgs.Empty);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchItem = txtSearch.Text.Trim();
            List<Product> searchResults = pbll.SearchProducts(searchItem);

            if (searchResults != null)
            {
                dgvProducts.DataSource = searchResults;
                dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                MessageBox.Show("Không tìm thấy sản phẩm nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnEditProduct_Click(object sender, EventArgs e)
        {
            EditProductClicked?.Invoke(this, EventArgs.Empty);
        }

        private void BtnRemoveProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                List<string> selectedProductIds = new List<string>();
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
                        Product product1 = pbll.GetProductById(productId);
                        if (product1 != null)
                        {
                            bool isDeleted = pbll.DeleteProduct(product1);
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
            LoadProducts();
        }

        public void LoadProducts()
        {
            try
            {
                List<Product> products = pbll.GetAllProducts();
                dgvProducts.DataSource = products;
                if (dgvProducts.Columns["Brand"] != null)
                {
                    dgvProducts.Columns["Brand"].Visible = false;
                }
                if (dgvProducts.Columns["Category"] != null)
                {
                    dgvProducts.Columns["Category"].Visible = false;
                }
                if (dgvProducts.Columns["NhaCungCap"] != null)
                {
                    dgvProducts.Columns["NhaCungCap"].Visible = false;
                }
                if (dgvProducts.Columns["BrandID"] != null)
                {
                    dgvProducts.Columns["BrandID"].Visible = false;
                }
                if (dgvProducts.Columns["CategoryID"] != null)
                {
                    dgvProducts.Columns["CategoryID"].Visible = false;
                }
                if (dgvProducts.Columns["maNhaCungCap"] != null)
                {
                    dgvProducts.Columns["maNhaCungCap"].Visible = false;
                }
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
                Product product = pbll.GetProductById(productId);
                if(product != null)
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
                                if(pbProductImage.Image != null)
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
                            catch (Exception ex) {
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
                    return pbll.GetProductById(selectedProductId);
                }
                return null;
            }
        }
    }
}
