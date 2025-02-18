using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

//Consume el servicio de la API

namespace Rigid.Services
{
    public class DtoolsApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public DtoolsApiService(HttpClient httpClient, string apiUrl)
        {
            _httpClient = httpClient;
            _apiUrl = apiUrl;
        }

        public async Task<T> GetDataAsync<T>(string endpoint, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_apiUrl}/{endpoint}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(result);
            }
            throw new Exception("Error al obtener datos.");
        }

        public async Task PostDataAsync(string endpoint, object data, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_apiUrl}/{endpoint}/data", content);

            if (response.IsSuccessStatusCode)
            {
                throw new Exception("Error al enviar datos.");
            }
        }
    }
}
