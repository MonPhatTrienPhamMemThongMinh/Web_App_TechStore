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
using DTO;

namespace App_QLWeb_DoDienTu
{
    public partial class frmProduct : Form
    {
        public frmProduct()
        {
            InitializeComponent();
            ucFormProduct1.AddProductClicked += UcFormProduct1_AddProductClicked;
            ucFormProduct1.EditProductClicked += UcFormProduct1_EditProductClicked;
            ucFormProduct1.BrandFormClicked += UcFormProduct1_BrandFormClicked;
            ucFormProduct1.CategoryFormClicked += UcFormProduct1_CategoryFormClicked;
            ucFormProduct1.SupplierFormClicked += UcFormProduct1_SupplierFormClicked;
        }

        private void UcFormProduct1_SupplierFormClicked(object sender, EventArgs e)
        {
            frmSupplier frm = new frmSupplier();
            frm.Show();
        }

        private void UcFormProduct1_CategoryFormClicked(object sender, EventArgs e)
        {
            frmCategory frm = new frmCategory();
            frm.Show();
        }

        private void UcFormProduct1_BrandFormClicked(object sender, EventArgs e)
        {
            frmBrand frm = new frmBrand();
            frm.Show();
        }

        private void UcFormProduct1_EditProductClicked(object sender, EventArgs e)
        {
            Product selectedProduct = ucFormProduct1.SelectedProduct;
            frmEditProduct frmEditProduct = new frmEditProduct(selectedProduct);
            frmEditProduct.ProductUpdated += FrmEditProduct_ProductUpdated;
            frmEditProduct.Show();
        }

        private void FrmEditProduct_ProductUpdated(object sender, EventArgs e)
        {
            ucFormProduct1.LoadProducts();
        }

        private void UcFormProduct1_AddProductClicked(object sender, EventArgs e)
        {
            frmAddProduct frmAddProduct = new frmAddProduct();
            frmAddProduct.ProductAdded += FrmAddProduct_ProductAdded;
            frmAddProduct.Show();
        }

        private void FrmAddProduct_ProductAdded(object sender, EventArgs e)
        {
            ucFormProduct1.LoadProducts();
        }
    }
}
