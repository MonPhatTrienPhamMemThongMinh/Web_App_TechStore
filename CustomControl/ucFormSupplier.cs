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
    public partial class ucFormSupplier : UserControl
    {
        SupplierBLL sbll = new SupplierBLL();
        public event EventHandler SupplierDeleted;
        public ucFormSupplier()
        {
            InitializeComponent();
            this.dgvSupplier.SelectionChanged += DgvSupplier_SelectionChanged;
            this.Load += UcFormSupplier_Load;
            this.btnCancel.Click += BtnCancel_Click;
            this.btnEditSupplier.Click += BtnEditSupplier_Click;
            this.btnRemoveSupplier.Click += BtnRemoveSupplier_Click;
            this.btnAddSupplier.Click += BtnAddSupplier_Click;
            this.txtSupplierPhone.KeyPress += TxtSupplierPhone_KeyPress;
        }

        private void DgvSupplier_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvSupplier.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvSupplier.SelectedRows[0];
                if (selectedRow.Cells["maNhaCungCap"].Value != null)
                {
                    string supplierId = selectedRow.Cells["maNhaCungCap"].Value.ToString();
                    LoadSupplierDetail(supplierId);
                }
            }
        }

        private void BtnAddSupplier_Click(object sender, EventArgs e)
        {
            if (btnAddSupplier.Text == "Add New")
            {
                ClearForm();
                EnableDataGridView(false);
                btnAddSupplier.Text = "Accept";
                btnCancel.Enabled = true;
                btnCancel.BackColor = Color.IndianRed;
                btnEditSupplier.Enabled = false;
                btnEditSupplier.BackColor = Color.DarkGray;
                btnRemoveSupplier.Enabled = false;
                btnRemoveSupplier.BackColor = Color.DarkGray;

                txtSupplierID.Enabled = true;
                /*txtBrandID.Text = brandRepository.GenerateBrandID();*/

                txtSupplierName.Enabled = true;
                txtSupplierPhone.Enabled = true;
                txtSupplierAddress.Enabled = true;
                txtSupplierEmail.Enabled = true;
            }
            else if (btnAddSupplier.Text == "Accept")
            {
                if (ValidateInput())
                {
                    string supplierId = txtSupplierID.Text.Trim();
                    string supplierName = txtSupplierName.Text.Trim();
                    string phone = txtSupplierPhone.Text.Trim();
                    string address = txtSupplierAddress.Text.Trim();
                    string email = txtSupplierEmail.Text.Trim();

                    NhaCungCap newNcc = new NhaCungCap
                    {
                        maNhaCungCap = supplierId,
                        tenNhaCungCap = supplierName,
                        soDienThoai = phone,
                        email = email,
                        diaChi = address
                    };
                    try
                    {
                        bool isSuccess = sbll.AddSupplier(newNcc);
                        if (isSuccess)
                        {
                            MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadSupplierData();

                            ClearForm();
                            EnableDataGridView(true);
                            btnEditSupplier.Enabled = true;
                            btnEditSupplier.BackColor = Color.FromArgb(32, 80, 189);
                            btnRemoveSupplier.Enabled = true;
                            btnRemoveSupplier.BackColor = Color.FromArgb(32, 80, 189);
                            btnAddSupplier.Text = "Add New";
                            btnCancel.Enabled = false;
                            btnCancel.BackColor = Color.DarkGray;

                            txtSupplierAddress.Enabled = false;
                            txtSupplierID.Enabled = false;
                            txtSupplierPhone.Enabled = false;
                            txtSupplierName.Enabled = false;
                            txtSupplierEmail.Enabled = false;
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

        private void BtnRemoveSupplier_Click(object sender, EventArgs e)
        {
            if (dgvSupplier.SelectedRows.Count > 0)
            {
                List<string> selectedSupplierIds = new List<string>();

                foreach (DataGridViewRow row in dgvSupplier.SelectedRows)
                {
                    selectedSupplierIds.Add(row.Cells["maNhaCungCap"].Value.ToString());
                }

                DialogResult dialogResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa {selectedSupplierIds.Count} nhà cung cấp được chọn?",
                                              "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    bool allDeleted = true;
                    foreach (string supplierId in selectedSupplierIds)
                    {
                        NhaCungCap ncc = sbll.GetSupplierById(supplierId);
                        if (ncc != null)
                        {
                            bool isDeleted = sbll.DeleteSupplier(ncc);
                            if (!isDeleted)
                            {
                                allDeleted = false;
                            }
                        }
                    }
                    if (allDeleted)
                    {
                        MessageBox.Show("Xóa tất cả nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadSupplierData();
                        SupplierDeleted?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show("Một số nhà cung cấp không thể xóa được.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnEditSupplier_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSupplierID.Text))
            {
                if (btnEditSupplier.Text == "Edit Supplier")
                {
                    EnableDataGridView(false);
                    btnAddSupplier.Enabled = false;
                    btnAddSupplier.BackColor = Color.DarkGray;
                    btnEditSupplier.Text = "Accept";
                    btnRemoveSupplier.Enabled = false;
                    btnRemoveSupplier.BackColor = Color.DarkGray;
                    btnCancel.Enabled = true;
                    btnCancel.BackColor = Color.IndianRed;

                    txtSupplierName.Enabled = true;
                    txtSupplierPhone.Enabled = true;
                    txtSupplierEmail.Enabled = true;
                    txtSupplierAddress.Enabled = true;
                }
                else if (btnEditSupplier.Text == "Accept")
                {
                    if (ValidateInput())
                    {
                        string supplierId = txtSupplierID.Text;
                        string supplierName = txtSupplierName.Text.Trim();
                        string phone = txtSupplierPhone.Text.Trim();
                        string address = txtSupplierAddress.Text.Trim();
                        string email = txtSupplierEmail.Text.Trim();

                        NhaCungCap newNcc = new NhaCungCap
                        {
                            maNhaCungCap = supplierId,
                            tenNhaCungCap = supplierName,
                            soDienThoai = phone,
                            email = email,
                            diaChi = address
                        };

                        try
                        {
                            // Gọi repository để cập nhật thông tin
                            bool isSuccess = sbll.UpdateSupplier(newNcc);

                            if (isSuccess)
                            {
                                MessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnAddSupplier.Enabled = true;
                                btnAddSupplier.BackColor = Color.FromArgb(32, 80, 189);
                                btnRemoveSupplier.Enabled = true;
                                btnRemoveSupplier.BackColor = Color.FromArgb(32, 80, 189);
                                btnEditSupplier.Text = "Edit Supplier";
                                btnCancel.Enabled = false;
                                btnCancel.BackColor = Color.DarkGray;

                                txtSupplierName.Enabled = false;
                                txtSupplierPhone.Enabled = false;
                                txtSupplierAddress.Enabled = false;
                                txtSupplierEmail.Enabled = false;

                                LoadSupplierData();
                                EnableDataGridView(true);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi cập nhật nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Chọn nhà cung cấp bạn muốn sửa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (btnAddSupplier.Text == "Accept")
            {
                DialogResult result = MessageBox.Show("Bạn muốn hủy thêm mới nhà cung cấp?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ClearForm();
                    btnAddSupplier.Text = "Add New";
                    btnEditSupplier.Enabled = true;
                    btnEditSupplier.BackColor = Color.FromArgb(32, 80, 189);
                    btnRemoveSupplier.Enabled = true;
                    btnRemoveSupplier.BackColor = Color.FromArgb(32, 80, 189);
                    btnCancel.Enabled = false;
                    btnCancel.BackColor = Color.DarkGray;

                    txtSupplierID.Enabled = false;
                    txtSupplierName.Enabled = false;
                    txtSupplierPhone.Enabled = false;
                    txtSupplierEmail.Enabled = false;
                    txtSupplierAddress.Enabled = false;

                    // Kích hoạt lại DataGridView khi bấm Cancel
                    EnableDataGridView(true);
                }
            }

            if (btnEditSupplier.Text == "Accept")
            {
                DialogResult result = MessageBox.Show("Bạn muốn hủy chỉnh sửa hả?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    LoadSupplierDetail(txtSupplierID.Text);

                    btnAddSupplier.Enabled = true;
                    btnAddSupplier.BackColor = Color.FromArgb(32, 80, 189);
                    btnRemoveSupplier.Enabled = true;
                    btnRemoveSupplier.BackColor = Color.FromArgb(32, 80, 189);
                    btnEditSupplier.Text = "Edit Supplier";
                    btnCancel.Enabled = false;
                    btnCancel.BackColor = Color.DarkGray;

                    txtSupplierName.Enabled = false;
                    txtSupplierPhone.Enabled = false;
                    txtSupplierEmail.Enabled = false;
                    txtSupplierAddress.Enabled = false;

                    EnableDataGridView(true);
                }
            }
        }

        private void LoadSupplierDetail(string ma)
        {
            NhaCungCap ncc = sbll.GetSupplierById(ma);
            if (ncc != null)
            {
                txtSupplierID.Text = ncc.maNhaCungCap;
                txtSupplierName.Text = ncc.tenNhaCungCap;
                txtSupplierPhone.Text = ncc.soDienThoai;
                txtSupplierAddress.Text = ncc.diaChi;
                txtSupplierEmail.Text = ncc.email;
            }
        }

        private void EnableDataGridView(bool enable)
        {
            dgvSupplier.Enabled = enable;
            dgvSupplier.DefaultCellStyle.BackColor = enable ? Color.White : Color.LightGray;
        }

        private void UcFormSupplier_Load(object sender, EventArgs e)
        {
            LoadSupplierData();
        }

        private void ClearForm()
        {
            txtSupplierAddress.Text = "";
            txtSupplierEmail.Text = "";
            txtSupplierID.Text = "";
            txtSupplierName.Text = "";
            txtSupplierPhone.Text = "";
        }
        private void LoadSupplierData()
        {
            List<NhaCungCap> suppliers = sbll.getAllSuppliers();
            dgvSupplier.DataSource = suppliers;
            dgvSupplier.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtSupplierID.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhà cung cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSupplierPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtSupplierPhone.Text.Length < 10 || txtSupplierPhone.Text.Length > 11)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số điện thoại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void TxtSupplierPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
