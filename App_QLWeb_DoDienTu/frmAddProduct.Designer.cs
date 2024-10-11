namespace App_QLWeb_DoDienTu
{
    partial class frmAddProduct
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
            this.ucAddProduct1 = new CustomControl.ucAddProduct();
            this.SuspendLayout();
            // 
            // ucAddProduct1
            // 
            this.ucAddProduct1.Location = new System.Drawing.Point(10, 11);
            this.ucAddProduct1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucAddProduct1.Name = "ucAddProduct1";
            this.ucAddProduct1.Size = new System.Drawing.Size(750, 631);
            this.ucAddProduct1.TabIndex = 0;
            // 
            // frmAddProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 788);
            this.Controls.Add(this.ucAddProduct1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmAddProduct";
            this.Text = "frmAddProduct";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.ucAddProduct ucAddProduct1;
    }
}