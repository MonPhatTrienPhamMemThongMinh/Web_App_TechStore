using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomControl
{
    public partial class ucFormBrand : UserControl
    {
        string cnn;

        public string Cnn { get => cnn; set => cnn = value; }
        public ucFormBrand()
        {
            InitializeComponent();
            this.Load += UcFormBrand_Load;
        }

        private void UcFormBrand_Load(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(cnn))
            {
                Load_BrandData();
            }
        }

        private void Load_BrandData()
        {
            
        }

        private void LoadBrandDetail(string brandId)
        {
            
        }

        private string GetWebProjectPicFolderPath()
        {
            // Đường dẫn tương đối từ thư mục gốc của dự án Windows Forms tới thư mục Pic của dự án web
            string relativePath = @"..\..\..\PicBrand";

            // Kết hợp với đường dẫn gốc của ứng dụng để tạo đường dẫn tuyệt đối
            string absolutePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath));

            return absolutePath;
        }
    }
}
