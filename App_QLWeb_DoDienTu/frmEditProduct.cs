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
    public partial class frmEditProduct : Form
    {
        public event EventHandler ProductUpdated;
        public frmEditProduct(Product selectedProduct)
        {
            InitializeComponent();
            ucEditProduct1.LoadProductData(selectedProduct);
            ucEditProduct1.ProductUpdated += UcEditProduct1_ProductUpdated;
        }

        private void UcEditProduct1_ProductUpdated(object sender, EventArgs e)
        {
            ProductUpdated?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
    }
}
