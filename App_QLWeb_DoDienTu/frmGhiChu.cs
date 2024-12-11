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
using Sunny.UI;
namespace App_QLWeb_DoDienTu
{
    public partial class frmGhiChu : Form
    {
        private string maPhieuDat;
        private PhieuDatBLL phieuDatBLL;
        public frmGhiChu(string maPhieuDat)
        {
            this.maPhieuDat = maPhieuDat;
            this.phieuDatBLL = new PhieuDatBLL();
            InitializeComponent();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmGhiChu_Load(object sender, EventArgs e)
        {
            PhieuDat phieuDat = phieuDatBLL.TimKiemPhieuDatTheoMaPhieuDat(maPhieuDat);
            txtLyDo.Text = phieuDat.GhiChu;
            if (phieuDat.TrangThai != "Không duyệt")
            {
                btnLuu.Enabled = false;
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            PhieuDat phieuDat = phieuDatBLL.TimKiemPhieuDatTheoMaPhieuDat(maPhieuDat);
            phieuDat.GhiChu=txtLyDo.Text;
            bool result = phieuDatBLL.SuaPhieuDat(phieuDat);
            if (result)
            {
                MessageBox.Show(this, "Lưu ghi chú thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, "Lưu ghi chú thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmGhiChu_FormClosing(object sender, FormClosingEventArgs e)
        {
            PhieuDat phieuDat = phieuDatBLL.TimKiemPhieuDatTheoMaPhieuDat(maPhieuDat);
            if (phieuDat.TrangThai == "Không duyệt")
            {
                if (phieuDat.GhiChu != txtLyDo.Text)
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn lưu các thay đổi trước khi đóng không?", "Xác nhận", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        phieuDat.GhiChu = txtLyDo.Text;
                        bool resultSuaPD = phieuDatBLL.SuaPhieuDat(phieuDat);
                        if (resultSuaPD)
                        {
                            MessageBox.Show(this, "Lưu ghi chú thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(this, "Lưu ghi chú thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }            
        }
    }
}
