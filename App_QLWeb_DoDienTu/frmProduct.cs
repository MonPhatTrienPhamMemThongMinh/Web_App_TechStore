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
using ThuVien.Models;

namespace App_QLWeb_DoDienTu
{
    public partial class frmProduct : Form
    {
        string _cnn = Properties.Settings.Default.CNN;
        public frmProduct()
        {
            InitializeComponent();
            ucFormProduct1.CNN = _cnn;
            ucFormProduct1.AddProductClicked += UcFormProduct1_AddProductClicked;
            ucFormProduct1.EditProductClicked += UcFormProduct1_EditProductClicked;
        }

        private void UcFormProduct1_EditProductClicked(object sender, EventArgs e)
        {

        }

        private void FrmEditProduct_ProductUpdated(object sender, EventArgs e)
        {
            ucFormProduct1.LoadProducts();
        }

        private void UcFormProduct1_AddProductClicked(object sender, EventArgs e)
        {

        }

        private void FrmAddProduct_ProductAdded(object sender, EventArgs e)
        {
            ucFormProduct1.LoadProducts();
        }
    }
}
