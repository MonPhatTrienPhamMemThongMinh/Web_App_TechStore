using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using Sunny.UI;
namespace App_QLWeb_DoDienTu
{
    public partial class frmDatHang : Form
    {
        #region
        private string maNhanVien;
        private string maPhieuDat;
        private string maNhaCungCap;
        private bool themPhieu;
        private BindingSource bindingSource;
        private BindingSource chiTietPhieuDats;
        private ProductBLL sanPhamBLL;
        private CategoryBLL loaiSanPhamBLL;
        private SupplierBLL nhaCungCapBLL;
        private PhieuDatBLL phieuDatBLL;
        private ChiTietPhieuDatBLL chiTietPhieuDatBLL;
        private List<DataGridViewRow> deletedRows;
        private List<ChiTietPhieuDat> chiTietPhieuDatBiXoa;
        public delegate void SendDataHandler(bool loadData);
        public event SendDataHandler DongForm;
        #endregion
        public frmDatHang(string maNhanVien,bool themPhieu,string maPhieuDat,string maNhaCungCap)
        {
            this.maNhanVien = maNhanVien;            
            this.themPhieu = themPhieu;
            if (!themPhieu)
            {
                this.maPhieuDat = maPhieuDat;
                this.maNhaCungCap = maNhaCungCap;
                this.chiTietPhieuDats = new BindingSource();                
            }
            this.bindingSource = new BindingSource();
            this.sanPhamBLL = new ProductBLL();
            this.loaiSanPhamBLL = new CategoryBLL();
            this.nhaCungCapBLL = new SupplierBLL();
            this.phieuDatBLL = new PhieuDatBLL();
            this.chiTietPhieuDatBLL = new ChiTietPhieuDatBLL();
            this.deletedRows = new List<DataGridViewRow>();
            this.chiTietPhieuDatBiXoa = new List<ChiTietPhieuDat>();
            InitializeComponent();
            this.txtSoLuongSanPham.KeyPress += onlyNumericInput;
            this.txtDonGia.KeyPress += onlyNumericInput;
        }
        private void onlyNumericInput(object sender, KeyPressEventArgs e)
        {
            UITextBox txtBox = sender as UITextBox;
            if (txtBox == txtSoLuongSanPham||txtBox== txtDonGia)
            {
                if (txtBox.Text.Length >= 15 && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void SetHinhAnh(List<Product> danhSachSanPham)
        {
            // Đường dẫn tương đối từ thư mục gốc của project
            string relativePath = @"..\..\..\Pic";
            string absolutePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath));
            int i = 0;
            foreach(Product sp in danhSachSanPham)
            {
                string imagePath = Path.Combine(absolutePath, Path.GetFileName(sp.ProductPic)).Replace("/", "\\");
                dtgvDanhSachSP.Rows[i].Cells["anh"].Value = Image.FromFile(imagePath);
                i++;
            }
        }       
        private void LoadDanhSachSanPham()
        {
            List<Product> danhSachSanPham = sanPhamBLL.GetAllProducts();
            bindingSource.DataSource = danhSachSanPham;
            dtgvDanhSachSP.DataSource = bindingSource;
            dtgvDanhSachSP.Columns["ProductPic"].Visible = false;
            dtgvDanhSachSP.Columns["Category"].Visible = false;
            dtgvDanhSachSP.Columns["Brand"].Visible = false;
            dtgvDanhSachSP.Columns["NhaCungCap"].Visible = false;
            dtgvDanhSachSP.Columns["ProductDescription"].Visible = false;
            dtgvDanhSachSP.Columns["BrandID"].Visible = false;
            dtgvDanhSachSP.Columns["CategoryID"].Visible = false;
            dtgvDanhSachSP.Columns["BaoHanh"].Visible = false;
            dtgvDanhSachSP.Columns["maNhaCungCap"].Visible = false;
            dtgvDanhSachSP.Columns["ProductDescription"].Visible = false;
            
            SetHinhAnh(danhSachSanPham);
        }
        private void frmDatHang_Load(object sender, EventArgs e)
        {
            LoadDanhSachSanPham();
            //Load dữ liệu danh sách loại sản phẩm
            cbLoaiSP.DataSource = loaiSanPhamBLL.GetAllCategories();
            cbLoaiSP.DisplayMember = "CategoryID";
            cbLoaiSP.ValueMember = "CategoryName";
            this.cbLoaiSP.SelectedValueChanged += cbLoaiSP_SelectedValueChanged;
            //Load dữ liệu danh sách nhà cung cấp
            cbNhaCungCap.DataSource = nhaCungCapBLL.getAllSuppliers();
            cbNhaCungCap.DisplayMember = "tenNhaCungCap";
            cbNhaCungCap.ValueMember = "maNhaCungCap";
            if (themPhieu)
            {
                txtMaPhieuDat.Text = phieuDatBLL.TaoMaPhieuDat();
            }
            else
            {
                txtMaPhieuDat.Text = maPhieuDat;
                cbNhaCungCap.SelectedValue = maNhaCungCap;
                chiTietPhieuDats.DataSource = chiTietPhieuDatBLL.LayChiTietPhieuDat(maPhieuDat);
                dtgvSanPhamTrongPhieuDat.DataSource = chiTietPhieuDats;
                dtgvSanPhamTrongPhieuDat.Columns["maPhieuDat"].Visible = false;
                dtgvSanPhamTrongPhieuDat.Columns["PhieuDat"].Visible = false;
                dtgvSanPhamTrongPhieuDat.Columns["SanPham"].Visible = false;
                PhieuDat phieuDat = phieuDatBLL.TimKiemPhieuDatTheoMaPhieuDat(maPhieuDat);
                string tongTien = phieuDat.TongTien.ToString();
                tongTien = tongTien.Split(',')[0].Trim();
                txtTongTien.Text = decimal.Parse(tongTien).ToString("C0");
            }
        }
        private void cbLoaiSP_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedValue = cbLoaiSP.SelectedValue.ToString();
            List<Product> sanPhams= sanPhamBLL.LocSanPhamTheoLoai(selectedValue);
            bindingSource.DataSource = sanPhams;
            SetHinhAnh(sanPhams);
        }
        private void btnHuyLoc_Click(object sender, EventArgs e)
        {
            List<Product> sanPhams = sanPhamBLL.GetAllProducts();
            bindingSource.DataSource = sanPhams;
            SetHinhAnh(sanPhams);
            txtTimKiem.Text = "Nhập mã hoặc tên sản phẩm để tìm kiếm";
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            var results = sanPhamBLL.SearchProducts(txtTimKiem.Text.Trim());
            if (results != null && results.Count > 0)
            {
                // Cập nhật DataGridView với kết quả tìm kiếm
                bindingSource.DataSource = results;
                SetHinhAnh(results);
            }
            else
            {
                // Nếu không tìm thấy kết quả, thông báo cho người dùng
                MessageBox.Show("Không tìm thấy sản phẩm nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }
        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "")
            {
                txtTimKiem.Text = "Nhập mã hoặc tên sản phẩm để tìm kiếm";
                txtTimKiem.ForeColor = Color.Silver;
                txtTimKiem.Font = new Font(txtTimKiem.Font, FontStyle.Italic);
                LoadDanhSachSanPham();
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
                btnTimKiem.PerformClick();

                // Ngăn chặn âm thanh bíp khi nhấn Enter
                e.Handled = true;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DongForm?.Invoke(true);
            this.Close();
        }
        private bool CheckChiTietPhieuDatSoLuongDat()
        {
            foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuDat.Rows)
            {
                int soLuongDat = int.Parse(row.Cells["soLuongDat"].Value.ToString());
                if (soLuongDat == 0)
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckChiTietPhieuDatDonGiaDat()
        {
            foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuDat.Rows)
            {
                decimal donGia = decimal.Parse(row.Cells["donGiaSP"].Value.ToString());
                if (donGia == 0)
                {
                    return false;
                }
            }
            return true;
        }
        private void LuuPhieuDat(int soLuongSanPham)
        {
            if (themPhieu)
            {
                //Tạo phiếu đặt
                PhieuDat pPhieuDat = new PhieuDat()
                {
                    MaPhieuDat = txtMaPhieuDat.Text,
                    MaNhaCungCap = cbNhaCungCap.SelectedValue.ToString(),
                    UserID = maNhanVien,
                    NgayLap = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    SoLuong = soLuongSanPham,
                    TongTien = decimal.Parse(txtTongTien.Text.Replace("₫", "").Replace(".", "").Trim()),
                    TrangThai = "Chưa duyệt"
                };
                bool result = phieuDatBLL.TaoPhieuDat(pPhieuDat);
                if (result)
                {
                    bool resultTaoCTPD = false;
                    foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuDat.Rows)
                    {
                        string donGia = row.Cells["donGiaSP"].Value.ToString().Replace("₫", "").Replace(".", "").Trim();
                        string tongTien = row.Cells["thanhTien"].Value.ToString().Replace("₫", "").Replace(".", "").Trim();
                        ChiTietPhieuDat pChiTietPhieuDat = new ChiTietPhieuDat()
                        {
                            MaPhieuDat = txtMaPhieuDat.Text,
                            ProductID = row.Cells["maSP"].Value.ToString(),
                            SoLuongDat = int.Parse(row.Cells["soLuongDat"].Value.ToString()),
                            SoLuongNhan = int.Parse(row.Cells["soLuongDaNhan"].Value.ToString()),
                            DonGia = decimal.Parse(donGia),
                            TongTien = decimal.Parse(tongTien)
                        };
                        if (pChiTietPhieuDat.SoLuongDat > 0)
                        {
                            resultTaoCTPD = chiTietPhieuDatBLL.ThemChiTietPhieuDat(pChiTietPhieuDat);
                            if (!resultTaoCTPD)
                            {
                                MessageBox.Show(this, "Tạo phiếu đặt thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                phieuDatBLL.XoaPhieuDat(pPhieuDat);
                                break;
                            }
                        }
                    }
                    if (resultTaoCTPD)
                    {
                        MessageBox.Show(this, "Tạo phiếu đặt thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Tạo phiếu đặt thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                themPhieu = false;
            }
            else
            {
                //Sửa phiếu đặt
                PhieuDat pPhieuDat = new PhieuDat()
                {
                    MaPhieuDat = txtMaPhieuDat.Text,
                    MaNhaCungCap = cbNhaCungCap.SelectedValue.ToString(),
                    UserID = maNhanVien,
                    NgayLap = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    SoLuong = soLuongSanPham,
                    TongTien = decimal.Parse(txtTongTien.Text.Replace("₫", "").Replace(".", "").Trim()),
                    TrangThai = "Chưa duyệt"
                };
                bool result = phieuDatBLL.SuaPhieuDat(pPhieuDat);
                if (result)
                {
                    bool resultSuaCTPD = false;
                    //Cập nhật chi tiết phiếu đặt
                    foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuDat.Rows)
                    {
                        string donGia = row.Cells["donGiaSP"].Value.ToString().Replace("₫", "").Replace(".", "").Trim();
                        string tongTien = row.Cells["thanhTien"].Value.ToString().Replace("₫", "").Replace(".", "").Trim();
                        ChiTietPhieuDat pChiTietPhieuDat = new ChiTietPhieuDat()
                        {
                            MaPhieuDat = txtMaPhieuDat.Text,
                            ProductID = row.Cells["maSP"].Value.ToString(),
                            SoLuongDat = int.Parse(row.Cells["soLuongDat"].Value.ToString()),
                            SoLuongNhan = int.Parse(row.Cells["soLuongDaNhan"].Value.ToString()),
                            DonGia = decimal.Parse(donGia),
                            TongTien = decimal.Parse(tongTien)
                        };
                        if (pChiTietPhieuDat.SoLuongDat > 0)
                        {
                            if (chiTietPhieuDatBLL.KiemTraTonTaiChiTietPhieuDat(pChiTietPhieuDat))
                            {
                                resultSuaCTPD = chiTietPhieuDatBLL.SuaChiTietPhieuDat(pChiTietPhieuDat);
                            }
                            else
                            {
                                resultSuaCTPD = chiTietPhieuDatBLL.ThemChiTietPhieuDat(pChiTietPhieuDat);
                            }
                        }
                        else
                        {
                            chiTietPhieuDatBLL.XoaChiTietPhieuDat(pChiTietPhieuDat);
                        }
                    }
                    //Xóa những sản phẩm khỏi chi tiết phiếu đặt
                    bool xoaSanPham = true;
                    if (chiTietPhieuDatBiXoa.Count > 0)
                    {
                        foreach (ChiTietPhieuDat ctpd in chiTietPhieuDatBiXoa)
                        {
                            string maPhieuDat = txtMaPhieuDat.Text;
                            string maSanPham = ctpd.ProductID;
                            xoaSanPham = chiTietPhieuDatBLL.XoaChiTietPhieuDat(maPhieuDat, maSanPham);
                            if (!xoaSanPham)
                            {
                                break;
                            }
                        }
                    }
                    if (resultSuaCTPD && xoaSanPham)
                    {
                        MessageBox.Show(this, "Sửa phiếu đặt thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(this, "Sửa phiếu đặt thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Sửa phiếu đặt thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dtgvSanPhamTrongPhieuDat.RowCount>0)
            {
                if (CheckChiTietPhieuDatDonGiaDat())
                {
                    if (!CheckChiTietPhieuDatSoLuongDat())
                    {
                        MessageBox.Show(this, "Có sản phẩm chưa có số lượng đặt. Vui lòng nhập số lượng đặt cho sản phẩm đấy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);                        
                    }
                    else
                    {
                        LuuPhieuDat(dtgvSanPhamTrongPhieuDat.RowCount);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Có sản phẩm chưa có đơn giá đặt. Vui lòng nhập đơn giá vào", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnThemSPVaoPhieuDat_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow selectedRow in dtgvDanhSachSP.SelectedRows)
            {
                string maSanPham = selectedRow.Cells["maSanPham"].Value.ToString();
                bool trungLap = false;
                foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuDat.Rows)
                {
                    if (row.Cells["maSP"].Value.Equals(maSanPham))
                    {
                        trungLap = true;
                        // Tăng số lượng nếu trùng lặp
                        int currentQuantity = Convert.ToInt32(row.Cells["soLuongDat"].Value);
                        row.Cells["soLuongDat"].Value = currentQuantity + 1;
                        break;
                    }
                }
                if (!trungLap)
                {                    
                    if (themPhieu)
                    {
                        object[] rowData = new object[6];
                        rowData[0] = selectedRow.Cells["maSanPham"].Value;
                        rowData[1] = selectedRow.Cells["tenSanPham"].Value;
                        rowData[2] = 0;
                        rowData[3] = 0;
                        rowData[4] = 0.ToString("C0");
                        rowData[5] = 0.ToString("C0");
                        dtgvSanPhamTrongPhieuDat.Rows.Add(rowData);
                    }
                    else
                    {
                        ChiTietPhieuDat pChiTietPhieuDat = new ChiTietPhieuDat()
                        {
                            MaPhieuDat = maPhieuDat,
                            ProductID = selectedRow.Cells["maSanPham"].Value.ToString(),
                            tenSanPham = selectedRow.Cells["tenSanPham"].Value.ToString(),
                            SoLuongDat = 0,
                            SoLuongNhan = 0,
                            DonGia = 0,
                            TongTien = 0,
                        };
                        chiTietPhieuDats.Add(pChiTietPhieuDat);
                    }
                }
            }
            dtgvDanhSachSP.ClearSelection();
        }
        private void btnXoaSPTrongPhieuDat_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuDat.SelectedRows)
            {
                    if (themPhieu)
                    {
                        deletedRows.Add(row);
                        dtgvSanPhamTrongPhieuDat.Rows.Remove(row);
                    }
                    else
                    {
                        string donGia = row.Cells["donGiaSP"].Value.ToString().Replace("₫", "").Replace(".", "").Trim();
                        string tongTien = row.Cells["thanhTien"].Value.ToString().Replace("₫", "").Replace(".", "").Trim();
                        ChiTietPhieuDat chiTietPhieuDatXoa = new ChiTietPhieuDat()
                        {
                            MaPhieuDat = maPhieuDat,
                            ProductID = row.Cells["maSP"].Value.ToString(),
                            tenSanPham = row.Cells["tenSP"].Value.ToString(),
                            SoLuongDat = int.Parse(row.Cells["soLuongDat"].Value.ToString()),
                            SoLuongNhan = int.Parse(row.Cells["soLuongDaNhan"].Value.ToString()),
                            DonGia = decimal.Parse(donGia),
                            TongTien = decimal.Parse(tongTien),
                        };
                        chiTietPhieuDatBiXoa.Add(chiTietPhieuDatXoa);
                        dtgvSanPhamTrongPhieuDat.Rows.Remove(row);
                    }
            }
            TinhTongTien();
        }
        private void dtgvSanPhamTrongPhieuDat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0)
            {
                txtMaSanPham.Text = dtgvSanPhamTrongPhieuDat.Rows[e.RowIndex].Cells["maSP"].Value.ToString();
                txtTenSanPham.Text = dtgvSanPhamTrongPhieuDat.Rows[e.RowIndex].Cells["tenSP"].Value.ToString();
                txtSoLuongSanPham.Text = dtgvSanPhamTrongPhieuDat.Rows[e.RowIndex].Cells["soLuongDat"].Value.ToString();                
                string donGia = dtgvSanPhamTrongPhieuDat.Rows[e.RowIndex].Cells["donGiaSP"].Value.ToString();
                donGia = donGia.Replace("₫", "").Replace(".", "").Split(',')[0].Trim();
                txtDonGia.Text = donGia;
            }
        }
        private void btnPhucHoi_Click(object sender, EventArgs e)
        {
            if (themPhieu)
            {
                foreach (DataGridViewRow row in deletedRows)
                {
                    dtgvSanPhamTrongPhieuDat.Rows.Add(row);                                      
                }
                deletedRows.Clear();
            }
            else
            {
                foreach(ChiTietPhieuDat chiTietPhieuDat in chiTietPhieuDatBiXoa)
                {
                    chiTietPhieuDats.Add(chiTietPhieuDat);
                }
                chiTietPhieuDatBiXoa.Clear();
            }
            TinhTongTien();
        }
        private void txtSoLuongSanPham_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoLuongSanPham.Text))
            {
                txtSoLuongSanPham.Text = "0";
                txtSoLuongSanPham.SelectionStart = txtSoLuongSanPham.Text.Length;
            }
            if (txtSoLuongSanPham.Text.StartsWith("0") && txtSoLuongSanPham.Text.Length > 1)
            {
                // Xóa số "0" ở đầu
                txtSoLuongSanPham.Text = txtSoLuongSanPham.Text.TrimStart('0');
                txtSoLuongSanPham.SelectionStart = txtSoLuongSanPham.Text.Length; // Đặt con trỏ ở cuối
            }
            if (dtgvSanPhamTrongPhieuDat.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuDat.SelectedRows)
                {
                    int soLuongDat = int.Parse(txtSoLuongSanPham.Text.ToString());
                    row.Cells["soLuongDat"].Value = soLuongDat;
                    
                }
            }
        }
        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDonGia.Text))
            {
                txtDonGia.Text = "0";
                txtDonGia.SelectionStart = txtDonGia.Text.Length;
            }
            if (txtDonGia.Text.StartsWith("0") && txtDonGia.Text.Length > 1)
            {
                // Xóa số "0" ở đầu
                txtDonGia.Text = txtDonGia.Text.TrimStart('0');
                txtDonGia.SelectionStart = txtDonGia.Text.Length; // Đặt con trỏ ở cuối
            }
            // Lưu vị trí con trỏ hiện tại
            int cursorPosition = txtDonGia.SelectionStart;

