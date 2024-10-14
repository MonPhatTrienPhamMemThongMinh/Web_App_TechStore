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
    public partial class frmCategory : Form
    {
        private ucFormProduct ucFormProductInstance;
        public frmCategory()
        {
            InitializeComponent();
            this.lblX.Click += LblX_Click;
            this.Paint += FrmCategory_Paint;
            ucFormProductInstance = new ucFormProduct();
            ucFormCategory1.CategoryDeleted += UcFormCategory1_CategoryDeleted;
        }

        private void FrmCategory_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 5);
            // Vẽ một hình chữ nhật xung quanh form
            e.Graphics.DrawRectangle(pen, this.DisplayRectangle);
        }

        private void UcFormCategory1_CategoryDeleted(object sender, EventArgs e)
        {
            ucFormProductInstance.LoadProducts();
        }

        private void LblX_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
