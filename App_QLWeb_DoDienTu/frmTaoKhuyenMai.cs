using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_QLWeb_DoDienTu
{
    public partial class frmTaoKhuyenMai : Form
    {
        private frmAdmin parentfrm;
        private string MaKM;
        KhuyenMaiBLL kmbll = new KhuyenMaiBLL();
        KhuyenMaiSanPhamBLL kmspbll = new KhuyenMaiSanPhamBLL();
        public frmTaoKhuyenMai(frmAdmin parentfrm,string MaKM)
        {
            InitializeComponent();
            this.parentfrm = parentfrm;
            this.MaKM = MaKM;
            this.Load += FrmTaoKhuyenMai_Load;
            this.dgvSanPhamKhuyenMai.CellFormatting += DgvSanPhamKhuyenMai_CellFormatting;
            this.btnBack.Click += BtnBack_Click;
            this.dtpTGBatDau.Value = dtpTGKetThuc.Value;
            this.dtpTGBatDau.ValueChanged += DtpTGBatDau_ValueChanged;
            this.dtpTGKetThuc.ValueChanged += DtpTGKetThuc_ValueChanged;
            this.btnXacNhan.Click += BtnXacNhan_Click;
            this.btnThemSanPham.Click += BtnThemSanPham_Click;
            this.btnHuySua.Click += BtnHuySua_Click; ;
            this.btnDungKM.Click += BtnDungKM_Click;
        }
        private void BtnHuySua_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc việc hủy sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                KhuyenMai khuyenMai = kmbll.LayTTKhuyenMaiTuMaKM(MaKM);
                txtTenKM.Text = khuyenMai.tenKhuyenMai;
                txtMoTa.Text = khuyenMai.moTa;
                dtpTGBatDau.Value = khuyenMai.ngayBatDau;
                dtpTGKetThuc.Value = khuyenMai.ngayKetThuc;
                btnHuySua.Enabled = false;
                txtTenKM.Enabled = false;
                txtMoTa.Enabled = false;
                dtpTGBatDau.Enabled = false;
                dtpTGKetThuc.Enabled = false;
                btnXacNhan.Text = "Sửa";
            }
        }
        private void BtnDungKM_Click(object sender, EventArgs e)
        {
            KhuyenMai khuyenMai = kmbll.LayTTKhuyenMaiTuMaKM(MaKM);
            var result = MessageBox.Show("Bạn có chắc muốn dừng chương trình khuyến mãi không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                if (khuyenMai.trangThai != "Đã kết thúc")
                {
                    khuyenMai.trangThai = "Đã kết thúc";
                    if (kmbll.CapNhatKhuyenMai(khuyenMai))
                    {
                        kmspbll.CapNhatTrangThaiSanPhamTrongKhuyenMai(MaKM, "Hết hiệu lực");
                        MessageBox.Show("Đã dừng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDungKM.Enabled = false;
                        btnThemSanPham.Enabled = false;
                        btnXacNhan.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Dừng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void DgvSanPhamKhuyenMai_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal so))
            {
                dgvSanPhamKhuyenMai.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (so % 1 == 0) // Nếu là số nguyên
                {
                    e.Value = ((int)so).ToString("N0").Replace(",", "."); // Chuyển thành số nguyên
                }
                else
                {
                    e.Value = so.ToString("N2").Replace(",", "."); // Giữ nguyên với 2 chữ số thập phân
                }
                e.FormattingApplied = true;
            }
        }
        private void FrmTaoKhuyenMai_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(MaKM))
            {
                txtTenKM.Enabled = false;
                txtMoTa.Enabled = false;
                dtpTGBatDau.Enabled = false;
                dtpTGKetThuc.Enabled = false;
                KhuyenMai khuyenMai = kmbll.LayTTKhuyenMaiTuMaKM(MaKM);
                txtMoTa.Text = khuyenMai.moTa;
                txtTenKM.Text = khuyenMai.tenKhuyenMai;
                dtpTGBatDau.Value = khuyenMai.ngayBatDau;
                dtpTGKetThuc.Value = khuyenMai.ngayKetThuc;
                btnXacNhan.Text = "Sửa";
                if (khuyenMai.trangThai == "Đã kết thúc")
                {
                    btnDungKM.Enabled = false;
                    btnThemSanPham.Enabled = false;
                    btnXacNhan.Enabled = false;
                }
                else if (khuyenMai.trangThai == "Đang diễn ra")
                {
                    btnThemSanPham.Enabled = false;
                    btnXacNhan.Enabled = false;
                    btnDungKM.Enabled = true;
                }                
                else
                {
                    btnThemSanPham.Enabled = true;
                    btnXacNhan.Enabled = true;
                    btnDungKM.Enabled = true;
                }
            }
            else
            {
                txtTenKM.Enabled = true;
                txtMoTa.Enabled = true;
                dtpTGBatDau.Enabled = true;
                dtpTGKetThuc.Enabled = true;
                btnXacNhan.Text = "Xác nhận";
                dtpTGBatDau.MinDate = DateTime.Now;
                dtpTGKetThuc.MinDate = DateTime.Now;
            }
            List<KhuyenMaiSanPham> danhSachSanPham = kmspbll.LoadSanPhamTheoKhuyenMai(MaKM);
            SettingDgv(danhSachSanPham);
        }
        private void SettingDgv(List<KhuyenMaiSanPham> danhSachSanPham)
        {
            dgvSanPhamKhuyenMai.DataSource = danhSachSanPham;
            dgvSanPhamKhuyenMai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvSanPhamKhuyenMai.Columns["maKhuyenMai"] != null )
            {
                dgvSanPhamKhuyenMai.Columns["maKhuyenMai"].Visible = false;
            }
            if (dgvSanPhamKhuyenMai.Columns["Product"] != null)
            {
                dgvSanPhamKhuyenMai.Columns["Product"].Visible = false;
            }
            if (dgvSanPhamKhuyenMai.Columns["KhuyenMai"] != null)
            {
                dgvSanPhamKhuyenMai.Columns["KhuyenMai"].Visible = false;
            }
            if (dgvSanPhamKhuyenMai.Columns["trangThai"] != null)
            {
                dgvSanPhamKhuyenMai.Columns["trangThai"].Visible = false;
            }
            if (dgvSanPhamKhuyenMai.Columns["maSanPham"] != null)
            {
                dgvSanPhamKhuyenMai.Columns["maSanPham"].HeaderText = "Mã sản phẩm";
                dgvSanPhamKhuyenMai.Columns["maSanPham"].DisplayIndex = 0;
                dgvSanPhamKhuyenMai.Columns["maSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            if (dgvSanPhamKhuyenMai.Columns["tenSanPham"] != null)
            {
                dgvSanPhamKhuyenMai.Columns["tenSanPham"].HeaderText = "Tên sản phẩm";
                dgvSanPhamKhuyenMai.Columns["tenSanPham"].DisplayIndex = 1;
            }
            if (dgvSanPhamKhuyenMai.Columns["phanTramGiam"] != null)
            {
                dgvSanPhamKhuyenMai.Columns["phanTramGiam"].HeaderText = "Phần trăm giảm";
                dgvSanPhamKhuyenMai.Columns["phanTramGiam"].DisplayIndex = 2;
                dgvSanPhamKhuyenMai.Columns["phanTramGiam"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }            
        }
        private void DtpTGKetThuc_ValueChanged(object sender, EventArgs e)
        {
            if (dtpTGBatDau.Value > dtpTGKetThuc.Value)
            {
                dtpTGBatDau.Value = dtpTGKetThuc.Value;
            }
        }
        private void DtpTGBatDau_ValueChanged(object sender, EventArgs e)
        {
            if(dtpTGBatDau.Value > dtpTGKetThuc.Value)
            {
                dtpTGKetThuc.Value = dtpTGBatDau.Value;
            }
        }
        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            if(btnXacNhan.Text == "Xác nhận")
            {
                if (!string.IsNullOrWhiteSpace(txtTenKM.Text.Trim()))
                {
                    string maKM = kmbll.TaoMaKhuyenMaiTuDong();
                    string tenKM = txtTenKM.Text.Trim();
                    DateTime ngayBatDau = dtpTGBatDau.Value;
                    DateTime ngayKetThuc = dtpTGKetThuc.Value;
                    string moTa = txtMoTa.Text.Trim();                    
                    if (string.IsNullOrEmpty(MaKM))
                    {
                        KhuyenMai khuyenMai = new KhuyenMai
                        {
                            maKhuyenMai = maKM,
                            tenKhuyenMai = tenKM,
                            moTa = moTa,
                            ngayBatDau = ngayBatDau,
                            ngayKetThuc = ngayKetThuc
                        };
                        if (kmbll.ThemChuongTrinhKhuyenMai(khuyenMai))
                        {
                            MessageBox.Show("Tạo chương trình khuyến mãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtTenKM.Enabled = false;
                            txtMoTa.Enabled = false;
                            dtpTGBatDau.Enabled = false;
                            dtpTGKetThuc.Enabled = false;
                            btnXacNhan.Text = "Sửa";
                            btnDungKM.Enabled = true;
                            this.MaKM = khuyenMai.maKhuyenMai;
                        }
                        else
                        {
                            MessageBox.Show("Tạo chương trình khuyến mãi thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        if (!kmbll.KiemTraThoiGianKhuyenMaiTruocKhiSua(MaKM, ngayBatDau, ngayKetThuc))
                        {
                            MessageBox.Show("Thời gian khuyến mãi không được trùng với khuyến mãi khác chứa sản phẩm này.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        KhuyenMai khuyenMai = new KhuyenMai
                        {
                            maKhuyenMai = MaKM,
                            tenKhuyenMai = tenKM,
                            moTa = moTa,
                            ngayBatDau = ngayBatDau,
                            ngayKetThuc = ngayKetThuc
                        };
                        if (kmbll.CapNhatKhuyenMai(khuyenMai))
                        {
                            MessageBox.Show("Cập nhật chương trình khuyến mãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtTenKM.Enabled = false;
                            txtMoTa.Enabled = false;
                            dtpTGBatDau.Enabled = false;
                            dtpTGKetThuc.Enabled = false;
                            btnHuySua.Enabled = false;
                            btnXacNhan.Text = "Sửa";
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật chương trình khuyến mãi thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tên khuyến mãi không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if(btnXacNhan.Text == "Sửa")
            {
                txtTenKM.Enabled = true;
                txtMoTa.Enabled = true;
                dtpTGBatDau.Enabled = true;
                dtpTGKetThuc.Enabled = true;
                btnXacNhan.Text = "Xác nhận";
                btnHuySua.Enabled = true;
            }
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            frmQLKhuyenMai frm = new frmQLKhuyenMai(parentfrm);
            parentfrm.OpenChildForm(frm);
        }
        private void BtnThemSanPham_Click(object sender, EventArgs e)
        {
            if(txtTenKM.Enabled == false)
            {
                frmThemSanPhamKhuyenMai frm = new frmThemSanPhamKhuyenMai(parentfrm, MaKM);
                parentfrm.OpenChildForm(frm);
            }
            else
            {
                MessageBox.Show("Hãy tạo chương trình khuyến mãi!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
