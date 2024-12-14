namespace App_QLWeb_DoDienTu
{
    partial class frmQLNhapHang
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.uiGroupBox4 = new Sunny.UI.UIGroupBox();
            this.btnTaoPhieuNhap = new Sunny.UI.UIButton();
            this.btnInPhieuNhap = new Sunny.UI.UIButton();
            this.uiGroupBox3 = new Sunny.UI.UIGroupBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.dtNgayTaoPhieuNhap = new System.Windows.Forms.DateTimePicker();
            this.txtTimKiem = new Sunny.UI.UITextBox();
            this.btnTimKiem = new Sunny.UI.UISymbolButton();
            this.tabControlPhieuNhap = new Sunny.UI.UITabControl();
            this.tabPhieuNhap = new System.Windows.Forms.TabPage();
            this.dtgvPhieuNhap = new Sunny.UI.UIDataGridView();
            this.tbChiTiet = new System.Windows.Forms.TabPage();
            this.dtgvChiTietPhieuNhap = new Sunny.UI.UIDataGridView();
            this.maPN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phieuDat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maPD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenSanPham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngaySanXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hanSuDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tongTienSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.maPhieuNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maPhieuDat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiPanel1.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.uiGroupBox4.SuspendLayout();
            this.uiGroupBox3.SuspendLayout();
            this.tabControlPhieuNhap.SuspendLayout();
            this.tabPhieuNhap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPhieuNhap)).BeginInit();
            this.tbChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvChiTietPhieuNhap)).BeginInit();
            this.SuspendLayout();
            // 
            // uiLabel1
            // 
            this.uiLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(0, 0);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(1113, 43);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "Quản lý phiếu nhập";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel1
            // 
            this.uiPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.uiPanel1.Controls.Add(this.uiPanel2);
            this.uiPanel1.Controls.Add(this.tabControlPhieuNhap);
            this.uiPanel1.Controls.Add(this.uiLabel1);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel1.FillColor = System.Drawing.Color.White;
            this.uiPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.uiPanel1.Location = new System.Drawing.Point(0, 0);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.RectColor = System.Drawing.SystemColors.Window;
            this.uiPanel1.Size = new System.Drawing.Size(1113, 720);
            this.uiPanel1.TabIndex = 8;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.uiGroupBox4);
            this.uiPanel2.Controls.Add(this.uiGroupBox3);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiPanel2.Location = new System.Drawing.Point(0, 43);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiPanel2.Size = new System.Drawing.Size(1113, 81);
            this.uiPanel2.TabIndex = 14;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Controls.Add(this.btnTaoPhieuNhap);
            this.uiGroupBox4.Controls.Add(this.btnInPhieuNhap);
            this.uiGroupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.uiGroupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiGroupBox4.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox4.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiGroupBox4.Size = new System.Drawing.Size(360, 81);
            this.uiGroupBox4.TabIndex = 12;
            this.uiGroupBox4.Text = "Thao tác";
            this.uiGroupBox4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnTaoPhieuNhap
            // 
            this.btnTaoPhieuNhap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTaoPhieuNhap.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTaoPhieuNhap.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTaoPhieuNhap.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTaoPhieuNhap.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTaoPhieuNhap.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTaoPhieuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnTaoPhieuNhap.Location = new System.Drawing.Point(27, 30);
            this.btnTaoPhieuNhap.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnTaoPhieuNhap.Name = "btnTaoPhieuNhap";
            this.btnTaoPhieuNhap.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTaoPhieuNhap.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTaoPhieuNhap.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTaoPhieuNhap.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTaoPhieuNhap.Size = new System.Drawing.Size(131, 35);
            this.btnTaoPhieuNhap.TabIndex = 3;
            this.btnTaoPhieuNhap.Text = "Tạo phiếu nhập";
            this.btnTaoPhieuNhap.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnTaoPhieuNhap.Click += new System.EventHandler(this.btnTaoPhieuNhap_Click);
            // 
            // btnInPhieuNhap
            // 
            this.btnInPhieuNhap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInPhieuNhap.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnInPhieuNhap.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnInPhieuNhap.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnInPhieuNhap.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnInPhieuNhap.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnInPhieuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnInPhieuNhap.Location = new System.Drawing.Point(212, 30);
            this.btnInPhieuNhap.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnInPhieuNhap.Name = "btnInPhieuNhap";
            this.btnInPhieuNhap.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnInPhieuNhap.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnInPhieuNhap.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnInPhieuNhap.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnInPhieuNhap.Size = new System.Drawing.Size(131, 35);
            this.btnInPhieuNhap.TabIndex = 5;
            this.btnInPhieuNhap.Text = "In phiếu nhập";
            this.btnInPhieuNhap.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnInPhieuNhap.Click += new System.EventHandler(this.btnInPhieuNhap_Click);
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.uiLabel2);
            this.uiGroupBox3.Controls.Add(this.dtNgayTaoPhieuNhap);
            this.uiGroupBox3.Controls.Add(this.txtTimKiem);
            this.uiGroupBox3.Controls.Add(this.btnTimKiem);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.uiGroupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiGroupBox3.Location = new System.Drawing.Point(360, 0);
            this.uiGroupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox3.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiGroupBox3.Size = new System.Drawing.Size(753, 81);
            this.uiGroupBox3.TabIndex = 11;
            this.uiGroupBox3.Text = "Tìm kiếm";
            this.uiGroupBox3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            this.uiLabel2.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel2.Location = new System.Drawing.Point(465, 42);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(156, 23);
            this.uiLabel2.TabIndex = 4;
            this.uiLabel2.Text = "Ngày tạo phiếu nhập";
            // 
            // dtNgayTaoPhieuNhap
            // 
            this.dtNgayTaoPhieuNhap.CustomFormat = "dd-MM-yyyy";
            this.dtNgayTaoPhieuNhap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtNgayTaoPhieuNhap.Location = new System.Drawing.Point(627, 40);
            this.dtNgayTaoPhieuNhap.Name = "dtNgayTaoPhieuNhap";
            this.dtNgayTaoPhieuNhap.Size = new System.Drawing.Size(121, 26);
            this.dtNgayTaoPhieuNhap.TabIndex = 3;
            this.dtNgayTaoPhieuNhap.ValueChanged += new System.EventHandler(this.dtNgayTaoPhieuNhap_ValueChanged);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.Location = new System.Drawing.Point(25, 37);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTimKiem.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Padding = new System.Windows.Forms.Padding(5);
            this.txtTimKiem.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.txtTimKiem.ShowText = false;
            this.txtTimKiem.Size = new System.Drawing.Size(385, 29);
            this.txtTimKiem.TabIndex = 2;
            this.txtTimKiem.Text = "Nhập mã phiếu đặt hoặc phiếu nhập để tìm";
            this.txtTimKiem.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTimKiem.Watermark = "";
            this.txtTimKiem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimKiem_KeyPress);
            this.txtTimKiem.Leave += new System.EventHandler(this.txtTimKiem_Leave);
            this.txtTimKiem.Enter += new System.EventHandler(this.txtTimKiem_Enter);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTimKiem.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTimKiem.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTimKiem.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTimKiem.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnTimKiem.Location = new System.Drawing.Point(417, 36);
            this.btnTimKiem.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTimKiem.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTimKiem.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTimKiem.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.btnTimKiem.Size = new System.Drawing.Size(29, 29);
            this.btnTimKiem.Symbol = 61442;
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // tabControlPhieuNhap
            // 
            this.tabControlPhieuNhap.Controls.Add(this.tabPhieuNhap);
            this.tabControlPhieuNhap.Controls.Add(this.tbChiTiet);
            this.tabControlPhieuNhap.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControlPhieuNhap.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlPhieuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabControlPhieuNhap.ItemSize = new System.Drawing.Size(180, 40);
            this.tabControlPhieuNhap.Location = new System.Drawing.Point(0, 132);
            this.tabControlPhieuNhap.MainPage = "";
            this.tabControlPhieuNhap.MenuStyle = Sunny.UI.UIMenuStyle.White;
            this.tabControlPhieuNhap.Name = "tabControlPhieuNhap";
            this.tabControlPhieuNhap.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControlPhieuNhap.SelectedIndex = 0;
            this.tabControlPhieuNhap.Size = new System.Drawing.Size(1113, 588);
            this.tabControlPhieuNhap.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlPhieuNhap.TabBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tabControlPhieuNhap.TabIndex = 13;
            this.tabControlPhieuNhap.TabSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.tabControlPhieuNhap.TabSelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.tabControlPhieuNhap.TabSelectedHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.tabControlPhieuNhap.TabUnSelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.tabControlPhieuNhap.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.tabControlPhieuNhap.SelectedIndexChanged += new System.EventHandler(this.tabControlPhieuNhap_SelectedIndexChanged);
            // 
            // tabPhieuNhap
            // 
            this.tabPhieuNhap.Controls.Add(this.dtgvPhieuNhap);
            this.tabPhieuNhap.Location = new System.Drawing.Point(0, 40);
            this.tabPhieuNhap.Name = "tabPhieuNhap";
            this.tabPhieuNhap.Size = new System.Drawing.Size(1113, 548);
            this.tabPhieuNhap.TabIndex = 1;
            this.tabPhieuNhap.Text = "Danh sách phiếu nhập";
            this.tabPhieuNhap.UseVisualStyleBackColor = true;
            // 
            // dtgvPhieuNhap
            // 
            this.dtgvPhieuNhap.AllowUserToAddRows = false;
            this.dtgvPhieuNhap.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dtgvPhieuNhap.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvPhieuNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvPhieuNhap.BackgroundColor = System.Drawing.Color.White;
            this.dtgvPhieuNhap.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvPhieuNhap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvPhieuNhap.ColumnHeadersHeight = 32;
            this.dtgvPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgvPhieuNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.maPhieuNhap,
            this.maPhieuDat,
            this.ngayNhap,
            this.soLan,
            this.tongTien});
            this.dtgvPhieuNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvPhieuNhap.EnableHeadersVisualStyles = false;
            this.dtgvPhieuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtgvPhieuNhap.GridColor = System.Drawing.Color.White;
            this.dtgvPhieuNhap.Location = new System.Drawing.Point(0, 0);
            this.dtgvPhieuNhap.Name = "dtgvPhieuNhap";
            this.dtgvPhieuNhap.ReadOnly = true;
            this.dtgvPhieuNhap.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvPhieuNhap.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dtgvPhieuNhap.RowHeadersWidth = 51;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            this.dtgvPhieuNhap.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dtgvPhieuNhap.RowTemplate.Height = 24;
            this.dtgvPhieuNhap.ScrollBarBackColor = System.Drawing.SystemColors.Control;
            this.dtgvPhieuNhap.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.dtgvPhieuNhap.ScrollBarStyleInherited = false;
            this.dtgvPhieuNhap.SelectedIndex = -1;
            this.dtgvPhieuNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvPhieuNhap.Size = new System.Drawing.Size(1113, 548);
            this.dtgvPhieuNhap.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dtgvPhieuNhap.TabIndex = 9;
            this.dtgvPhieuNhap.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvPhieuNhap_CellDoubleClick);
            // 
            // tbChiTiet
            // 
            this.tbChiTiet.Controls.Add(this.dtgvChiTietPhieuNhap);
            this.tbChiTiet.Location = new System.Drawing.Point(0, 40);
            this.tbChiTiet.Name = "tbChiTiet";
            this.tbChiTiet.Size = new System.Drawing.Size(1113, 548);
            this.tbChiTiet.TabIndex = 2;
            this.tbChiTiet.Text = "Chi tiết phiếu nhập";
            this.tbChiTiet.UseVisualStyleBackColor = true;
            // 
            // dtgvChiTietPhieuNhap
            // 
            this.dtgvChiTietPhieuNhap.AllowUserToAddRows = false;
            this.dtgvChiTietPhieuNhap.AllowUserToDeleteRows = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dtgvChiTietPhieuNhap.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dtgvChiTietPhieuNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dtgvChiTietPhieuNhap.BackgroundColor = System.Drawing.Color.White;
            this.dtgvChiTietPhieuNhap.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvChiTietPhieuNhap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dtgvChiTietPhieuNhap.ColumnHeadersHeight = 32;
            this.dtgvChiTietPhieuNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dtgvChiTietPhieuNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.maPN,
            this.phieuDat,
            this.maPD,
            this.maSanPham,
            this.tenSanPham,
            this.soLuong,
            this.donGia,
            this.ngaySanXuat,
            this.hanSuDung,
            this.tongTienSP});
            this.dtgvChiTietPhieuNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvChiTietPhieuNhap.EnableHeadersVisualStyles = false;
            this.dtgvChiTietPhieuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dtgvChiTietPhieuNhap.GridColor = System.Drawing.Color.White;
            this.dtgvChiTietPhieuNhap.Location = new System.Drawing.Point(0, 0);
            this.dtgvChiTietPhieuNhap.Name = "dtgvChiTietPhieuNhap";
            this.dtgvChiTietPhieuNhap.ReadOnly = true;
            this.dtgvChiTietPhieuNhap.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvChiTietPhieuNhap.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dtgvChiTietPhieuNhap.RowHeadersWidth = 51;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.dtgvChiTietPhieuNhap.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dtgvChiTietPhieuNhap.RowTemplate.Height = 24;
            this.dtgvChiTietPhieuNhap.ScrollBarBackColor = System.Drawing.SystemColors.Control;
            this.dtgvChiTietPhieuNhap.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.dtgvChiTietPhieuNhap.ScrollBarRectColor = System.Drawing.Color.White;
            this.dtgvChiTietPhieuNhap.ScrollBarStyleInherited = false;
            this.dtgvChiTietPhieuNhap.SelectedIndex = -1;
            this.dtgvChiTietPhieuNhap.Size = new System.Drawing.Size(1113, 548);
            this.dtgvChiTietPhieuNhap.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dtgvChiTietPhieuNhap.TabIndex = 10;
            // 
            // maPN
            // 
            this.maPN.DataPropertyName = "MaPhieuNhap";
            this.maPN.HeaderText = "Mã phiếu nhập";
            this.maPN.Name = "maPN";
            this.maPN.ReadOnly = true;
            this.maPN.Width = 138;
            // 
            // phieuDat
            // 
            this.phieuDat.DataPropertyName = "PhieuDats";
            this.phieuDat.HeaderText = "Phiếu đặt";
            this.phieuDat.Name = "phieuDat";
            this.phieuDat.ReadOnly = true;
            this.phieuDat.Visible = false;
            // 
            // maPD
            // 
            this.maPD.DataPropertyName = "MaPhieuDat";
            this.maPD.HeaderText = "Mã phiếu đặt";
            this.maPD.Name = "maPD";
            this.maPD.ReadOnly = true;
            this.maPD.Width = 125;
            // 
            // maSanPham
            // 
            this.maSanPham.DataPropertyName = "ProductID";
            this.maSanPham.HeaderText = "Mã sản phẩm";
            this.maSanPham.Name = "maSanPham";
            this.maSanPham.ReadOnly = true;
            this.maSanPham.Width = 129;
            // 
            // tenSanPham
            // 
            this.tenSanPham.DataPropertyName = "tenSanPham";
            this.tenSanPham.HeaderText = "Tên sản phẩm";
            this.tenSanPham.Name = "tenSanPham";
            this.tenSanPham.ReadOnly = true;
            this.tenSanPham.Width = 134;
            // 
            // soLuong
            // 
            this.soLuong.DataPropertyName = "SoLuong";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.soLuong.DefaultCellStyle = dataGridViewCellStyle10;
            this.soLuong.HeaderText = "Số lượng";
            this.soLuong.Name = "soLuong";
            this.soLuong.ReadOnly = true;
            this.soLuong.Width = 96;
            // 
            // donGia
            // 
            this.donGia.DataPropertyName = "DonGia";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "C0";
            this.donGia.DefaultCellStyle = dataGridViewCellStyle11;
            this.donGia.HeaderText = "Đơn giá";
            this.donGia.Name = "donGia";
            this.donGia.ReadOnly = true;
            this.donGia.Width = 88;
            // 
            // ngaySanXuat
            // 
            this.ngaySanXuat.DataPropertyName = "NgaySanXuat";
            this.ngaySanXuat.HeaderText = "Ngày sản xuất";
            this.ngaySanXuat.Name = "ngaySanXuat";
            this.ngaySanXuat.ReadOnly = true;
            this.ngaySanXuat.Width = 133;
            // 
            // hanSuDung
            // 
            this.hanSuDung.DataPropertyName = "HanSuDung";
            this.hanSuDung.HeaderText = "Hạn sử dụng";
            this.hanSuDung.Name = "hanSuDung";
            this.hanSuDung.ReadOnly = true;
            this.hanSuDung.Width = 124;
            // 
            // tongTienSP
            // 
            this.tongTienSP.DataPropertyName = "TongTien";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "C0";
            this.tongTienSP.DefaultCellStyle = dataGridViewCellStyle12;
            this.tongTienSP.HeaderText = "Tổng tiền";
            this.tongTienSP.Name = "tongTienSP";
            this.tongTienSP.ReadOnly = true;
            this.tongTienSP.Width = 99;
            // 
            // maPhieuNhap
            // 
            this.maPhieuNhap.DataPropertyName = "MaPhieuNhap";
            this.maPhieuNhap.HeaderText = "Mã phiếu nhập";
            this.maPhieuNhap.Name = "maPhieuNhap";
            this.maPhieuNhap.ReadOnly = true;
            // 
            // maPhieuDat
            // 
            this.maPhieuDat.DataPropertyName = "MaPhieuDat";
            this.maPhieuDat.HeaderText = "Mã phiếu đặt";
            this.maPhieuDat.Name = "maPhieuDat";
            this.maPhieuDat.ReadOnly = true;
            // 
            // ngayNhap
            // 
            this.ngayNhap.DataPropertyName = "NgayNhap";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ngayNhap.DefaultCellStyle = dataGridViewCellStyle3;
            this.ngayNhap.HeaderText = "Ngày nhập";
            this.ngayNhap.Name = "ngayNhap";
            this.ngayNhap.ReadOnly = true;
            // 
            // soLan
            // 
            this.soLan.DataPropertyName = "SoLan";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.soLan.DefaultCellStyle = dataGridViewCellStyle4;
            this.soLan.HeaderText = "Số lần";
            this.soLan.Name = "soLan";
            this.soLan.ReadOnly = true;
            // 
            // tongTien
            // 
            this.tongTien.DataPropertyName = "TongTien";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "C0";
            this.tongTien.DefaultCellStyle = dataGridViewCellStyle5;
            this.tongTien.HeaderText = "Tổng tiền";
            this.tongTien.Name = "tongTien";
            this.tongTien.ReadOnly = true;
            // 
            // frmQLNhapHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1113, 720);
            this.Controls.Add(this.uiPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmQLNhapHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmNhapHang";
            this.Load += new System.EventHandler(this.frmNhapHang_Load);
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel2.ResumeLayout(false);
            this.uiGroupBox4.ResumeLayout(false);
            this.uiGroupBox3.ResumeLayout(false);
            this.tabControlPhieuNhap.ResumeLayout(false);
            this.tabPhieuNhap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvPhieuNhap)).EndInit();
            this.tbChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvChiTietPhieuNhap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIGroupBox uiGroupBox3;
        private Sunny.UI.UITextBox txtTimKiem;
        private Sunny.UI.UISymbolButton btnTimKiem;
        private System.Windows.Forms.DateTimePicker dtNgayTaoPhieuNhap;
        private Sunny.UI.UILabel uiLabel2;
        private System.Windows.Forms.ToolTip toolTip1;
        private Sunny.UI.UIButton btnTaoPhieuNhap;
        private Sunny.UI.UIButton btnInPhieuNhap;
        private Sunny.UI.UIGroupBox uiGroupBox4;
        private Sunny.UI.UITabControl tabControlPhieuNhap;
        private System.Windows.Forms.TabPage tabPhieuNhap;
        private Sunny.UI.UIDataGridView dtgvPhieuNhap;
        private System.Windows.Forms.TabPage tbChiTiet;
        private Sunny.UI.UIDataGridView dtgvChiTietPhieuNhap;
        private Sunny.UI.UIPanel uiPanel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn maPN;
        private System.Windows.Forms.DataGridViewTextBoxColumn phieuDat;
        private System.Windows.Forms.DataGridViewTextBoxColumn maPD;
        private System.Windows.Forms.DataGridViewTextBoxColumn maSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenSanPham;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn donGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngaySanXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn hanSuDung;
        private System.Windows.Forms.DataGridViewTextBoxColumn tongTienSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn maPhieuNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn maPhieuDat;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLan;
        private System.Windows.Forms.DataGridViewTextBoxColumn tongTien;
    }
}