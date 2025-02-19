using Microsoft.AspNetCore.Mvc;
using Rigid.Services;
using Rigid.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

[ApiController]
[Route("api/dtools")] 
public class DtoolsController : ControllerBase
{
    private readonly IDtoolsApiService _dtoolsApiService;


    //Inyeccion de la interfaz 
    public DtoolsController(IDtoolsApiService dtoolsApiService)
    {
        _dtoolsApiService = dtoolsApiService;
    }

    //Endpoints 
    //Endpoint para obtener los datos de la facturacion 

    [HttpGet("billing")]
    public async Task<IActionResult> GetBilling([FromHeader] string authorization)
    {
        var token = authorization.Replace("Bearer ", "");
        var billingData = await _dtoolsApiService.GetBillingAsync(token);
        return Ok(billingData);
    }


    //Endpoint para registrar los datos de la facturacion 

    [HttpPost("billing")]
    public async Task<IActionResult> PostBilling([FromBody] Billing billing, [FromHeader] string authorization)
    {
        var token = authorization.Replace("Bearer ", "");
        await _dtoolsApiService.PostBillingAsync(billing, token);
        return CreatedAtAction(nameof(GetBilling), new { id = billing.Id }, billing);
    }

    // Endpoint para obtener licencias

    [HttpGet("licenses")]
    public async Task<IActionResult> GetLicenses([FromHeader] string authorization)
    {
        var token = authorization.Replace("Bearer ", "");
        var licenses = await _dtoolsApiService.GetLicensesAsync(token);
        return Ok(licenses);
    }
    // Endpoint para registrar licencias

    [HttpPost("licenses")]
    public async Task<IActionResult> PostLicense([FromBody] License license, [FromHeader] string authorization)
    {
        var token = authorization.Replace("Bearer ", "");
        await _dtoolsApiService.PostLicenseAsync(license, token);
        return CreatedAtAction(nameof(GetLicenses), new { id = license.Id }, license);
    }

    // Endpoint para obtener documentos

    [HttpGet("documents")]
    public async Task<IActionResult> GetDocuments([FromHeader] string authorization)
    {
        var token = authorization.Replace("Bearer ", "");
        var documents = await _dtoolsApiService.GetDocumentsAsync(token);
        return Ok(documents);
    }
    // Endpoint para registrar documentos

    [HttpPost("documents")]
    public async Task<IActionResult> PostDocument([FromBody] Document document, [FromHeader] string authorization)
    {
        var token = authorization.Replace("Bearer ", "");
        await _dtoolsApiService.PostDocumentAsync(document, token);
        return CreatedAtAction(nameof(GetDocuments), new { id = document.Id }, document);
    }
}
