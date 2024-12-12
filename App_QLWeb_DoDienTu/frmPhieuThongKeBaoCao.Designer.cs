namespace App_QLWeb_DoDienTu
{
    partial class frmPhieuThongKeBaoCao
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
            this.rpPhieuBaoCao = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpPhieuBaoCao
            // 
            this.rpPhieuBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpPhieuBaoCao.LocalReport.ReportEmbeddedResource = "MeVaBeProject.PhieuThongKeBaoCao.rdlc";
            this.rpPhieuBaoCao.Location = new System.Drawing.Point(0, 0);
            this.rpPhieuBaoCao.Name = "rpPhieuBaoCao";
            this.rpPhieuBaoCao.ServerReport.BearerToken = null;
            this.rpPhieuBaoCao.Size = new System.Drawing.Size(1122, 626);
            this.rpPhieuBaoCao.TabIndex = 0;
            // 
            // frmPhieuThongKeBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 626);
            this.Controls.Add(this.rpPhieuBaoCao);
            this.Name = "frmPhieuThongKeBaoCao";
            this.Text = "frmPhieuThongKeBaoCao";
            this.Load += new System.EventHandler(this.frmPhieuThongKeBaoCao_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpPhieuBaoCao;
    }
}