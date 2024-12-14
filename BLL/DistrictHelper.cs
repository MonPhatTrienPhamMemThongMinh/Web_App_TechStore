using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Newtonsoft.Json;
namespace BLL
{
    public class DistrictHelper
    {
        private const string ApiKey = "5c296349-b9e1-11ef-9083-dadc35c0870d"; // Thay bằng API Key của bạn    
        private const string DistrictUrl = "https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/district";
        public async Task<int> GetDistrictIdByNameAsync(string districtName)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Token", ApiKey);

                HttpResponseMessage response = await client.GetAsync(DistrictUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json);

                    // Tìm quận/huyện dựa trên tên
                    var district = apiResponse.Data
                        .FirstOrDefault(d => string.Equals(d.DistrictName, districtName, StringComparison.OrdinalIgnoreCase));

                    return district.DistrictID; // Trả về DistrictID hoặc null nếu không tìm thấy
                }
                else
                {
                    throw new Exception($"Lỗi khi gọi API: {response.ReasonPhrase}");
                }
            }
        }
    }
}
