namespace App_QLWeb_DoDienTu
{
    partial class frmPhieuNhapHang
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
            this.phieuNhapReportView = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // phieuNhapReportView
            // 
            this.phieuNhapReportView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.phieuNhapReportView.LocalReport.ReportEmbeddedResource = "App_QLWeb_DoDienTu.PhieuNhapHang.rdlc";
            this.phieuNhapReportView.Location = new System.Drawing.Point(0, 0);
            this.phieuNhapReportView.Name = "phieuNhapReportView";
            this.phieuNhapReportView.ServerReport.BearerToken = null;
            this.phieuNhapReportView.Size = new System.Drawing.Size(681, 661);
            this.phieuNhapReportView.TabIndex = 0;
            // 
            // frmPhieuNhapHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 661);
            this.Controls.Add(this.phieuNhapReportView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPhieuNhapHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu nhập hàng";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPhieuNhapHang_FormClosed);
            this.Load += new System.EventHandler(this.frmPhieuNhapHang_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer phieuNhapReportView;
    }
}