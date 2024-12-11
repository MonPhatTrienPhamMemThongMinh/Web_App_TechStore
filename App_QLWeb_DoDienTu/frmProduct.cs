using CustomControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using System.IO;
using BLL;
using Sunny.UI;

namespace App_QLWeb_DoDienTu
{
    public partial class frmProduct : Form
    {
        ProductBLL pbll = new ProductBLL();
        CategoryBLL ctbll = new CategoryBLL();
        BrandBLL bbll = new BrandBLL();
        SupplierBLL sbll = new SupplierBLL();
        private BindingSource bindingSource;
        private string imagePath;

        private frmAdmin parentfrm;
        public frmProduct(frmAdmin parentfrm)
        {
            InitializeComponent();
            this.parentfrm = parentfrm;
            this.bindingSource = new BindingSource();
            this.Load += FrmProduct_Load;
            this.btnChonAnh.Click += BtnChonAnh_Click;
            this.btnHuyBo.Click += BtnHuyBo_Click;
            this.btnThem.Click += BtnThem_Click;
            this.btnSua.Click += BtnSua_Click;
            this.dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;

            this.btnBrand.Click += BtnBrand_Click;
            this.btnCategory.Click += BtnCategory_Click;
        }

        private void BtnCategory_Click(object sender, EventArgs e)
        {
            frmCategory frm = new frmCategory(parentfrm);
            parentfrm.OpenChildForm(frm);
        }

