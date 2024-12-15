using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace App_QLWeb_DoDienTu
{
    public partial class frmOrder : Form
    {
        OrderBLL odbll = new OrderBLL();
        OrderDetailBLL orderDetailBLL = new OrderDetailBLL();
        private string MaHoaDon;
        private const string ApiKey = "5c296349-b9e1-11ef-9083-dadc35c0870d"; // Thay bằng API Key của bạn
        private const string CreateOrderUrl = "https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/create";
        public frmOrder()
        {
            InitializeComponent();
            this.Load += FrmOrder_Load;
            this.dtpNgayBatDau.Value = this.dtpNgayKetThuc.Value;
            this.btnReset.Click += BtnReset_Click;
            this.btnTimKiem.Click += BtnTimKiem_Click;
            this.cboTieuChi.SelectedIndexChanged += CboTieuChi_SelectedIndexChanged;
            this.btnLocTheoNgay.Click += BtnLocTheoNgay_Click;
            this.btnLocHienTai.Click += BtnLocHienTai_Click;
            this.dtpNgayBatDau.ValueChanged += DtpNgayBatDau_ValueChanged;
            this.dtpNgayKetThuc.ValueChanged += DtpNgayKetThuc_ValueChanged;
            this.dgvHoaDon.SelectionChanged += DgvHoaDon_SelectionChanged;
            this.dgvHoaDon.CellFormatting += DgvHoaDon_CellFormatting;
            this.btnXemChiTiet.Click += BtnXemChiTiet_Click;
        }
        private void DtpNgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpNgayBatDau.Value > dtpNgayKetThuc.Value)
            {
                this.dtpNgayKetThuc.Value = dtpNgayBatDau.Value;
            }
        }
        private void DtpNgayKetThuc_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpNgayBatDau.Value > dtpNgayKetThuc.Value)
            {
                this.dtpNgayBatDau.Value = dtpNgayKetThuc.Value;
            }
        }
        private void DgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvHoaDon.SelectedRows[0];
                MaHoaDon = selectedRow.Cells["maDonHang"].Value.ToString();
            }
        }
        private void BtnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MaHoaDon))
            {
                frmCTHD frm = new frmCTHD(MaHoaDon);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                dtpNgayKetThuc.Value = DateTime.Now.Date;
                dtpNgayBatDau.Value = DateTime.Now.Date;
                txtTimKiem.Text = "";
                txtTimKiem.Enabled = false;
                List<Order> dsHoaDon = odbll.LoadAllOrders();
                SettingDgv(dsHoaDon);
                LoadTieuChiCombobox();
                LoadStatusCombobox();

                decimal tongDoanhThu = odbll.TinhTongDoanhThu();
                lblTongDoanhThu.Text = "Tổng doanh thu: " + tongDoanhThu.ToString("N0").Replace(",", ".") + "đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải đơn hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnLocHienTai_Click(object sender, EventArgs e)
        {
            this.dtpNgayKetThuc.Value = DateTime.Now.Date;
            this.dtpNgayBatDau.Value = DateTime.Now.Date;
            string tieuChi = cboTieuChi.SelectedItem.ToString();
            string tenTimKiem = txtTimKiem.Text.Trim();
            string trangThai = cboStatus.SelectedItem.ToString();
            DateTime ngayBatDau = DateTime.Now.Date;
            DateTime ngayKetThuc = DateTime.Now.Date;
            LoadDanhSachHoaDonTheoNgayLoc(tieuChi, tenTimKiem, ngayBatDau, ngayKetThuc, trangThai);
        }
        private void BtnLocTheoNgay_Click(object sender, EventArgs e)
        {
            string tieuChi = cboTieuChi.SelectedItem.ToString();
            string tenTimKiem = txtTimKiem.Text.Trim();
            string trangThai = cboStatus.SelectedItem.ToString();
            LoadDanhSachHoaDonTheoNgayLoc(tieuChi, tenTimKiem, dtpNgayBatDau.Value, dtpNgayKetThuc.Value, trangThai);
        }
        private void LoadDanhSachHoaDonTheoNgayLoc(string tieuChi, string tenTimKiem, DateTime ngayBatDau, DateTime ngayKetThuc, string trangThai)
        {
            decimal tongDoanhThu = 0;
            List<Order> ketQuaTimKiem = odbll.TimKiemVaLocHoaDon(tieuChi, tenTimKiem, ngayBatDau, ngayKetThuc, trangThai)
                                             .OrderByDescending(hd => hd.CreatedDate).ToList();
            tongDoanhThu = ketQuaTimKiem.Sum(hd => hd.TotalAmount);
            SettingDgv(ketQuaTimKiem);
            lblTongDoanhThu.Text = "Tổng doanh thu: " + tongDoanhThu.ToString("N0").Replace(",", ".") + "đ";
        }
        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            string tieuChi = cboTieuChi.SelectedItem.ToString();
            string tenTimKiem = txtTimKiem.Text.Trim();
            string trangThai = cboStatus.SelectedItem.ToString();
            LoadDanhSachHoaDonTheoNgayLoc(tieuChi, tenTimKiem, dtpNgayBatDau.Value, dtpNgayKetThuc.Value, trangThai);
        }
        // Dữ liệu số nằm bên phải
        private void DgvHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvHoaDon.Columns[e.ColumnIndex].Name == "soDienThoai")
            {
                return;
            }
            if (dgvHoaDon.Columns[e.ColumnIndex].Name == "diaChi")
            {
                return;
            }
            if (dgvHoaDon.Columns[e.ColumnIndex].Name == "maDonHang")
            {
                return;
            }
            if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal tien))
            {
                dgvHoaDon.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (tien != 0)
                {
                    e.Value = tien.ToString("N0").Replace(",", ".") + "đ";
                }
                else
                {
                    e.Value = tien.ToString("N0").Replace(",", ".");
                }
                e.FormattingApplied = true;
            }
        }
        private void LoadOrder()
        {
            List<Order> orders = odbll.LoadAllOrders();
            SettingDgv(orders);
        }
        private void FrmOrder_Load(object sender, EventArgs e)
        {
            this.dtpNgayBatDau.MaxDate = DateTime.Now.Date;
            this.dtpNgayKetThuc.MaxDate = DateTime.Now.Date;
            LoadOrder();
            LoadTieuChiCombobox();
            LoadStatusCombobox();
            decimal tongDoanhThu = odbll.TinhTongDoanhThu();
            lblTongDoanhThu.Text = "Tổng doanh thu: " + tongDoanhThu.ToString("N0").Replace(",", ".") + "đ";
        }
        private void CboTieuChi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTieuChi.SelectedItem.ToString() == "Các tiêu chí")
            {
                txtTimKiem.Enabled = false;
                txtTimKiem.Text = "";
            }
            else
            {
                txtTimKiem.Enabled = true;
            }
        }
        private void SettingDgv(List<Order> dsHoaDon)
        {
            dgvHoaDon.DataSource = dsHoaDon;
            dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHoaDon.Columns["AspNetUser"].Visible = false;
            dgvHoaDon.Columns["UserId"].Visible = false;
            dgvHoaDon.Columns["CustomerEmail"].Visible = false;
            dgvHoaDon.Columns["CustomerProvince"].Visible = false;
            dgvHoaDon.Columns["CustomerWard"].Visible = false;
            dgvHoaDon.Columns["CustomerDistrict"].Visible = false;
        }
        private void LoadTieuChiCombobox()
        {
            List<string> tieuChi = new List<string>
            {
                "Các tiêu chí",
                "Mã đơn hàng",
                "Tên khách hàng"
            };
            cboTieuChi.DataSource = tieuChi;

            cboTieuChi.SelectedIndex = 0;
        }
        private void LoadStatusCombobox()
        {
            List<string> status = new List<string>
            {
                "Chọn trạng thái..",
                "Chưa thanh toán",
                "Đã thanh toán"
            };
            cboStatus.DataSource = status;

            cboStatus.SelectedIndex = 0;
        }
        private async void btnTaoDonGiaoHang_Click(object sender, EventArgs e)
        {
            if (MaHoaDon!=null)
            {
                Order order = odbll.LoadHoaDonTheoMa(MaHoaDon);
                if (order.Status=="Đã xác nhận")
                {
                    int districtId = int.Parse(order.CustomerDistrict);
                    string wardCode = order.CustomerWard;
                    string to_name = order.CustomerName;
                    string to_phone = order.CustomerPhone;
                    string to_address = order.CustomerAddress;
                    int priceCOD = (order.PaymentMethod != "Chuyển khoản ngân hàng") ? int.Parse(order.TotalAmount.ToString().Split(',')[0]) : 0;
                    List<OrderDetail> details = orderDetailBLL.LoadOrderDetail(order.OrderId);
                    var items = details.Select(p => new { name = p.ProductName, quantity = p.Quantity });
                    var orderData = new
                    {
                        shop_id = 5522958, // ID cửa hàng từ GHN
                        payment_type_id = 2,
                        note = "Giao hàng nhanh",
                        client_order_code = order.OrderId,
                        required_note = "CHOXEMHANGKHONGTHU",
                        from_name = "Đặng Hoàng Phúc",
                        from_phone = "0888003346",
                        from_address = "469/32 Nguyễn Kiệm, Phường 9, Quận Phú Nhuận, Hồ Chí Minh, Vietnam",
                        from_ward_name = "Phường 9",
                        from_district_name = "Quận Phú Nhuận",
                        from_province_name = "HCM",
                        return_phone = "0888003346",
                        return_address = "469/32 Nguyễn Kiệm",
                        return_district_id = 1457,
                        to_name = to_name,
                        to_phone = to_phone,
                        to_address = to_address,
                        to_ward_code = wardCode,
                        to_district_id = districtId, // ID quận/huyện từ GHN
                        cod_amount = priceCOD, // Tiền thu hộ
                        weight = 500, // Trọng lượng (gram)
                        length = 10,
                        width = 10,
                        height = 10,
                        service_type_id = 2,
                        items = items
                    };
                    try
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Add("Token", ApiKey);

                            string json = JsonConvert.SerializeObject(orderData);
                            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                            HttpResponseMessage response = await client.PostAsync(CreateOrderUrl, content);

                            if (response.IsSuccessStatusCode)
                            {
                                string result = await response.Content.ReadAsStringAsync();
                                var maDonHang = JsonConvert.DeserializeObject<OrderResponse>(result);
                                MessageBox.Show(this, "Đơn hàng tạo thành công: " + maDonHang.order_code, "Tạo đơn hàng thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                string error = await response.Content.ReadAsStringAsync();
                                MessageBox.Show("Lỗi: " + error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Đơn hàng chưa được xác nhận không thể tạo đơn giao hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(this, "Vui lòng chọn 1 đơn hàng để xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (MaHoaDon!=null)
            {
                Order order = odbll.LoadHoaDonTheoMa(MaHoaDon);
                if (order.Status != "Đã xác nhận")
                {
                    bool result = odbll.XacNhanDonHang(order.OrderId);
                    if (result)
                    {
                        MessageBox.Show(this, "Xác nhận đơn hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadOrder();
                    }
                    else
                    {
                        MessageBox.Show(this, "Xác nhận đơn hàng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }       
    }
}

