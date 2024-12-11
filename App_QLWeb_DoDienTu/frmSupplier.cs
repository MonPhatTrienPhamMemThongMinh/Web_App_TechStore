using BLL;
using DTO;
using Sunny.UI;
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
        SupplierBLL nccbll = new SupplierBLL();
        public frmSupplier()
        {
            InitializeComponent();
            this.btnThem.Click += BtnThem_Click;
            this.txtSDT.KeyPress += TxtSDT_KeyPress;
            this.Load += FrmSupplier_Load;
            this.dgvNhaCungCap.CellClick += DgvNhaCungCap_CellClick;

            this.btnHuyBo.Click += BtnHuyBo_Click;
            this.btnSua.Click += BtnSua_Click;
            this.btnXoa.Click += BtnXoa_Click;
        }

        private void TxtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void SetButtonStyle(UIButton button, bool isEnabled)
        {
            if (isEnabled)
            {
                button.RectColor = System.Drawing.Color.FromArgb(84, 160, 255);
                button.RectDisableColor = System.Drawing.Color.FromArgb(84, 160, 255);
                button.RectHoverColor = System.Drawing.Color.FromArgb(84, 160, 255);
                button.RectPressColor = System.Drawing.Color.FromArgb(84, 160, 255);
                button.RectSelectedColor = System.Drawing.Color.FromArgb(255, 128, 255);
                button.FillColor = System.Drawing.Color.FromArgb(84, 160, 255);
                button.FillHoverColor = System.Drawing.Color.FromArgb(10, 189, 227);
                button.FillPressColor = System.Drawing.Color.FromArgb(46, 134, 222);
                button.ForeColor = System.Drawing.Color.White; // Màu chữ khi nút được kích hoạt
            }
            else
            {
                button.RectColor = System.Drawing.Color.DarkGray;
                button.RectDisableColor = System.Drawing.Color.DarkGray;
                button.RectHoverColor = System.Drawing.Color.DarkGray;
                button.RectPressColor = System.Drawing.Color.DarkGray;
                button.RectSelectedColor = System.Drawing.Color.DarkGray;
                button.FillColor = System.Drawing.Color.DarkGray;
                button.FillHoverColor = System.Drawing.Color.DarkGray;
                button.FillPressColor = System.Drawing.Color.DarkGray;
                button.ForeColor = System.Drawing.Color.Gray; // Màu chữ khi nút bị vô hiệu hóa
            }
        }
        private void LoadNhaCungCap()
        {
            List<NhaCungCap> nccList = nccbll.getAllSuppliers();
            dgvNhaCungCap.DataSource = nccList;
        }
        private void ClearForm()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
        }
        private void EnableDataGridView(bool enable)
        {
            dgvNhaCungCap.Enabled = enable;
            dgvNhaCungCap.DefaultCellStyle.BackColor = enable ? Color.White : Color.LightGray;
        }
        private void EnableTextBox(bool enable)
        {
            txtTenNCC.Enabled = enable;
            txtSDT.Enabled = enable;
            txtDiaChi.Enabled = enable;
            txtEmail.Enabled = enable;
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhà cung cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtSDT.Text.Length < 10 || txtSDT.Text.Length > 11)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số điện thoại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void FrmSupplier_Load(object sender, EventArgs e)
        {
            LoadNhaCungCap();
            EnableTextBox(false);
        }
        private void DgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaNCC.Text = dgvNhaCungCap.Rows[e.RowIndex].Cells["maNhaCungCap"].Value.ToString();
                txtTenNCC.Text = dgvNhaCungCap.Rows[e.RowIndex].Cells["tenNhaCungCap"].Value.ToString();
                txtDiaChi.Text = dgvNhaCungCap.Rows[e.RowIndex].Cells["diaChi"].Value.ToString();
                txtEmail.Text = dgvNhaCungCap.Rows[e.RowIndex].Cells["email"].Value.ToString();
                txtSDT.Text = dgvNhaCungCap.Rows[e.RowIndex].Cells["soDienThoai"].Value.ToString();
            }
        }
        private void BtnHuyBo_Click(object sender, EventArgs e)
        {
            ClearForm();
            EnableTextBox(false);

            EnableDataGridView(true);
            btnThem.Enabled = true;
            SetButtonStyle(btnThem, true);
            btnSua.Enabled = true;
            SetButtonStyle(btnSua, true);
            btnXoa.Enabled = true;
            SetButtonStyle(btnXoa, true);

            btnThem.Text = "Thêm";
            btnSua.Text = "Sửa";
            btnHuyBo.Enabled = false;
            btnHuyBo.BackColor = Color.DarkGray;
        }
        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (btnThem.Text == "Thêm")
            {
                ClearForm();
                EnableDataGridView(false);
                btnThem.Text = "Xác nhận";
                btnHuyBo.Enabled = true;
                btnHuyBo.BackColor = Color.IndianRed;
                btnSua.Enabled = false;
                SetButtonStyle(btnSua, false);
                btnXoa.Enabled = false;
                SetButtonStyle(btnXoa, false);
                txtMaNCC.Text = nccbll.TaoMaNhaCungCap();
                EnableTextBox(true);
            }
            else if (btnThem.Text == "Xác nhận")
            {
                if (ValidateInput())
                {
                    string maNcc = txtMaNCC.Text.Trim();
                    string tenNcc = txtTenNCC.Text.Trim();
                    string sdt = txtSDT.Text.Trim();
                    string diaChi = txtDiaChi.Text.Trim();
                    string email = txtEmail.Text.Trim();

                    NhaCungCap newNcc = new NhaCungCap
                    {
                        maNhaCungCap = maNcc,
                        tenNhaCungCap = tenNcc,
                        soDienThoai = sdt,
                        email = email,
                        diaChi = diaChi
                    };
                    try
                    {
                        bool isSuccess = nccbll.AddSupplier(newNcc);
                        if (isSuccess)
                        {
                            MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadNhaCungCap();

                            ClearForm();
                            EnableDataGridView(true);
                            btnSua.Enabled = true;
                            SetButtonStyle(btnSua, true);
                            btnXoa.Enabled = true;
                            SetButtonStyle(btnXoa, true);

                            btnThem.Text = "Thêm";
                            btnHuyBo.Enabled = false;
                            btnHuyBo.BackColor = Color.DarkGray;

                            EnableTextBox(false);
                        }
                        else
                        {
                            MessageBox.Show("Thêm nhà cung cấp thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi thêm nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNCC.Text != string.Empty)
            {
                string maNhaCungCap = txtMaNCC.Text;
                string tenNhaCungCap = txtTenNCC.Text;
                string soDienThoai = txtSDT.Text;
                string email = txtEmail.Text;
                string diaChi = txtDiaChi.Text;
                DialogResult r = MessageBox.Show(this, "Bạn có chắc chắc muốn xóa nhà cung cấp " + tenNhaCungCap + " này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    NhaCungCap nhaCungCap = new NhaCungCap
                    {
                        maNhaCungCap = maNhaCungCap,
                        tenNhaCungCap = tenNhaCungCap,
                        soDienThoai = soDienThoai,
                        email = email,
                        diaChi = diaChi
                    };
                    bool isSuccess = nccbll.DeleteSupplier(nhaCungCap);
                    if (isSuccess)
                    {
                        MessageBox.Show(this, "Xóa nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNhaCungCap();
                    }
                    else
                    {
                        MessageBox.Show(this, "Xóa nhà cung cấp thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (txtMaNCC.Text != string.Empty)
            {
                if (btnSua.Text == "Sửa")
                {
                    EnableDataGridView(false);
                    btnSua.Text = "Xác nhận";
                    btnHuyBo.Enabled = true;
                    btnHuyBo.BackColor = Color.IndianRed;
                    btnThem.Enabled = false;
                    SetButtonStyle(btnThem, false);
                    btnXoa.Enabled = false;
                    SetButtonStyle(btnXoa, false);

                    EnableTextBox(true);
                }
                else if (btnSua.Text == "Xác nhận")
                {
                    if (ValidateInput())
                    {
                        string maNcc = txtMaNCC.Text.Trim();
                        string tenNcc = txtTenNCC.Text.Trim();
                        string sdt = txtSDT.Text.Trim();
                        string diaChi = txtDiaChi.Text.Trim();
                        string email = txtEmail.Text.Trim();

                        NhaCungCap newNcc = new NhaCungCap
                        {
                            maNhaCungCap = maNcc,
                            tenNhaCungCap = tenNcc,
                            soDienThoai = sdt,
                            email = email,
                            diaChi = diaChi
                        };
                        try
                        {
                            bool isSuccess = nccbll.UpdateSupplier(newNcc);
                            if (isSuccess)
                            {
                                MessageBox.Show("Sửa nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadNhaCungCap();

                                ClearForm();
                                EnableDataGridView(true);
                                btnThem.Enabled = true;
                                SetButtonStyle(btnThem, true);
                                btnXoa.Enabled = true;
                                SetButtonStyle(btnXoa, true);

                                btnSua.Text = "Sửa";
                                btnHuyBo.Enabled = false;
                                btnHuyBo.BackColor = Color.DarkGray;

                                EnableTextBox(false);
                            }
                            else
                            {
                                MessageBox.Show("Sửa nhà cung cấp thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi sửa nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

    }
}
