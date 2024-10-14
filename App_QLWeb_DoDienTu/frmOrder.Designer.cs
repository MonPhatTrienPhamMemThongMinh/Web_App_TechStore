namespace App_QLWeb_DoDienTu
{
    partial class frmOrder
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
            this.ucFormOrder1 = new CustomControl.ucFormOrder();
            this.SuspendLayout();
            // 
            // ucFormOrder1
            // 
            this.ucFormOrder1.Location = new System.Drawing.Point(-5, -4);
            this.ucFormOrder1.Name = "ucFormOrder1";
            this.ucFormOrder1.Size = new System.Drawing.Size(1563, 913);
            this.ucFormOrder1.TabIndex = 0;
            // 
            // frmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1567, 931);
            this.Controls.Add(this.ucFormOrder1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmOrder";
            this.Text = "frmOrder";
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.ucFormOrder ucFormOrder1;
    }
}