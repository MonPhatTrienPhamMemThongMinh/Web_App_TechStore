namespace CustomControl
{
    partial class ucFormBrand
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbBrandLogo = new System.Windows.Forms.PictureBox();
            this.txtBrandDescription = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBrandName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBrandID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvBrand = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pbBrandBackground = new System.Windows.Forms.PictureBox();
            this.btnBrandBackground = new CustomControl.CustomButtonDN();
            this.btnBrandLogo = new CustomControl.CustomButtonDN();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBrandLogo)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBrand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBrandBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnBrandBackground);
            this.panel1.Controls.Add(this.btnBrandLogo);
            this.panel1.Controls.Add(this.pbBrandBackground);
            this.panel1.Controls.Add(this.pbBrandLogo);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtBrandDescription);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtBrandName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtBrandID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(29, 548);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1566, 355);
            this.panel1.TabIndex = 3;
            // 
            // pbBrandLogo
            // 
            this.pbBrandLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBrandLogo.Location = new System.Drawing.Point(721, 26);
            this.pbBrandLogo.Name = "pbBrandLogo";
            this.pbBrandLogo.Size = new System.Drawing.Size(300, 300);
            this.pbBrandLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBrandLogo.TabIndex = 29;
            this.pbBrandLogo.TabStop = false;
            // 
            // txtBrandDescription
            // 
            this.txtBrandDescription.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtBrandDescription.Location = new System.Drawing.Point(188, 153);
            this.txtBrandDescription.Multiline = true;
            this.txtBrandDescription.Name = "txtBrandDescription";
            this.txtBrandDescription.Size = new System.Drawing.Size(300, 173);
            this.txtBrandDescription.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(553, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 21);
            this.label5.TabIndex = 3;
            this.label5.Text = "Brand Logo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(25, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Brand Description:";
            // 
            // txtBrandName
            // 
            this.txtBrandName.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtBrandName.Location = new System.Drawing.Point(188, 85);
            this.txtBrandName.Name = "txtBrandName";
            this.txtBrandName.Size = new System.Drawing.Size(300, 28);
            this.txtBrandName.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(25, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Brand Name:";
            // 
            // txtBrandID
            // 
            this.txtBrandID.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtBrandID.Location = new System.Drawing.Point(188, 26);
            this.txtBrandID.Name = "txtBrandID";
            this.txtBrandID.Size = new System.Drawing.Size(300, 28);
            this.txtBrandID.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(25, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Brand ID:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvBrand);
            this.panel2.Location = new System.Drawing.Point(29, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1566, 453);
            this.panel2.TabIndex = 4;
            // 
            // dgvBrand
            // 
            this.dgvBrand.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBrand.Location = new System.Drawing.Point(24, 0);
            this.dgvBrand.Name = "dgvBrand";
            this.dgvBrand.RowHeadersWidth = 51;
            this.dgvBrand.RowTemplate.Height = 24;
            this.dgvBrand.Size = new System.Drawing.Size(1522, 453);
            this.dgvBrand.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(231, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "Brand Detail";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(1042, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 21);
            this.label6.TabIndex = 3;
            this.label6.Text = "Brand Background:";
            // 
            // pbBrandBackground
            // 
            this.pbBrandBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBrandBackground.Location = new System.Drawing.Point(1210, 26);
            this.pbBrandBackground.Name = "pbBrandBackground";
            this.pbBrandBackground.Size = new System.Drawing.Size(300, 300);
            this.pbBrandBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBrandBackground.TabIndex = 29;
            this.pbBrandBackground.TabStop = false;
            // 
            // btnBrandBackground
            // 
            this.btnBrandBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(80)))), ((int)(((byte)(189)))));
            this.btnBrandBackground.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(80)))), ((int)(((byte)(189)))));
            this.btnBrandBackground.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(80)))), ((int)(((byte)(189)))));
            this.btnBrandBackground.BorderRadius = 10;
            this.btnBrandBackground.BorderSize = 0;
            this.btnBrandBackground.FlatAppearance.BorderSize = 0;
            this.btnBrandBackground.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrandBackground.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnBrandBackground.ForeColor = System.Drawing.Color.White;
            this.btnBrandBackground.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrandBackground.Location = new System.Drawing.Point(1046, 69);
            this.btnBrandBackground.Name = "btnBrandBackground";
            this.btnBrandBackground.Size = new System.Drawing.Size(129, 44);
            this.btnBrandBackground.TabIndex = 6;
            this.btnBrandBackground.Text = "Import";
            this.btnBrandBackground.TextColor = System.Drawing.Color.White;
            this.btnBrandBackground.UseVisualStyleBackColor = false;
            // 
            // btnBrandLogo
            // 
            this.btnBrandLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(80)))), ((int)(((byte)(189)))));
            this.btnBrandLogo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(80)))), ((int)(((byte)(189)))));
            this.btnBrandLogo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(80)))), ((int)(((byte)(189)))));
            this.btnBrandLogo.BorderRadius = 10;
            this.btnBrandLogo.BorderSize = 0;
            this.btnBrandLogo.FlatAppearance.BorderSize = 0;
            this.btnBrandLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrandLogo.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnBrandLogo.ForeColor = System.Drawing.Color.White;
            this.btnBrandLogo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrandLogo.Location = new System.Drawing.Point(557, 69);
            this.btnBrandLogo.Name = "btnBrandLogo";
            this.btnBrandLogo.Size = new System.Drawing.Size(129, 44);
            this.btnBrandLogo.TabIndex = 6;
            this.btnBrandLogo.Text = "Import";
            this.btnBrandLogo.TextColor = System.Drawing.Color.White;
            this.btnBrandLogo.UseVisualStyleBackColor = false;
            // 
            // ucFormBrand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucFormBrand";
            this.Size = new System.Drawing.Size(1611, 919);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBrandLogo)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBrand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBrandBackground)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtBrandDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBrandName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBrandID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvBrand;
        private System.Windows.Forms.PictureBox pbBrandLogo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private CustomButtonDN btnBrandLogo;
        private CustomButtonDN btnBrandBackground;
        private System.Windows.Forms.PictureBox pbBrandBackground;
        private System.Windows.Forms.Label label6;
    }
}
