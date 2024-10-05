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
    public partial class frmAddProduct : Form
    {
        string cnn = Properties.Settings.Default.CNN;
        public event EventHandler ProductAdded;
        
        public frmAddProduct()
        {
            InitializeComponent();
            ucAddProduct1.CNN = cnn;
            ucAddProduct1.ProductAdded += UcAddProduct1_ProductAdded;
        }

        private void UcAddProduct1_ProductAdded(object sender, EventArgs e)
        {
            ProductAdded?.Invoke(this, EventArgs.Empty); // sau khi thêm thành công
            this.Close();
        }
    }
}
