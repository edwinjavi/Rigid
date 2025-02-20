using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Rigid.Models;

namespace Rigid.Services
{
    public class DtoolsApiService : IDtoolsApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://api.dtools.com/api/v1";

        public DtoolsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // MÉTODO ACTUALIZADO PARA OBTENER FACTURACIÓN
        public async Task<List<Billing>> GetBillingAsync(string token, string search = null, int page = 1, int pageSize = 20)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var url = $"{_apiUrl}/PurchaseOrders/GetPurchaseOrders?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(search))
                url += $"&search={Uri.EscapeDataString(search)}";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var purchaseOrders = JsonSerializer.Deserialize<PurchaseOrdersResponse>(result);
                return purchaseOrders?.PurchaseOrders ?? new List<Billing>();
            }
            throw new Exception("Error al obtener facturación.");
        }

        public async Task PostBillingAsync(Billing billing, string token)
        {
            throw new NotImplementedException("No se ha implementado el POST para PurchaseOrders.");
        }

        // MÉTODOS PARA LICENCIAS
        public async Task<List<License>> GetLicensesAsync(string token, string search = null, int page = 1, int pageSize = 20)
        {
            return await GetDataAsync<List<License>>("ServiceContracts/GetServiceContracts", token);
        }

        public async Task PostLicenseAsync(License license, string token)
        {
            throw new NotImplementedException("No se ha implementado el POST para GetServiceContracts.");
        }

        // MÉTODOS PARA DOCUMENTOS
        public async Task<List<Document>> GetDocumentsAsync(string token)
        {
            // Implementación para obtener documentos
            return await GetDataAsync<List<Document>>("Files/GetFile", token);
        }

        public async Task<Document> GetFileAsync(string id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_apiUrl}/Files/GetFile?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Document>(result);
            }
            throw new Exception($"Error al obtener el archivo con ID {id}.");
        }

        public async Task PostDocumentAsync(Document document, string token)
        {
            await PostDataAsync("Files", document, token);
        }

        // MÉTODOS GENERALES PARA GET Y POST
        private async Task<T> GetDataAsync<T>(string endpoint, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_apiUrl}/{endpoint}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(result);
            }
            throw new Exception($"Error al obtener datos de {endpoint}.");
        }

        private async Task PostDataAsync(string endpoint, object data, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonSerializer.Serialize(data), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiUrl}/{endpoint}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al enviar datos a {endpoint}.");
            }
        }
    }

    // Clase para deserializar la respuesta de la API
    public class PurchaseOrdersResponse
    {
        public List<Billing> PurchaseOrders { get; set; }
    }
}
