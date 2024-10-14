using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomControl
{
    public partial class ucFormOrder : UserControl
    {
        OrderBLL obll = new OrderBLL();
        public ucFormOrder()
        {
            InitializeComponent();
            this.Load += UcFormOrder_Load;
        }

        private void UcFormOrder_Load(object sender, EventArgs e)
        {
            LoadAllOrders();
        }

        private void LoadAllOrders()
        {
            try
            {
                List<dynamic> orders = obll.LoadAllOrders();
                dgvOrder.DataSource = orders;
                dgvOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvOrder.Columns["OrderId"].DisplayIndex = 0;
                dgvOrder.Columns["UserID"].DisplayIndex = 1;
                dgvOrder.Columns["CustomerName"].DisplayIndex = 2;
                dgvOrder.Columns["CustomerPhone"].DisplayIndex = 3;
                dgvOrder.Columns["CustomerAddress"].DisplayIndex = 4;
                dgvOrder.Columns["CustomerEmail"].DisplayIndex = 5;
                dgvOrder.Columns["TotalAmount"].DisplayIndex = 6;
                dgvOrder.Columns["PaymentMethod"].DisplayIndex = 7;
                dgvOrder.Columns["CreatedDate"].DisplayIndex = 8;
                dgvOrder.Columns["StatusText"].DisplayIndex = 9;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải các đơn hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
