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
        private string selectedFilePath;
        private string saveFilePath;
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
            this.btnXoa.Click += BtnXoa_Click;
            this.dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;
            this.btnBrand.Click += BtnBrand_Click;
            this.btnCategory.Click += BtnCategory_Click;
            this.cbLocTheoLoai.SelectedIndexChanged += CbLocTheoLoaiSP_SelectedIndexChanged;
            this.cbNhaCungCap.SelectedIndexChanged += CbNhaCungCap_SelectedIndexChanged;
            this.cbThuongHieu.SelectedIndexChanged += CbThuongHieu_SelectedIndexChanged;
            this.cbTrangThai.SelectedIndexChanged += CbTrangThai_SelectedIndexChanged;
        }
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                DialogResult r = MessageBox.Show(this, "Bạn có chắc chắc muốn xóa sản phẩm không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    int soLuong = int.Parse(dgvProducts.SelectedRows[0].Cells["soLuong"].Value.ToString());
                    if (soLuong > 0)
                    {
                        MessageBox.Show(this, "Không thể xóa sản phẩm do sản phầm còn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (pbll.KiemTraSanPhamCoThuocHoaDon(dgvProducts.SelectedRows[0].Cells["maSanPham"].Value.ToString()))
                    {
                        MessageBox.Show(this, "Không thể xóa sản phẩm do sản phầm có trong hóa đơn bán hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (pbll.KiemTraSanPhamCoThuocPhieuDat(dgvProducts.SelectedRows[0].Cells["maSanPham"].Value.ToString()))
                    {
                        MessageBox.Show(this, "Không thể xóa sản phẩm do sản phầm có trong phiếu đặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        bool isSuccess = pbll.DeleteProduct(dgvProducts.SelectedRows[0].Cells["maSanPham"].Value.ToString());
                        if (isSuccess)
                        {
                            MessageBox.Show(this, "Xóa sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show(this, "Xóa sản phẩm thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }
        private void CbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTrangThai.SelectedItem!=null)
            {
                bindingSource.DataSource = pbll.LocSanPhamTheoTrangThai(cbTrangThai.SelectedText);
            }            
        }
        private void CbThuongHieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbThuongHieu.SelectedItem!=null)
            {
                bindingSource.DataSource = pbll.LocSanPhamTheoThuongHieu(cbThuongHieu.SelectedValue.ToString());
            }            
        }
        private void CbNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNhaCungCap.SelectedItem!=null)
            {
                bindingSource.DataSource = pbll.LocSanPhamTheoNhaCungCap(cbNhaCungCap.SelectedValue.ToString());
            }            
        }
        private void CbLocTheoLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocTheoLoai.SelectedItem!=null)
            {
                bindingSource.DataSource = pbll.LocSanPhamTheoLoai(cbLocTheoLoai.SelectedValue.ToString());
            }            
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
                if (selectedRow.Cells["maSanPham"].Value != null)
                {
                    string productID = selectedRow.Cells["maSanPham"].Value.ToString();
                    LoadProductDetail(productID);
                }
            }
        }
        private bool IsImageFile(string filePath)
        {
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".webp" };
            string fileExtension = Path.GetExtension(filePath)?.ToLower();

            return Array.Exists(validExtensions, ext => ext == fileExtension);
        }
        private void BtnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";
                openFileDialog.Title = "Chọn hình ảnh sản phẩm";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Đường dẫn file đã chọn
                    selectedFilePath = openFileDialog.FileName;

                    // Đường dẫn thư mục lưu ảnh (thiết lập sẵn)
                    string relativeFolder = @"..\..\..\Pic";
                    string destinationFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeFolder));

                    // Tạo thư mục nếu chưa tồn tại
                    Directory.CreateDirectory(destinationFolder);
                    // Đường dẫn file lưu
                    string fileName = Path.GetFileName(selectedFilePath); // Lấy tên file gốc
                    saveFilePath = Path.Combine(destinationFolder, fileName); // Tạo đường dẫn đầy đủ                    
                    imagePath = Path.Combine("Pic", fileName);
                    if (IsImageFile(selectedFilePath))
                    {
                        hinhAnh.Image = Image.FromFile(selectedFilePath);
                    }
                    else
                    {
                        MessageBox.Show(this, "Vui lòng chọn file ảnh hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        if (selectedFilePath != null && saveFilePath != null)
                        {
                            try
                            {
                                // Sao chép file đến nơi lưu
                                File.Copy(selectedFilePath, saveFilePath, overwrite: true);
                                hinhAnh.Image = Image.FromFile(saveFilePath);
                                MessageBox.Show($"Đã lưu ảnh thành công tại {saveFilePath}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi lưu ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        Product newProduct = new Product()
                        {
                            ProductID = txtMaSanPham.Text.Trim(),
                            ProductName = txtTenSanPham.Text.Trim(),
                            CategoryID = cbLoaiSP.SelectedValue.ToString(),
                            BrandID = cboTH.SelectedValue.ToString(),
                            maNhaCungCap = cboNCC.SelectedValue.ToString(),
                            ProductPic = imagePath,
                            ProductDescription = txtMoTa.Texts.Trim(),
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
                    try
                    {
                        // Sao chép file đến nơi lưu
                        File.Copy(selectedFilePath, saveFilePath, overwrite: true);
                        MessageBox.Show($"Đã lưu ảnh thành công tại {saveFilePath}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi lưu ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    string maSanPham = txtMaSanPham.Text.Trim();
                    string tenSanPham = txtTenSanPham.Text.Trim();
                    string moTa = txtMoTa.Texts.Trim();
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
            cbLocTheoLoai.SelectedIndex = 0;
        }
        private void LoadCBThuongHieu()
        {
            cboTH.DataSource = bbll.GetAllBrands();
            cboTH.ValueMember = "BrandID";
            cboTH.DisplayMember = "BrandName";

            cbThuongHieu.DataSource = bbll.GetAllBrands();
            cbThuongHieu.ValueMember = "BrandID";
            cbThuongHieu.DisplayMember = "BrandName";
            cbThuongHieu.SelectedIndex = 0;
        }
        private void LoadCBNhaCungCap()
        {
            cboNCC.DataSource = sbll.getAllSuppliers();
            cboNCC.ValueMember = "maNhaCungCap";
            cboNCC.DisplayMember = "tenNhaCungCap";

            cbNhaCungCap.DataSource = sbll.getAllSuppliers();
            cbNhaCungCap.ValueMember = "maNhaCungCap";
            cbNhaCungCap.DisplayMember = "tenNhaCungCap";
            cbNhaCungCap.SelectedIndex = 0;
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
            txtMoTa.Texts = "";
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
                    txtMoTa.Texts = product.ProductDescription;
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
                MessageBox.Show($"Lỗi tải chi tiết sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
