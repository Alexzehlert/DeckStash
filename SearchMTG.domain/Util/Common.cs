using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchMTG.domain.Util
{
    public class Common
    {
        public static async Task<T> HttpGet<T>(string endpointUri)
        {
            try
            {
                string json;
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(endpointUri);
                    if (!response.IsSuccessStatusCode)
                        return default(T);
                    json = await response.Content.ReadAsStringAsync();
                }
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return default(T);
            }
        }
    }
}