            // Loại bỏ dấu chấm (nếu có) để xử lý lại
            string input = txtDonGia.Text.Replace(".", "");

            // Định dạng lại chuỗi với dấu chấm
            string formatted = FormatLuong(input);

            // Cập nhật lại giá trị vào textbox
            txtDonGia.Text = formatted;

            // Đặt lại vị trí con trỏ vào cuối
            txtDonGia.SelectionStart = cursorPosition + 1;
            if (dtgvSanPhamTrongPhieuDat.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuDat.SelectedRows)
                {
                    decimal donGia = decimal.Parse(txtDonGia.Text.ToString());
                    row.Cells["donGiaSP"].Value = donGia;
                }
            }
        }
        private void TinhTongTien()
        {
            decimal tongTien = 0;
            foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuDat.Rows)
            {
                string price = row.Cells["thanhTien"].Value.ToString();
                price = price.Replace("₫", "").Replace(".", "").Split(',')[0].Trim();
                decimal thanhTien = decimal.Parse(price);
                tongTien += thanhTien;
            }
            txtTongTien.Text = tongTien.ToString("C0");
        }
        private void dtgvSanPhamTrongPhieuDat_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        { 
            string columnName = dtgvSanPhamTrongPhieuDat.Columns[e.ColumnIndex].Name;
            if (columnName == "soLuongDat" || columnName == "donGiaSP")
            {                
                foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuDat.SelectedRows)
                {
                    string price = row.Cells["donGiaSP"].Value.ToString();
                    price = price.Replace("₫", "").Replace(".", "").Split(',')[0].Trim();
                    decimal donGia = decimal.Parse(price);
                    int soLuongDat = int.Parse(row.Cells["soLuongDat"].Value.ToString());
                    decimal thanhTien = soLuongDat * donGia;
                    row.Cells["thanhTien"].Value = thanhTien;
                }
            }
            if (columnName == "thanhTien")
            {
                TinhTongTien();
            }
        }
        private void dtgvSanPhamTrongPhieuDat_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!themPhieu)
            {
                if ((dtgvSanPhamTrongPhieuDat.Columns[e.ColumnIndex].Name == "donGiaSP" && e.Value != null) || (dtgvSanPhamTrongPhieuDat.Columns[e.ColumnIndex].Name == "thanhTien" && e.Value != null))
                {
                    e.Value = ((decimal)e.Value).ToString("C0");
                    e.FormattingApplied = true;
                }
            }            
        }
        private string FormatLuong(string input)
        {
            // Loại bỏ tất cả dấu chấm hiện có
            input = input.Replace(".", "");

            // Nếu không phải là số thì trả về input ban đầu
            if (!long.TryParse(input, out long luong))
                return input;

            // Chia thành các nhóm ba chữ số và thêm dấu chấm vào giữa
            string formatted = "";
            int count = 0;

            // Duyệt ngược từ cuối chuỗi và chèn dấu chấm sau mỗi 3 chữ số
            for (int i = input.Length - 1; i >= 0; i--)
            {
                formatted = input[i] + formatted;
                count++;

                // Thêm dấu chấm sau mỗi ba chữ số nếu không phải là ký tự đầu tiên
                if (count % 3 == 0 && i > 0)
                {
                    formatted = "." + formatted;
                }
            }
            return formatted;
        }

    }
}