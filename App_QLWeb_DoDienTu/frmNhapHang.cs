using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using NPOI.SS.Formula.Functions;
using Sunny.UI;
using Sunny.UI.Win32;
namespace App_QLWeb_DoDienTu
{
    public partial class frmNhapHang : Form
    {
        private string maPhieuNhap;
        private bool themLanCuoi;
        private string maNhanVien;
        private PhieuDat phieuDat;
        private PhieuDatBLL phieuDatBLL;
        private PhieuNhapBLL phieuNhapBLL;
        private ChiTietPhieuNhapBLL chiTietPhieuNhapBLL;
        private ChiTietPhieuDatBLL chiTietPhieuDatBLL;
        public delegate void SendDataHandler(bool loadData);
        public event SendDataHandler DongForm;
        public frmNhapHang(string maNhanVien)
        {
            this.phieuNhapBLL = new PhieuNhapBLL();
            this.phieuDatBLL = new PhieuDatBLL();
            this.chiTietPhieuNhapBLL = new ChiTietPhieuNhapBLL();
            this.chiTietPhieuDatBLL = new ChiTietPhieuDatBLL();
            InitializeComponent();
            this.maPhieuNhap = phieuNhapBLL.TaoMaPhieuNhap();
            this.maNhanVien = maNhanVien;
            this.Load += FrmNhapHang_Load;
        }
        private void FrmNhapHang_Load(object sender, EventArgs e)
        {
            txtMaPhieuNhap.Text = maPhieuNhap;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DongForm?.Invoke(true);
            this.Close();
        }
        private void ClearTextBox()
        {
            txtMaSanPham.Text = string.Empty;
            txtTenSanPham.Text = string.Empty;
            txtDonGia.Text = string.Empty;
            txtSoLuongSanPham.Text = null;            
        }
        private void btnChonPhieuDat_Click(object sender, EventArgs e)
        {
            frmDanhSachPhieuDat frmDanhSachPhieuDat = new frmDanhSachPhieuDat();
            frmDanhSachPhieuDat.DataSent += FormDanhSachPhieuDat_SendData;
            frmDanhSachPhieuDat.ShowDialog();
        }
        private void FormDanhSachPhieuDat_SendData(string maPhieuDat)
        {
            ClearTextBox();
            phieuDat = phieuDatBLL.TimKiemPhieuDatTheoMaPhieuDat(maPhieuDat);
            List<PhieuNhap> danhSachPhieuNhap = phieuNhapBLL.TimKiemPhieuNhapTheoMaPhieuDat(maPhieuDat);
            txtMaPhieuDat.Text = phieuDat.MaPhieuDat;
            txtNhaCungCap.Text = phieuDat.NhaCungCap.tenNhaCungCap;
            txtLanNhap.Text = (danhSachPhieuNhap.Count>0) ? (danhSachPhieuNhap.Count+1).ToString() : "1";
            themLanCuoi = (int.Parse(txtLanNhap.Text.ToString()) == 3) ? true : false;
            List<ChiTietPhieuDat> danhSachSanPhamDat = new List<ChiTietPhieuDat>();
            dtgvSanPhamTrongPhieuNhap.ClearRows();

            danhSachSanPhamDat = chiTietPhieuDatBLL.LayChiTietPhieuDat(maPhieuDat);
            if (txtLanNhap.Text!="3")
            {
                txtSoLuongSanPham.Enabled = true;
                foreach (ChiTietPhieuDat chiTietPhieuDat in danhSachSanPhamDat)
                {
                    if (chiTietPhieuDat.SoLuongDat != chiTietPhieuDat.SoLuongNhan)
                    {
                        object[] row = new object[dtgvSanPhamTrongPhieuNhap.ColumnCount];
                        row[0] = chiTietPhieuDat.ProductID;
                        row[1] = chiTietPhieuDat.tenSanPham;
                        row[2] = chiTietPhieuDat.SoLuongDat - chiTietPhieuDat.SoLuongNhan;
                        row[3] = 0;
                        row[4] = chiTietPhieuDat.DonGia;
                        row[5] = 0;
                        dtgvSanPhamTrongPhieuNhap.Rows.Add(row);
                    }
                }
            }
            else
            {
                txtSoLuongSanPham.Enabled = false;
                foreach (ChiTietPhieuDat chiTietPhieuDat in danhSachSanPhamDat)
                {
                    if (chiTietPhieuDat.SoLuongDat != chiTietPhieuDat.SoLuongNhan)
                    {
                        object[] row = new object[dtgvSanPhamTrongPhieuNhap.ColumnCount];
                        row[0] = chiTietPhieuDat.ProductID;
                        row[1] = chiTietPhieuDat.tenSanPham;
                        row[2] = chiTietPhieuDat.SoLuongDat - chiTietPhieuDat.SoLuongNhan;
                        row[3] = chiTietPhieuDat.SoLuongDat - chiTietPhieuDat.SoLuongNhan;
                        row[4] = chiTietPhieuDat.DonGia;
                        row[5] = (chiTietPhieuDat.SoLuongDat - chiTietPhieuDat.SoLuongNhan) * chiTietPhieuDat.DonGia;
                        dtgvSanPhamTrongPhieuNhap.Rows.Add(row);
                    }
                }
                TinhTongTien();
            }
        }
        private void dtgvSanPhamTrongPhieuDat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                txtMaSanPham.Text = dtgvSanPhamTrongPhieuNhap.Rows[e.RowIndex].Cells["maSP"].Value.ToString();
                txtTenSanPham.Text = dtgvSanPhamTrongPhieuNhap.Rows[e.RowIndex].Cells["tenSP"].Value.ToString();                
                int price = int.Parse(dtgvSanPhamTrongPhieuNhap.Rows[e.RowIndex].Cells["donGiaSP"].Value.ToString().Split(',')[0]);
                txtDonGia.Text = price.ToString("C0");
                txtSoLuongSanPham.Text = dtgvSanPhamTrongPhieuNhap.Rows[e.RowIndex].Cells["soLuongDaNhan"].Value.ToString();                
            }
        }
        private void txtSoLuongSanPham_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
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
            if (dtgvSanPhamTrongPhieuNhap.SelectedRows.Count > 0)
            {
                int soLuongNhap = int.Parse(txtSoLuongSanPham.Text);
                int soLuongConLai = int.Parse(dtgvSanPhamTrongPhieuNhap.SelectedRows[0].Cells["soLuongConLai"].Value.ToString());
                if (soLuongNhap > soLuongConLai)
                {
                    MessageBox.Show($"Số lượng nhập không được lớn hơn số lượng hàng chưa nhận ({soLuongConLai}).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoLuongSanPham.Text = soLuongConLai.ToString();
                    // Đưa con trỏ về cuối TextBox
                    txtSoLuongSanPham.SelectionStart = txtSoLuongSanPham.Text.Length;
                }
                foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuNhap.SelectedRows)
                {
                    int soLuongDat = int.Parse(txtSoLuongSanPham.Text.ToString());
                    row.Cells["soLuongDaNhan"].Value = soLuongDat;
                }
            }
        }
        private void dtgvSanPhamTrongPhieuDat_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = dtgvSanPhamTrongPhieuNhap.Columns[e.ColumnIndex].Name;
            if (columnName == "soLuongDaNhan")
            {
                foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuNhap.SelectedRows)
                {
                    string price = row.Cells["donGiaSP"].Value.ToString();
                    price = price.Split(',')[0].Trim();
                    decimal donGia = decimal.Parse(price);
                    int soLuongDat = int.Parse(row.Cells["soLuongDaNhan"].Value.ToString());
                    decimal thanhTien = soLuongDat * donGia;
                    row.Cells["thanhTien"].Value = thanhTien;
                }
            }
            if (columnName == "thanhTien")
            {
                TinhTongTien();
            }
        }
        private void TinhTongTien()
        {
            decimal tongTien = 0;
            foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuNhap.Rows)
            {
                string price = row.Cells["thanhTien"].Value.ToString();
                price = price.Split(',')[0].Trim();
                decimal thanhTien = decimal.Parse(price);
                tongTien += thanhTien;
            }
            txtTongTien.Text = tongTien.ToString("C0");
        }
        private bool KiemTraNgaySanXuat_HanSuDung()
        {
            foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuNhap.Rows)
            {
                if (row.Cells["ngaySanXuat"].Value == null || row.Cells["hanSuDung"].Value == null)
                {
                    return false;
                }
            }
            return true;
        }
        private void ThemPhieuNhap()
        {
            decimal tongTien = decimal.Parse(txtTongTien.Text.Replace("₫", "").Replace(".", "").Split(',')[0].Trim());
            PhieuNhap phieu = new PhieuNhap()
            {
                MaPhieuNhap = maPhieuNhap,
                MaPhieuDat = txtMaPhieuDat.Text,
                UserID = maNhanVien,
                NgayNhap = DateTime.Now,
                SoLan = int.Parse(txtLanNhap.Text),
                TongTien = tongTien,
            };
            bool result = phieuNhapBLL.TaoPhieuNhap(phieu);
            if (result)
            {
                if (KiemTraNgaySanXuat_HanSuDung())
                {
                    foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuNhap.Rows)
                    {
                        int soLuongNhan = int.Parse(row.Cells["soLuongDaNhan"].Value.ToString());
                        decimal donGia = decimal.Parse(row.Cells["donGiaSP"].Value.ToString().Split(',')[0].Trim());
                        decimal thanhTien = decimal.Parse(row.Cells["thanhTien"].Value.ToString().Split(',')[0].Trim());
                        string ngaySanXuat = row.Cells["ngaySanXuat"].Value.ToString();
                        string hanSuDung = row.Cells["hanSuDung"].Value.ToString();
                        if (soLuongNhan > 0)
                        {
                            ChiTietPhieuNhap chiTietPhieuNhap = new ChiTietPhieuNhap()
                            {
                                MaPhieuNhap = maPhieuNhap,
                                MaPhieuDat = txtMaPhieuDat.Text,
                                ProductID = row.Cells["maSP"].Value.ToString(),
                                SoLuong = soLuongNhan,
                                DonGia = donGia,
                                NgaySanXuat = DateTime.Parse(ngaySanXuat),
                                HanSuDung = DateTime.Parse(hanSuDung),
                                TongTien = thanhTien,
                            };
                            chiTietPhieuNhapBLL.TaoChiTietPhieuNhap(chiTietPhieuNhap);
                        }
                    }
                    MessageBox.Show(this, "Tạo phiếu nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult r = MessageBox.Show(this, "Bạn có muốn in phiếu nhập ra không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        frmPhieuNhapHang frmPhieuNhapHang = new frmPhieuNhapHang(maPhieuNhap);
                        frmPhieuNhapHang.DongForm += FrmPhieuNhapHang_DongForm;
                        frmPhieuNhapHang.ShowDialog();
                    }
                    else
                    {
                        DongForm?.Invoke(true);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(this, "Tạo phiếu nhập lỗi do có 1 số sản phẩm chưa có ngày sản xuất hoặc hạn sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    phieuNhapBLL.XoaPhieuNhap(phieu.MaPhieuNhap, phieu.SoLan);
                }
            }
            else
            {
                MessageBox.Show(this, "Tạo phiếu nhập thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FrmPhieuNhapHang_DongForm(bool loadData)
        {
            if (loadData)
            {
                DongForm?.Invoke(true);
                this.Close();
            }
        }
        private bool CheckSoLuongDat()
        {
            int soLuongSanPham = dtgvSanPhamTrongPhieuNhap.RowCount;
            int dem = 0;
            foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuNhap.Rows)
            {
                int soLuongNhan = int.Parse(row.Cells["soLuongDaNhan"].Value.ToString());                
                if (soLuongNhan == 0)
                {
                    dem++;
                }
            }
            if (dem==soLuongSanPham)
            {
                return false;
            }
            return true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dtgvSanPhamTrongPhieuNhap.Rows.Count>0)
            {
                if (CheckSoLuongDat())
                {
                    if (!themLanCuoi)
                    {
                        ThemPhieuNhap();
                    }
                    else
                    {
                        bool chuaNhanDu = false;
                        foreach (DataGridViewRow row in dtgvSanPhamTrongPhieuNhap.Rows)
                        {
                            int soLuongConLai = int.Parse(row.Cells["soLuongConLai"].Value.ToString());
                            int soLuongDaNhan = int.Parse(row.Cells["soLuongDaNhan"].Value.ToString());
                            if (soLuongConLai != soLuongDaNhan)
                            {
                                chuaNhanDu = true;
                                break;
                            }
                        }
                        if (chuaNhanDu)
                        {
                            DialogResult r = MessageBox.Show(this, $"Đây là lần cuối để tạo phiếu nhập của phiếu đặt ({txtMaPhieuDat.Text})" +
                                $" này nhưng số lượng sản phẩm nhận vào chưa đủ so với số lượng đặt, " +
                                $"bạn có chắc chắn vẫn muốn tạo phiếu nhập này không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (r == DialogResult.Yes)
                            {
                                ThemPhieuNhap();
                            }
                        }
                        else
                        {
                            ThemPhieuNhap();
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "Lỗi số lượng nhận của tất cả sản phẩm bằng 0 hoặc không có ngày sản xuất, hạn sử dụng. Không thể tạo phiếu nhập", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }        
    }
}
