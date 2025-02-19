using Rigid.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rigid.Services
{
    public interface IDtoolsApiService
    {

        //INTERFAZ DE LA API DE DTOOLS
        Task<List<Billing>> GetBillingAsync(string token);
        Task PostBillingAsync(Billing billing, string token);
        Task<List<License>> GetLicensesAsync(string token);
        Task PostLicenseAsync(License license, string token);
        Task<List<Document>> GetDocumentsAsync(string token);
        Task PostDocumentAsync(Document document, string token);
    }
}
