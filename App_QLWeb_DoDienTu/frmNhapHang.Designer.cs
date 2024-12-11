namespace App_QLWeb_DoDienTu
{
    partial class frmNhapHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.btnClose = new Sunny.UI.UILabel();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.txtTongTien = new Sunny.UI.UITextBox();
            this.uiLabel9 = new Sunny.UI.UILabel();
            this.uiGroupBox2 = new Sunny.UI.UIGroupBox();
            this.dtgvSanPhamTrongPhieuNhap = new Sunny.UI.UIDataGridView();
            this.maSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongConLai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongDaNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngaySanXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hanSuDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donGiaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiGroupBox4 = new Sunny.UI.UIGroupBox();
            this.txtLanNhap = new Sunny.UI.UITextBox();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.btnChonPhieuDat = new Sunny.UI.UIButton();
            this.txtNhaCungCap = new Sunny.UI.UITextBox();
            this.btnLuu = new Sunny.UI.UISymbolButton();
            this.txtMaPhieuNhap = new Sunny.UI.UITextBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.txtDonGia = new Sunny.UI.UITextBox();
            this.uiLabel8 = new Sunny.UI.UILabel();
            this.txtSoLuongSanPham = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.txtTenSanPham = new Sunny.UI.UITextBox();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.txtMaSanPham = new Sunny.UI.UITextBox();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.txtMaPhieuDat = new Sunny.UI.UITextBox();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiPanel1.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvSanPhamTrongPhieuNhap)).BeginInit();
            this.uiGroupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiPanel1
            // 
            this.uiPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.uiPanel1.Controls.Add(this.btnClose);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel1.FillColor = System.Drawing.SystemColors.Window;
            this.uiPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiPanel1.Size = new System.Drawing.Size(1102, 43);
            this.uiPanel1.TabIndex = 0;
            this.uiPanel1.Text = "Phiếu nhập hàng";
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnClose.Location = new System.Drawing.Point(1063, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // uiPanel2
            // 
            this.uiPanel2.BackColor = System.Drawing.SystemColors.Window;
            this.uiPanel2.Controls.Add(this.txtTongTien);
            this.uiPanel2.Controls.Add(this.uiLabel9);
            this.uiPanel2.Controls.Add(this.uiGroupBox2);
            this.uiPanel2.Controls.Add(this.uiGroupBox4);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel2.FillColor = System.Drawing.SystemColors.Window;
            this.uiPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel2.Location = new System.Drawing.Point(0, 43);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiPanel2.Size = new System.Drawing.Size(1102, 493);
            this.uiPanel2.TabIndex = 11;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTongTien
            // 
            this.txtTongTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTongTien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTongTien.Enabled = false;
            this.txtTongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtTongTien.Location = new System.Drawing.Point(882, 452);
            this.txtTongTien.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTongTien.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.Padding = new System.Windows.Forms.Padding(5);
            this.txtTongTien.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.txtTongTien.ShowText = false;
            this.txtTongTien.Size = new System.Drawing.Size(216, 29);
            this.txtTongTien.TabIndex = 23;
            this.txtTongTien.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.txtTongTien.Watermark = "";
            // 
            // uiLabel9
            // 
            this.uiLabel9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uiLabel9.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel9.Location = new System.Drawing.Point(784, 456);
            this.uiLabel9.Name = "uiLabel9";
            this.uiLabel9.Size = new System.Drawing.Size(91, 23);
            this.uiLabel9.TabIndex = 25;
            this.uiLabel9.Text = "Tổng tiền:";
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox2.Controls.Add(this.dtgvSanPhamTrongPhieuNhap);
            this.uiGroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiGroupBox2.Location = new System.Drawing.Point(432, 0);
            this.uiGroupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiGroupBox2.Size = new System.Drawing.Size(669, 442);
            this.uiGroupBox2.TabIndex = 1;
            this.uiGroupBox2.Text = "Sản phẩm nhận";
            this.uiGroupBox2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtgvSanPhamTrongPhieuNhap
            // 
            this.dtgvSanPhamTrongPhieuNhap.AllowUserToAddRows = false;
            this.dtgvSanPhamTrongPhieuNhap.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dtgvSanPhamTrongPhieuNhap.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvSanPhamTrongPhieuNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dtgvSanPhamTrongPhieuNhap.BackgroundColor = System.Drawing.Color.White;
            this.dtgvSanPhamTrongPhieuNhap.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvSanPhamTrongPhieuNhap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvSanPhamTrongPhieuNhap.ColumnHeadersHeight = 32;
            this.dtgvSanPhamTrongPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgvSanPhamTrongPhieuNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.maSP,
            this.tenSP,
            this.soLuongConLai,
            this.soLuongDaNhan,
            this.ngaySanXuat,
            this.hanSuDung,
            this.donGiaSP,
            this.thanhTien});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvSanPhamTrongPhieuNhap.DefaultCellStyle = dataGridViewCellStyle7;
            this.dtgvSanPhamTrongPhieuNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvSanPhamTrongPhieuNhap.EnableHeadersVisualStyles = false;
            this.dtgvSanPhamTrongPhieuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dtgvSanPhamTrongPhieuNhap.GridColor = System.Drawing.Color.White;
            this.dtgvSanPhamTrongPhieuNhap.Location = new System.Drawing.Point(0, 32);
            this.dtgvSanPhamTrongPhieuNhap.Name = "dtgvSanPhamTrongPhieuNhap";
            this.dtgvSanPhamTrongPhieuNhap.ReadOnly = true;
            this.dtgvSanPhamTrongPhieuNhap.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvSanPhamTrongPhieuNhap.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.dtgvSanPhamTrongPhieuNhap.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dtgvSanPhamTrongPhieuNhap.ScrollBarBackColor = System.Drawing.SystemColors.Control;
            this.dtgvSanPhamTrongPhieuNhap.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.dtgvSanPhamTrongPhieuNhap.ScrollBarRectColor = System.Drawing.SystemColors.Control;
            this.dtgvSanPhamTrongPhieuNhap.ScrollBarStyleInherited = false;
            this.dtgvSanPhamTrongPhieuNhap.SelectedIndex = -1;
            this.dtgvSanPhamTrongPhieuNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvSanPhamTrongPhieuNhap.Size = new System.Drawing.Size(669, 410);
            this.dtgvSanPhamTrongPhieuNhap.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dtgvSanPhamTrongPhieuNhap.TabIndex = 1;
            this.dtgvSanPhamTrongPhieuNhap.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvSanPhamTrongPhieuDat_CellClick);
            this.dtgvSanPhamTrongPhieuNhap.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvSanPhamTrongPhieuDat_CellValueChanged);
            // 
            // maSP
            // 
            this.maSP.HeaderText = "Mã sản phẩm";
            this.maSP.Name = "maSP";
            this.maSP.ReadOnly = true;
            this.maSP.Width = 129;
            // 
            // tenSP
            // 
            this.tenSP.HeaderText = "Tên sản phẩm";
            this.tenSP.Name = "tenSP";
            this.tenSP.ReadOnly = true;
            this.tenSP.Width = 134;
            // 
            // soLuongConLai
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.soLuongConLai.DefaultCellStyle = dataGridViewCellStyle3;
            this.soLuongConLai.HeaderText = "Số lượng chưa nhận";
            this.soLuongConLai.Name = "soLuongConLai";
            this.soLuongConLai.ReadOnly = true;
            this.soLuongConLai.Width = 175;
            // 
            // soLuongDaNhan
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.soLuongDaNhan.DefaultCellStyle = dataGridViewCellStyle4;
            this.soLuongDaNhan.HeaderText = "Số lượng nhận";
            this.soLuongDaNhan.Name = "soLuongDaNhan";
            this.soLuongDaNhan.ReadOnly = true;
            this.soLuongDaNhan.Width = 136;
            // 
            // ngaySanXuat
            // 
            this.ngaySanXuat.HeaderText = "Ngày sản xuất";
            this.ngaySanXuat.Name = "ngaySanXuat";
            this.ngaySanXuat.ReadOnly = true;
            this.ngaySanXuat.Width = 133;
            // 
            // hanSuDung
            // 
            this.hanSuDung.HeaderText = "Hạn sử dụng";
            this.hanSuDung.Name = "hanSuDung";
            this.hanSuDung.ReadOnly = true;
            this.hanSuDung.Width = 124;
            // 
            // donGiaSP
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C0";
            dataGridViewCellStyle5.NullValue = null;
            this.donGiaSP.DefaultCellStyle = dataGridViewCellStyle5;
            this.donGiaSP.HeaderText = "Đơn giá";
            this.donGiaSP.Name = "donGiaSP";
            this.donGiaSP.ReadOnly = true;
            this.donGiaSP.Width = 88;
            // 
            // thanhTien
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "C0";
            dataGridViewCellStyle6.NullValue = null;
            this.thanhTien.DefaultCellStyle = dataGridViewCellStyle6;
            this.thanhTien.HeaderText = "Thành tiền";
            this.thanhTien.Name = "thanhTien";
            this.thanhTien.ReadOnly = true;
            this.thanhTien.Width = 108;
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Controls.Add(this.txtLanNhap);
            this.uiGroupBox4.Controls.Add(this.uiLabel4);
            this.uiGroupBox4.Controls.Add(this.btnChonPhieuDat);
            this.uiGroupBox4.Controls.Add(this.txtNhaCungCap);
            this.uiGroupBox4.Controls.Add(this.btnLuu);
            this.uiGroupBox4.Controls.Add(this.txtMaPhieuNhap);
            this.uiGroupBox4.Controls.Add(this.uiLabel1);
            this.uiGroupBox4.Controls.Add(this.txtDonGia);
            this.uiGroupBox4.Controls.Add(this.uiLabel8);
            this.uiGroupBox4.Controls.Add(this.txtSoLuongSanPham);
            this.uiGroupBox4.Controls.Add(this.uiLabel2);
            this.uiGroupBox4.Controls.Add(this.uiLabel7);
            this.uiGroupBox4.Controls.Add(this.txtTenSanPham);
            this.uiGroupBox4.Controls.Add(this.uiLabel6);
            this.uiGroupBox4.Controls.Add(this.txtMaSanPham);
            this.uiGroupBox4.Controls.Add(this.uiLabel5);
            this.uiGroupBox4.Controls.Add(this.txtMaPhieuDat);
            this.uiGroupBox4.Controls.Add(this.uiLabel3);
            this.uiGroupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiGroupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiGroupBox4.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox4.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiGroupBox4.Size = new System.Drawing.Size(435, 493);
            this.uiGroupBox4.TabIndex = 13;
            this.uiGroupBox4.Text = "Thông tin phiếu nhập";
            this.uiGroupBox4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLanNhap
            // 
            this.txtLanNhap.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLanNhap.Enabled = false;
            this.txtLanNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtLanNhap.Location = new System.Drawing.Point(146, 140);
            this.txtLanNhap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLanNhap.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtLanNhap.Name = "txtLanNhap";
            this.txtLanNhap.Padding = new System.Windows.Forms.Padding(5);
            this.txtLanNhap.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.txtLanNhap.ShowText = false;
            this.txtLanNhap.Size = new System.Drawing.Size(278, 29);
            this.txtLanNhap.TabIndex = 22;
            this.txtLanNhap.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.txtLanNhap.Watermark = "";
            // 
            // uiLabel4
            // 
            this.uiLabel4.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel4.Location = new System.Drawing.Point(16, 146);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(114, 23);
            this.uiLabel4.TabIndex = 23;
            this.uiLabel4.Text = "Lần nhập:";
            // 
            // btnChonPhieuDat
            // 
            this.btnChonPhieuDat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChonPhieuDat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnChonPhieuDat.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnChonPhieuDat.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnChonPhieuDat.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnChonPhieuDat.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnChonPhieuDat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnChonPhieuDat.Location = new System.Drawing.Point(302, 87);
            this.btnChonPhieuDat.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnChonPhieuDat.Name = "btnChonPhieuDat";
            this.btnChonPhieuDat.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnChonPhieuDat.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnChonPhieuDat.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnChonPhieuDat.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnChonPhieuDat.Size = new System.Drawing.Size(122, 29);
            this.btnChonPhieuDat.TabIndex = 26;
            this.btnChonPhieuDat.Text = "Chọn phiếu đặt";
            this.btnChonPhieuDat.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnChonPhieuDat.Click += new System.EventHandler(this.btnChonPhieuDat_Click);
            // 
            // txtNhaCungCap
            // 
            this.txtNhaCungCap.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNhaCungCap.Enabled = false;
            this.txtNhaCungCap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtNhaCungCap.Location = new System.Drawing.Point(146, 195);
            this.txtNhaCungCap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNhaCungCap.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtNhaCungCap.Name = "txtNhaCungCap";
            this.txtNhaCungCap.Padding = new System.Windows.Forms.Padding(5);
            this.txtNhaCungCap.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.txtNhaCungCap.ShowText = false;
            this.txtNhaCungCap.Size = new System.Drawing.Size(278, 29);
            this.txtNhaCungCap.TabIndex = 22;
            this.txtNhaCungCap.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtNhaCungCap.Watermark = "";
            // 
            // btnLuu
            // 
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnLuu.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnLuu.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnLuu.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnLuu.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnLuu.Location = new System.Drawing.Point(347, 445);
            this.btnLuu.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnLuu.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnLuu.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnLuu.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnLuu.Size = new System.Drawing.Size(77, 35);
            this.btnLuu.Symbol = 61639;
            this.btnLuu.TabIndex = 21;
            this.btnLuu.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // txtMaPhieuNhap
            // 
            this.txtMaPhieuNhap.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaPhieuNhap.Enabled = false;
            this.txtMaPhieuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtMaPhieuNhap.Location = new System.Drawing.Point(146, 32);
            this.txtMaPhieuNhap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMaPhieuNhap.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtMaPhieuNhap.Name = "txtMaPhieuNhap";
            this.txtMaPhieuNhap.Padding = new System.Windows.Forms.Padding(5);
            this.txtMaPhieuNhap.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.txtMaPhieuNhap.ShowText = false;
            this.txtMaPhieuNhap.Size = new System.Drawing.Size(278, 29);
            this.txtMaPhieuNhap.TabIndex = 21;
            this.txtMaPhieuNhap.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMaPhieuNhap.Watermark = "";
            // 
            // uiLabel1
            // 
            this.uiLabel1.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(16, 39);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(123, 23);
            this.uiLabel1.TabIndex = 25;
            this.uiLabel1.Text = "Mã phiếu nhập:";
            // 
            // txtDonGia
            // 
            this.txtDonGia.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDonGia.Enabled = false;
            this.txtDonGia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtDonGia.Location = new System.Drawing.Point(146, 408);
            this.txtDonGia.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDonGia.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Padding = new System.Windows.Forms.Padding(5);
            this.txtDonGia.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.txtDonGia.ShowText = false;
            this.txtDonGia.Size = new System.Drawing.Size(278, 29);
            this.txtDonGia.TabIndex = 22;
            this.txtDonGia.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.txtDonGia.Watermark = "";
            // 
            // uiLabel8
            // 
            this.uiLabel8.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel8.Location = new System.Drawing.Point(16, 414);
            this.uiLabel8.Name = "uiLabel8";
            this.uiLabel8.Size = new System.Drawing.Size(75, 23);
            this.uiLabel8.TabIndex = 24;
            this.uiLabel8.Text = "Đơn giá:";
            // 
            // txtSoLuongSanPham
            // 
            this.txtSoLuongSanPham.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoLuongSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtSoLuongSanPham.Location = new System.Drawing.Point(146, 354);
            this.txtSoLuongSanPham.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSoLuongSanPham.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtSoLuongSanPham.Name = "txtSoLuongSanPham";
            this.txtSoLuongSanPham.Padding = new System.Windows.Forms.Padding(5);
            this.txtSoLuongSanPham.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.txtSoLuongSanPham.ShowText = false;
            this.txtSoLuongSanPham.Size = new System.Drawing.Size(278, 29);
            this.txtSoLuongSanPham.TabIndex = 21;
            this.txtSoLuongSanPham.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.txtSoLuongSanPham.Watermark = "";
            this.txtSoLuongSanPham.TextChanged += new System.EventHandler(this.txtSoLuongSanPham_TextChanged);
            this.txtSoLuongSanPham.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoLuongSanPham_KeyPress);
            // 
            // uiLabel2
            // 
            this.uiLabel2.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel2.Location = new System.Drawing.Point(15, 201);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(122, 23);
            this.uiLabel2.TabIndex = 16;
            this.uiLabel2.Text = "Nhà cung cấp:";
            // 
            // uiLabel7
            // 
            this.uiLabel7.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel7.Location = new System.Drawing.Point(15, 360);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(91, 23);
            this.uiLabel7.TabIndex = 23;
            this.uiLabel7.Text = "Số lượng:";
            // 
            // txtTenSanPham
            // 
            this.txtTenSanPham.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenSanPham.Enabled = false;
            this.txtTenSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtTenSanPham.Location = new System.Drawing.Point(146, 303);
            this.txtTenSanPham.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTenSanPham.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtTenSanPham.Name = "txtTenSanPham";
            this.txtTenSanPham.Padding = new System.Windows.Forms.Padding(5);
            this.txtTenSanPham.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.txtTenSanPham.ShowText = false;
            this.txtTenSanPham.Size = new System.Drawing.Size(278, 29);
            this.txtTenSanPham.TabIndex = 22;
            this.txtTenSanPham.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTenSanPham.Watermark = "";
            // 
            // uiLabel6
            // 
            this.uiLabel6.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel6.Location = new System.Drawing.Point(16, 309);
            this.uiLabel6.Name = "uiLabel6";
            this.uiLabel6.Size = new System.Drawing.Size(114, 23);
            this.uiLabel6.TabIndex = 22;
            this.uiLabel6.Text = "Tên sản phẩm:";
            // 
            // txtMaSanPham
            // 
            this.txtMaSanPham.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaSanPham.Enabled = false;
            this.txtMaSanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtMaSanPham.Location = new System.Drawing.Point(146, 250);
            this.txtMaSanPham.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMaSanPham.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtMaSanPham.Name = "txtMaSanPham";
            this.txtMaSanPham.Padding = new System.Windows.Forms.Padding(5);
            this.txtMaSanPham.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.txtMaSanPham.ShowText = false;
            this.txtMaSanPham.Size = new System.Drawing.Size(278, 29);
            this.txtMaSanPham.TabIndex = 21;
            this.txtMaSanPham.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMaSanPham.Watermark = "";
            // 
            // uiLabel5
            // 
            this.uiLabel5.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel5.Location = new System.Drawing.Point(16, 256);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(114, 23);
            this.uiLabel5.TabIndex = 21;
            this.uiLabel5.Text = "Mã sản phẩm:";
            // 
            // txtMaPhieuDat
            // 
            this.txtMaPhieuDat.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaPhieuDat.Enabled = false;
            this.txtMaPhieuDat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtMaPhieuDat.Location = new System.Drawing.Point(146, 87);
            this.txtMaPhieuDat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMaPhieuDat.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtMaPhieuDat.Name = "txtMaPhieuDat";
            this.txtMaPhieuDat.Padding = new System.Windows.Forms.Padding(5);
            this.txtMaPhieuDat.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.txtMaPhieuDat.ShowText = false;
            this.txtMaPhieuDat.Size = new System.Drawing.Size(149, 29);
            this.txtMaPhieuDat.TabIndex = 20;
            this.txtMaPhieuDat.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMaPhieuDat.Watermark = "";
            // 
            // uiLabel3
            // 
            this.uiLabel3.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel3.Location = new System.Drawing.Point(16, 93);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(123, 23);
            this.uiLabel3.TabIndex = 18;
            this.uiLabel3.Text = "Mã phiếu đặt:";
            // 
            // frmNhapHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 536);
            this.Controls.Add(this.uiPanel2);
            this.Controls.Add(this.uiPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmNhapHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmNhapHang";
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel2.ResumeLayout(false);
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvSanPhamTrongPhieuNhap)).EndInit();
            this.uiGroupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UILabel btnClose;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UITextBox txtTongTien;
        private Sunny.UI.UILabel uiLabel9;
        private Sunny.UI.UISymbolButton btnLuu;
        private Sunny.UI.UIGroupBox uiGroupBox2;
        private Sunny.UI.UIDataGridView dtgvSanPhamTrongPhieuNhap;
        private Sunny.UI.UIGroupBox uiGroupBox4;
        private Sunny.UI.UITextBox txtDonGia;
        private Sunny.UI.UILabel uiLabel8;
        private Sunny.UI.UITextBox txtSoLuongSanPham;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel7;
        private Sunny.UI.UITextBox txtTenSanPham;
        private Sunny.UI.UILabel uiLabel6;
        private Sunny.UI.UITextBox txtMaSanPham;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UITextBox txtMaPhieuDat;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox txtMaPhieuNhap;
        private Sunny.UI.UITextBox txtNhaCungCap;
        private Sunny.UI.UIButton btnChonPhieuDat;
        private Sunny.UI.UITextBox txtLanNhap;
        private Sunny.UI.UILabel uiLabel4;
        private System.Windows.Forms.DataGridViewTextBoxColumn maSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongConLai;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongDaNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngaySanXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn hanSuDung;
        private System.Windows.Forms.DataGridViewTextBoxColumn donGiaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn thanhTien;
    }
}