using Rigid.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rigid.Services
{
    public interface IDtoolsApiService
    {
        // Métodos para obtener y registrar facturas, licencias y documentos
        Task<List<Billing>> GetBillingAsync(string token, string search = null, int page = 1, int pageSize = 20);
        Task PostBillingAsync(Billing billing, string token);
        Task<List<License>> GetLicensesAsync(string token, string search = null, int page = 1, int pageSize = 20);
        Task<Document> GetFileAsync(string id, string token); // Método para obtener un archivo específico

    }
}
