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
using ThuVien.DataAccess;
using ThuVien.Models;

namespace CustomControl
{
    public partial class ucFormBrand : UserControl
    {
        string cnn;
        private BrandRepository brandRepository;

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
            brandRepository = new BrandRepository(cnn);
            try
            {
                List<Brand> brands = brandRepository.GetAllBrands();
                dgvBrand.DataSource = brands.Select(br => new
                {
                    br.BrandID,
                    br.BrandName,
                    br.BrandDescription,
                    br.BrandPic,
                    br.BrandBackground
                }).ToList();

                dgvBrand.Columns["BrandID"].HeaderText = "Brand ID";
                dgvBrand.Columns["BrandName"].HeaderText = "Brand Name";
                dgvBrand.Columns["BrandDescription"].HeaderText = "Brand Description";
                dgvBrand.Columns["BrandPic"].HeaderText = "Brand Pic";
                dgvBrand.Columns["BrandBackground"].HeaderText = "Brand Background";

                dgvBrand.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải hãng sản phẩm: { ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBrandDetail(string brandId)
        {
            try
            {
                Brand brand = brandRepository.GetBrandById(brandId);
                if(brand != null)
                {
                    txtBrandID.Text = brand.BrandID;
                    txtBrandName.Text = brand.BrandName;
                    txtBrandDescription.Text = brand.BrandDescription;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chi tiết hãng sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
