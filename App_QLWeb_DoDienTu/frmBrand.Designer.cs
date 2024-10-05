namespace App_QLWeb_DoDienTu
{
    partial class frmBrand
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
            this.ucFormBrand1 = new CustomControl.ucFormBrand();
            this.SuspendLayout();
            // 
            // ucFormBrand1
            // 
            this.ucFormBrand1.Cnn = null;
            this.ucFormBrand1.Location = new System.Drawing.Point(-1, 0);
            this.ucFormBrand1.Name = "ucFormBrand1";
            this.ucFormBrand1.Size = new System.Drawing.Size(1656, 912);
            this.ucFormBrand1.TabIndex = 0;
            // 
            // frmBrand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1667, 924);
            this.Controls.Add(this.ucFormBrand1);
            this.Name = "frmBrand";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBrand";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.ucFormBrand ucFormBrand1;
    }
}