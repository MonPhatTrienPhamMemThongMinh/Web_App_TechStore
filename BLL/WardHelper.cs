using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BLL
{
    public class WardHelper
    {
        private const string ApiKey = "5c296349-b9e1-11ef-9083-dadc35c0870d"; // Thay bằng API Key của bạn    
        private const string WardUrl = "https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/ward?district_id";
        public async Task<string> GetWardIdAsync(int? districtId, string wardName)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Token", ApiKey);

                    // Gửi yêu cầu API
                    HttpResponseMessage response = await client.GetAsync($"{WardUrl}?district_id={districtId}");

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var json = JObject.Parse(result);

                        if (json["code"].ToString() == "200")
                        {
                            // Duyệt qua danh sách Ward
                            foreach (var ward in json["data"])
                            {
                                if (ward["WardName"].ToString().Equals(wardName, StringComparison.OrdinalIgnoreCase))
                                {
                                    return ward["WardCode"].ToString();
                                }
                            }
                        }                        
                    }
                    else
                    {
                        string error = await response.Content.ReadAsStringAsync();                        
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }
    }
}