        private void BtnBrand_Click(object sender, EventArgs e)
        {
            frmBrand frm = new frmBrand(parentfrm);
            parentfrm.OpenChildForm(frm);
        }
        private void DgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvProducts.SelectedRows[0];
                if (selectedRow.Cells["ProductID"].Value != null)
                {
                    string productID = selectedRow.Cells["ProductID"].Value.ToString();
                    LoadProductDetail(productID);
                }
            }
        }

        private void BtnChonAnh_Click(object sender, EventArgs e)
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
                        if (hinhAnh.Image != null)
                        {
                            hinhAnh.Image.Dispose();
                            hinhAnh.Image = null;
                        }

                        // Tải hình ảnh từ Pic folder
                        using (FileStream fs = new FileStream(finalImagePath, FileMode.Open, FileAccess.Read))
                        {
                            Image img = Image.FromStream(fs);
                            hinhAnh.Image = new Bitmap(img);
                        }

                        // Set productImagePath sau khi tải thành công
                        imagePath = Path.Combine("Pic", Path.GetFileName(finalImagePath)).Replace("\\", "/");
                    }
                    catch (OutOfMemoryException)
                    {
                        MessageBox.Show("Định dạng hình ảnh không hợp lệ hoặc hình ảnh quá lớn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        imagePath = null;
                        return;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Hình ảnh không hợp lệ hoặc bị hỏng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        imagePath = null;
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi tải hình ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        imagePath = null;
                        return;
                    }
                }
            }
        }

        private void BtnHuyBo_Click(object sender, EventArgs e)
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

        private void BtnRemoveBrand_Click(object sender, EventArgs e)
        {

        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (txtMaSanPham.Text != string.Empty)
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
                        string updatedProductPic = string.IsNullOrEmpty(imagePath) ? hinhAnh.Tag?.ToString() : imagePath;

                        Product newProduct = new Product()
                        {
                            ProductID = txtMaSanPham.Text.Trim(),
                            ProductName = txtTenSanPham.Text.Trim(),
                            CategoryID = cbLoaiSP.SelectedValue.ToString(),
                            BrandID = cboTH.SelectedValue.ToString(),
                            maNhaCungCap = cboNCC.SelectedValue.ToString(),
                            ProductPic = imagePath,

                            ProductDescription = txtMoTa.Text.Trim(),
                            BaoHanh = txtBaoHanh.Text.Trim(),
                        };
                        try
                        {
                            bool isSuccess = pbll.UpdateProduct(newProduct);
                            if (isSuccess)
                            {
                                MessageBox.Show("Sửa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();

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
                                MessageBox.Show("Sửa sản phẩm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi sửa sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
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
        private void BtnThem_Click(object sender, EventArgs e)
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
                txtMaSanPham.Text = pbll.TaoMaSanPham();
                EnableTextBox(true);
            }
            else if (btnThem.Text == "Xác nhận")
            {
                if (ValidateInput())
                {
                    string maSanPham = txtMaSanPham.Text.Trim();
                    string tenSanPham = txtTenSanPham.Text.Trim();
                    string moTa = txtMoTa.Text.Trim();
                    Product newProduct = new Product
                    {
                        ProductID = maSanPham,
                        ProductName = tenSanPham,
                        ProductDescription = moTa,
                        CategoryID = cbLoaiSP.SelectedValue.ToString(),
                        BrandID = cboTH.SelectedValue.ToString(),
                        maNhaCungCap = cboNCC.SelectedValue.ToString(),
                        Price = 0,
                        ProductPic = imagePath,
                        AvailabilityStatus = "OutOfStock",
                        Quantity = 0,
                        BaoHanh = txtBaoHanh.Text.Trim()
                    };
                    try
                    {
                        bool isSuccess = pbll.AddProduct(newProduct);
                        if (isSuccess)
                        {
                            MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
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
                            MessageBox.Show("Thêm sản phẩm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi thêm sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadData()
        {
            List<Product> danhSachSanPham = pbll.GetAllProducts();
            bindingSource.DataSource = danhSachSanPham;
            dgvProducts.DataSource = bindingSource;
            dgvProducts.Columns["ProductPic"].Visible = false;
            dgvProducts.Columns["Category"].Visible = false;
            dgvProducts.Columns["Brand"].Visible = false;
            dgvProducts.Columns["NhaCungCap"].Visible = false;
            LoadCBLoaiSanPham();
            LoadCBThuongHieu();
            LoadCBNhaCungCap();
        }
        private void LoadCBLoaiSanPham()
        {
            cbLoaiSP.DataSource = ctbll.GetAllCategories();
            cbLoaiSP.ValueMember = "CategoryID";
            cbLoaiSP.DisplayMember = "CategoryName";

            cbLocTheoLoai.DataSource = ctbll.GetAllCategories();
            cbLocTheoLoai.ValueMember = "CategoryID";
            cbLocTheoLoai.DisplayMember = "CategoryName";

            cbTrangThai.SelectedIndex = 0;
        }
        private void LoadCBThuongHieu()
        {
            cboTH.DataSource = bbll.GetAllBrands();
            cboTH.ValueMember = "BrandID";
            cboTH.DisplayMember = "BrandName";
        }
        private void LoadCBNhaCungCap()
        {
            cboNCC.DataSource = sbll.getAllSuppliers();
            cboNCC.ValueMember = "maNhaCungCap";
            cboNCC.DisplayMember = "tenNhaCungCap";
        }

        private void EnableDataGridView(bool enable)
        {
            dgvProducts.Enabled = enable;
            dgvProducts.DefaultCellStyle.BackColor = enable ? Color.White : Color.LightGray;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (imagePath == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh sản phẩm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void ClearForm()
        {
            txtMaSanPham.Text = "";
            txtTenSanPham.Text = "";
            txtMoTa.Text = "";
            hinhAnh.Image = null;
            cboTH.SelectedIndex = 0;
            cbLoaiSP.SelectedIndex = 0;
            cboNCC.SelectedIndex = 0;
            txtBaoHanh.Text = "";
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            LoadData();
            EnableTextBox(false);
        }
        private void EnableTextBox(bool enable)
        {
            txtTenSanPham.Enabled = enable;
            txtMoTa.Enabled = enable;
            txtBaoHanh.Enabled = enable;
            cbLoaiSP.Enabled = enable;
            btnChonAnh.Enabled = enable;
            cboTH.Enabled = enable;
            cboNCC.Enabled = enable;
        }
        private void LoadProductDetail(string productId)
        {
            try
            {
                Product product = pbll.GetProductById(productId);
                if (product != null)
                {
                    txtMaSanPham.Text = product.ProductID;
                    txtTenSanPham.Text = product.ProductName;
                    txtMoTa.Text = product.ProductDescription;
                    txtBaoHanh.Text = product.BaoHanh;
                    cboTH.SelectedValue = product.BrandID;
                    cbLoaiSP.SelectedValue = product.CategoryID;
                    cboNCC.SelectedValue = product.maNhaCungCap;

                    string absolutePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Pic"));
                    imagePath = Path.Combine(absolutePath, Path.GetFileName(product.ProductPic)).Replace("/", "\\");
                    hinhAnh.Image = Image.FromFile(imagePath);
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

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                txtTimKiem.Text = "Nhập mã hoặc tên sản phẩm để tìm kiếm";
                txtTimKiem.ForeColor = Color.Silver;
                txtTimKiem.Font = new Font(txtTimKiem.Font, FontStyle.Italic);
                LoadData();
            }
        }
        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập mã hoặc tên sản phẩm để tìm kiếm")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
                txtTimKiem.Font = new Font(txtTimKiem.Font, FontStyle.Regular);
            }
        }
        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var results = pbll.SearchProducts(txtTimKiem.Text.Trim());

            // Kiểm tra kết quả trả về có hợp lệ không
            if (results != null && results.Count > 0)
            {
                // Cập nhật DataGridView với kết quả tìm kiếm
                bindingSource.DataSource = results;
            }
            else
            {
                // Nếu không tìm thấy kết quả, thông báo cho người dùng
                MessageBox.Show("Không tìm thấy sản phẩm nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
