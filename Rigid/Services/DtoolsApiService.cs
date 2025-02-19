using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Rigid.Models;
using System.Collections.Generic;

namespace Rigid.Services
{

    //modificacion para que se use la interfaz IDtoolsApiService
    public class DtoolsApiService : IDtoolsApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://api.dtools.com"; // Define la URL bien, ya que es un valor constante es mejor setearlo aqui en ves de pasarlo como constructor

        public DtoolsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //Se comento por las razones de arriba _apiUrl = "https://api.dtools.com";

        }

        //Billing, Documentation y Licenses

        public async Task<List<Billing>> GetBillingAsync(string token)
        {
            return await GetDataAsync<List<Billing>>("billing", token);
        }

        public async Task PostBillingAsync(Billing billing, string token)
        {
            await PostDataAsync("billing", billing, token);
        }

        public async Task<List<License>> GetLicensesAsync(string token)
        {
            return await GetDataAsync<List<License>>("licenses", token);
        }

        public async Task PostLicenseAsync(License license, string token)
        {
            await PostDataAsync("licenses", license, token);
        }

        public async Task<List<Document>> GetDocumentsAsync(string token)
        {
            return await GetDataAsync<List<Document>>("documents", token);
        }

        public async Task PostDocumentAsync(Document document, string token)
        {
            await PostDataAsync("documents", document, token);
        }


        //Codigo que estaba antes (No tocado)
        //Metodos para obtener y enviar datos de la API

        private async Task<T> GetDataAsync<T>(string endpoint, string token)
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

        private async Task PostDataAsync(string endpoint, object data, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_apiUrl}/{endpoint}/data", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al enviar datos.");
            }
        }
    }
}
