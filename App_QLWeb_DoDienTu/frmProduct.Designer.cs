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
            this.ucFormProduct1.Location = new System.Drawing.Point(-5, -4);
            this.ucFormProduct1.Name = "ucFormProduct1";
            this.ucFormProduct1.Size = new System.Drawing.Size(1563, 913);
            this.ucFormProduct1.TabIndex = 0;
            // 
            // frmProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1567, 931);
            this.Controls.Add(this.ucFormProduct1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmProduct";
            this.Text = "frmProduct";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.ucFormProduct ucFormProduct1;
    }
}