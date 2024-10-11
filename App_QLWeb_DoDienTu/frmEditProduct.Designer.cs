namespace App_QLWeb_DoDienTu
{
    partial class frmEditProduct
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
            this.ucEditProduct1 = new CustomControl.ucEditProduct();
            this.SuspendLayout();
            // 
            // ucEditProduct1
            // 
            this.ucEditProduct1.Location = new System.Drawing.Point(-2, -2);
            this.ucEditProduct1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucEditProduct1.Name = "ucEditProduct1";
            this.ucEditProduct1.Size = new System.Drawing.Size(1400, 863);
            this.ucEditProduct1.TabIndex = 0;
            // 
            // frmEditProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 640);
            this.Controls.Add(this.ucEditProduct1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmEditProduct";
            this.Text = "frmEditProduct";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.ucEditProduct ucEditProduct1;
    }
}