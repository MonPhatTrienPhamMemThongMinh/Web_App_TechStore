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
using BLL;
namespace App_QLWeb_DoDienTu
{
    public partial class frmDanhSachPhieuDat : Form
    {
        private PhieuDatBLL phieuDatBLL;
        private PhieuNhapBLL phieuNhapBLL;
        private ChiTietPhieuDatBLL chiTietPhieuDatBLL;       
        public delegate void SendDataHandler(string data);
        public event SendDataHandler DataSent;
        public frmDanhSachPhieuDat()
        {
            InitializeComponent();
            this.phieuDatBLL = new PhieuDatBLL();
            this.phieuNhapBLL = new PhieuNhapBLL();
            this.chiTietPhieuDatBLL = new ChiTietPhieuDatBLL();
            this.Load += FrmDanhSachPhieuDat_Load;
        }
        private void FrmDanhSachPhieuDat_Load(object sender, EventArgs e)
        {
            List<PhieuDat> danhSachPhieuDat = phieuDatBLL.LayDanhSachPhieuDatDaDuyet();
            if (danhSachPhieuDat!=null)
            {
                for (int i = danhSachPhieuDat.Count - 1; i >= 0; i--)
                {
                    PhieuDat phieuDat = danhSachPhieuDat[i];
                    List<PhieuNhap> danhSachPhieuNhap = phieuNhapBLL.TimKiemPhieuNhapTheoMaPhieuDat(phieuDat.MaPhieuDat);
                    if (danhSachPhieuNhap.Count == 3 || (phieuDat.NgayLap.Hour + 5 > DateTime.Now.Hour && phieuDat.NgayLap.Day == DateTime.Now.Day))
                    {
                        danhSachPhieuDat.Remove(phieuDat);
                    }
                    else
                    {
                        List<ChiTietPhieuDat> chiTietPhieuDats = chiTietPhieuDatBLL.LayChiTietPhieuDat(phieuDat.MaPhieuDat);
                        int dem = 0;
                        foreach (ChiTietPhieuDat chiTietPhieuDat in chiTietPhieuDats)
                        {
                            if (chiTietPhieuDat.SoLuongDat == chiTietPhieuDat.SoLuongNhan)
                            {
                                dem++;
                            }
                        }
                        if (dem == phieuDat.SoLuong)
                        {
                            danhSachPhieuDat.Remove(phieuDat);
                        }
                    }
                }
                dtgvDanhSachPhieuDat.DataSource = danhSachPhieuDat;
                dtgvDanhSachPhieuDat.Columns["TrangThai"].Visible = false;
                dtgvDanhSachPhieuDat.Columns["NgayCapNhat"].Visible = false;
                dtgvDanhSachPhieuDat.Columns["UserID"].Visible = false;
                dtgvDanhSachPhieuDat.Columns["AspNetUser"].Visible = false;
                dtgvDanhSachPhieuDat.Columns["NhaCungCap"].Visible = false;
                dtgvDanhSachPhieuDat.Columns["MaNhaCungCap"].Visible = false;
            }            
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dtgvDanhSachPhieuDat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0)
            {
                string maPhieuDat = dtgvDanhSachPhieuDat.Rows[e.RowIndex].Cells["maPhieuDat"].Value.ToString();
                DataSent?.Invoke(maPhieuDat);
                this.Close();
            }
        }
    }
}
