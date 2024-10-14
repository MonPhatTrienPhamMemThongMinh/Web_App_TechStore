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

namespace App_QLWeb_DoDienTu
{
    public partial class frmBrand : Form
    {
        private ucFormProduct ucFormProductInstance;
        public frmBrand()
        {
            InitializeComponent();
            this.lblX.Click += LblX_Click;
            this.Paint += FrmBrand_Paint;
            ucFormProductInstance = new ucFormProduct();
            ucFormBrand1.BrandDeleted += UcFormBrand1_BrandDeleted;
        }

        private void UcFormBrand1_BrandDeleted(object sender, EventArgs e)
        {
            ucFormProductInstance.LoadProducts();
        }

        private void FrmBrand_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 5);
            // Vẽ một hình chữ nhật xung quanh form
            e.Graphics.DrawRectangle(pen, this.DisplayRectangle);
        }

        private void LblX_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
