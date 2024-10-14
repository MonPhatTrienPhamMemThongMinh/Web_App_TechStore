namespace App_QLWeb_DoDienTu
{
    partial class frmAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdmin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnQLOrders = new CustomControl.CustomButtonDN();
            this.btnQLProducts = new CustomControl.CustomButtonDN();
            this.btnDashboard = new CustomControl.CustomButtonDN();
            this.btnLogout = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblX = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(33)))), ((int)(((byte)(78)))));
            this.panel1.Controls.Add(this.btnQLOrders);
            this.panel1.Controls.Add(this.btnQLProducts);
            this.panel1.Controls.Add(this.btnDashboard);
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 725);
            this.panel1.TabIndex = 2;
            // 
            // btnQLOrders
            // 
            this.btnQLOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(33)))), ((int)(((byte)(78)))));
            this.btnQLOrders.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(33)))), ((int)(((byte)(78)))));
            this.btnQLOrders.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnQLOrders.BorderRadius = 10;
            this.btnQLOrders.BorderSize = 0;
            this.btnQLOrders.FlatAppearance.BorderSize = 0;
            this.btnQLOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQLOrders.ForeColor = System.Drawing.Color.White;
            this.btnQLOrders.Location = new System.Drawing.Point(35, 377);
            this.btnQLOrders.Name = "btnQLOrders";
            this.btnQLOrders.Size = new System.Drawing.Size(197, 49);
            this.btnQLOrders.TabIndex = 6;
            this.btnQLOrders.Text = "Order";
            this.btnQLOrders.TextColor = System.Drawing.Color.White;
            this.btnQLOrders.UseVisualStyleBackColor = false;
            // 
            // btnQLProducts
            // 
            this.btnQLProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(33)))), ((int)(((byte)(78)))));
            this.btnQLProducts.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(33)))), ((int)(((byte)(78)))));
            this.btnQLProducts.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnQLProducts.BorderRadius = 10;
            this.btnQLProducts.BorderSize = 0;
            this.btnQLProducts.FlatAppearance.BorderSize = 0;
            this.btnQLProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQLProducts.ForeColor = System.Drawing.Color.White;
            this.btnQLProducts.Location = new System.Drawing.Point(35, 312);
            this.btnQLProducts.Name = "btnQLProducts";
            this.btnQLProducts.Size = new System.Drawing.Size(197, 49);
            this.btnQLProducts.TabIndex = 6;
            this.btnQLProducts.Text = "Product";
            this.btnQLProducts.TextColor = System.Drawing.Color.White;
            this.btnQLProducts.UseVisualStyleBackColor = false;
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(33)))), ((int)(((byte)(78)))));
            this.btnDashboard.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(33)))), ((int)(((byte)(78)))));
            this.btnDashboard.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnDashboard.BorderRadius = 10;
            this.btnDashboard.BorderSize = 0;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(35, 247);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(197, 49);
            this.btnDashboard.TabIndex = 6;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.TextColor = System.Drawing.Color.White;
            this.btnDashboard.UseVisualStyleBackColor = false;
            // 
            // btnLogout
            // 
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Image = ((System.Drawing.Image)(resources.GetObject("btnLogout.Image")));
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(35, 670);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(158, 48);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(50, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "Welcome, Admin";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(33)))), ((int)(((byte)(78)))));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(65, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(131, 131);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblX.Location = new System.Drawing.Point(1403, 9);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(28, 28);
            this.lblX.TabIndex = 0;
            this.lblX.Text = "X";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.lblX);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1445, 40);
            this.panel2.TabIndex = 1;
            // 
            // frmAdmin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1445, 765);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Name = "frmAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Management System";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogout;
        private CustomControl.CustomButtonDN btnQLOrders;
        private CustomControl.CustomButtonDN btnQLProducts;
        private CustomControl.CustomButtonDN btnDashboard;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Panel panel2;
    }
}