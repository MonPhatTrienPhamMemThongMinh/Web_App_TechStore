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
    public partial class frmAdmin : Form
    {
        private Button currentButton = null;
        private Form currentChildForm = null;
        public frmAdmin()
        {
            InitializeComponent();
            this.lblX.Click += LblX_Click;
            this.btnLogout.Click += BtnLogout_Click;
            this.btnQLProducts.Click += BtnQLProducts_Click;
            this.btnQLOrders.Click += BtnQLOrders_Click;
        }

        private void BtnQLOrders_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmOrder(), (Button)sender);
        }

        private void BtnQLProducts_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmProduct(), (Button)sender);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Bạn có chắc là muốn thoát không?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                frmLogin frmLogin = new frmLogin();
                frmLogin.Show();
                this.Hide();
            }
        }

        private void LblX_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            frm.Show();
            this.Hide();
        }

        private void SetButtonSelected(Button button)
        {
            if(currentButton != null)
            {
                currentButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(33)))), ((int)(((byte)(78)))));
            }

            currentButton = button;
            currentButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(43)))), ((int)(((byte)(88))))); // Màu khi được chọn
        }

        // hàm đóng form hiện tại để hiện form mới mình bấm
        private void OpenChildForm(Form form, Button senderButton)
        {
            if(currentChildForm != null)
            {
                currentChildForm.Close();
            }

            currentChildForm = form;
            currentChildForm.MdiParent = this;
            currentChildForm.Dock = DockStyle.Fill;
            currentChildForm.Show();
            SetButtonSelected(senderButton);
        }
    }
}
