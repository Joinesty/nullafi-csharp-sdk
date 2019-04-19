using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NullafiSDK.Services
{
    public class API
    {
        private static readonly HttpClient client = new HttpClient();

        public API()
        {
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://enterprise-api.nullafi.com");
            }

            if (!client.DefaultRequestHeaders.Contains("Accept"))
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }

            if (!client.DefaultRequestHeaders.Contains("Content-Type"))
            {
                client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            }
        }

        protected void SetSessionToken(string sessionToken)
        {
            if (client.DefaultRequestHeaders.Contains("Authentication"))
            {
                client.DefaultRequestHeaders.Remove("Authentication");
            }

            client.DefaultRequestHeaders.Add("Authentication", $"Bearer {sessionToken}");
        }

        private string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }

        public async Task<TResponse> Get<TResponse>(string path)
        {
            return JsonConvert.DeserializeObject<TResponse>(await client.GetStringAsync(path));
        }

        public async Task<TResponse> Patch<TPayload, TResponse>(string path, TPayload data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await client.PatchAsync(path, content);
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
        }

        public async Task<TResponse> Post<TPayload, TResponse>(string path, TPayload data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(path, content);
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
        }

        public async Task<TResponse> Put<TPayload, TResponse>(string path, TPayload data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(path, content);
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
        }

        public async Task Delete(string path)
        {
            await client.DeleteAsync(path);
        }
    }
}
