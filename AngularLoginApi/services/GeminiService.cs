using Newtonsoft.Json;
using System.Text;

namespace AngularLoginApi.services
{
    public class GeminiService
    {
        private readonly string _apikey;
        private readonly HttpClient _http;

        public GeminiService(IConfiguration _config)
        {
            _apikey = _config["Gemini:Apikey"];
            _http = new HttpClient();
        }
        public async Task<string> AskGemini(string prompt)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apikey}";

            var requestbody = new
            {
                contents = new[]
                {
                   new
                   {
                       parts = new[]
                       {
                           new{text=prompt}
                       }
                   }
                }
            };

            var json = JsonConvert.SerializeObject(requestbody);
            var response = await _http.PostAsync(url, new StringContent(json, Encoding.UTF8,"appication/json"));

            return await response.Content.ReadAsStringAsync();
        }
    }
}
