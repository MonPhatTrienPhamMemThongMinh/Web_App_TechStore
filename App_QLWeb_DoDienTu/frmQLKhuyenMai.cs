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
    public partial class frmQLKhuyenMai : Form
    {
        private frmAdmin parentfrm;
        KhuyenMaiBLL kmbll = new KhuyenMaiBLL();
        private string MaKM;
        public frmQLKhuyenMai(frmAdmin parentfrm)
        {
            InitializeComponent();

            cboTrangThai.SelectedIndex = 0;
            this.dtpNgayKetThuc.MaxDate = DateTime.Now;
            this.dtpNgayBatDau.MaxDate = this.dtpNgayKetThuc.MaxDate;
            this.dtpNgayBatDau.ValueChanged += DtpNgayBatDau_ValueChanged;
            this.dtpNgayKetThuc.ValueChanged += DtpNgayKetThuc_ValueChanged;
            
            this.parentfrm = parentfrm;
            this.Load += FrmQLKhuyenMai_Load;
            this.btnBack.Click += BtnBack_Click;

            this.btnTaoKhuyenMai.Click += BtnTaoKhuyenMai_Click;
            this.btnXemKhuyenMai.Click += BtnXemKhuyenMai_Click;
            this.btnReset.Click += BtnReset_Click;
            this.dgvKhuyenMai.SelectionChanged += DgvKhuyenMai_SelectionChanged;

            this.btnXacNhan.Click += BtnXacNhan_Click;
            this.btnHuyLoc.Click += BtnHuyLoc_Click;
        }

        private void DtpNgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            if(dtpNgayBatDau.Value > dtpNgayKetThuc.Value)
            {
                dtpNgayKetThuc.Value = dtpNgayBatDau.Value;
            }
        }

        private void DtpNgayKetThuc_ValueChanged(object sender, EventArgs e)
        {
            if (dtpNgayKetThuc.Value < dtpNgayBatDau.Value)
            {
                dtpNgayBatDau.Value = dtpNgayKetThuc.Value;
            }
        }

        private void BtnHuyLoc_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc là muốn hủy lọc?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(result == DialogResult.Yes)
            {
                List<KhuyenMai> khuyenMais = kmbll.LoadDanhSachKhuyenMai();
                SettingDgv(khuyenMais);
                this.btnHuyLoc.Enabled = false;
                cboTrangThai.SelectedIndex = 0;
            }
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            DateTime tgBatDau = dtpNgayBatDau.Value;
            DateTime tgKetThuc = dtpNgayKetThuc.Value;
            string trangThai = cboTrangThai.SelectedItem.ToString();
            List<KhuyenMai> dsKhuyenMaiLoc = kmbll.LocDanhSachKhuyenMai(tgBatDau, tgKetThuc, trangThai);
            SettingDgv(dsKhuyenMaiLoc);
            this.btnHuyLoc.Enabled = true;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            kmbll.UpdateTrangThaiKhuyenMai();
            LoadDanhSachKhuyenMai();
        }

        private void DgvKhuyenMai_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvKhuyenMai.SelectedRows.Count > 0)
            {
                string maKm = dgvKhuyenMai.SelectedRows[0].Cells["maKhuyenMai"].Value.ToString();
                this.MaKM = maKm;
            }
        }

        private void BtnXemKhuyenMai_Click(object sender, EventArgs e)
        {
            frmTaoKhuyenMai frm = new frmTaoKhuyenMai(parentfrm,MaKM);
            parentfrm.OpenChildForm(frm);
        }

        private void FrmQLKhuyenMai_Load(object sender, EventArgs e)
        {
            LoadDanhSachKhuyenMai();
        }

        private void LoadDanhSachKhuyenMai()
        {
            List<KhuyenMai> khuyenMais = kmbll.LoadDanhSachKhuyenMai();
            SettingDgv(khuyenMais);
        }

        private void SettingDgv(List<KhuyenMai> khuyenMais)
        {
            dgvKhuyenMai.DataSource = khuyenMais;
            dgvKhuyenMai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvKhuyenMai.Columns["ngayBatDau"] != null)
            {
                dgvKhuyenMai.Columns["ngayBatDau"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                dgvKhuyenMai.Columns["ngayBatDau"].HeaderText = "Thời gian bắt đầu";
            }
            if (dgvKhuyenMai.Columns["ngayKetThuc"] != null)
            {
                dgvKhuyenMai.Columns["ngayKetThuc"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                dgvKhuyenMai.Columns["ngayKetThuc"].HeaderText = "Thời gian kết thúc";
            }

            if (dgvKhuyenMai.Columns["maKhuyenMai"] != null)
            {
                dgvKhuyenMai.Columns["maKhuyenMai"].HeaderText = "Mã khuyến mãi";
                dgvKhuyenMai.Columns["maKhuyenMai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            if (dgvKhuyenMai.Columns["tenKhuyenMai"] != null)
            {
                dgvKhuyenMai.Columns["tenKhuyenMai"].HeaderText = "Tên khuyến mãi";
                dgvKhuyenMai.Columns["maKhuyenMai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            if (dgvKhuyenMai.Columns["moTa"] != null)
            {
                dgvKhuyenMai.Columns["moTa"].Visible = false;
            }
            if (dgvKhuyenMai.Columns["trangThai"] != null)
            {
                dgvKhuyenMai.Columns["trangThai"].HeaderText = "Trạng thái";
                dgvKhuyenMai.Columns["trangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
        private void BtnTaoKhuyenMai_Click(object sender, EventArgs e)
        {
            frmTaoKhuyenMai frm = new frmTaoKhuyenMai(parentfrm, "");
            parentfrm.OpenChildForm(frm);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct(parentfrm);
            parentfrm.OpenChildForm(frm);
        }
    }
}
