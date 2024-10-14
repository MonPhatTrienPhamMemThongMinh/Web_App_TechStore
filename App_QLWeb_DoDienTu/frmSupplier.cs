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
    public partial class frmSupplier : Form
    {
        private ucFormProduct ucFormProductInstance;
        public frmSupplier()
        {
            InitializeComponent();
            this.lblX.Click += LblX_Click;
            this.Paint += FrmCategory_Paint;
            ucFormProduct uc = new ucFormProduct();
            ucFormProductInstance = uc;
            ucFormSupplier1.SupplierDeleted += UcFormSupplier1_SupplierDeleted;
        }

        private void UcFormSupplier1_SupplierDeleted(object sender, EventArgs e)
        {
            ucFormProductInstance.LoadProducts();
        }

        private void FrmCategory_Paint(object sender, PaintEventArgs e)
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
