namespace App_QLWeb_DoDienTu
{
    partial class frmProduct
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
            this.ucFormProduct1 = new CustomControl.ucFormProduct();
            this.SuspendLayout();
            // 
            // ucFormProduct1
            // 
            this.ucFormProduct1.Location = new System.Drawing.Point(-4, -3);
            this.ucFormProduct1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucFormProduct1.Name = "ucFormProduct1";
            this.ucFormProduct1.Size = new System.Drawing.Size(1662, 913);
            this.ucFormProduct1.TabIndex = 0;
            // 
            // frmProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 640);
            this.Controls.Add(this.ucFormProduct1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmProduct";
            this.Text = "frmProduct";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.ucFormProduct ucFormProduct1;
    }
}