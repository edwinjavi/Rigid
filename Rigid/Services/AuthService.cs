using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rigid.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public AuthService(HttpClient httpClient, string apiUrl)
        {
            _httpClient = httpClient;
            _apiUrl = apiUrl;
        }

        //Token de autenticación
        public async Task<string> GetTokenAsync(string username, string password)
        {
            var loginData = new
            {
                username,
                password
            };

            var content = new StringContent(
                JsonSerializer.Serialize(loginData),
                Encoding.UTF8, 
                "application/json"
                );

            var response = await _httpClient.PostAsync($"{_apiUrl}/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<dynamic>(result);
                return tokenResponse["token"].ToString();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception("Error al autenticarse.");
        }
    }
}
