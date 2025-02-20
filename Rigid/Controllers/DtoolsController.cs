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

    // Inyección de la interfaz
    public DtoolsController(IDtoolsApiService dtoolsApiService)
    {
        _dtoolsApiService = dtoolsApiService;
    }

    // Endpoint para obtener facturación PurchaseOrders
    [HttpGet("billing")]
    public async Task<IActionResult> GetBilling(
        [FromHeader] string authorization,
        [FromQuery] string search = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var token = authorization.Replace("Bearer ", "");
        var billingData = await _dtoolsApiService.GetBillingAsync(token, search, page, pageSize);
        return Ok(billingData);
    }

    // 🔹 Endpoint para obtener licencias GetServiceContracts
    [HttpGet("licenses")]
    public async Task<IActionResult> GetLicenses(
        [FromHeader] string authorization,
        [FromQuery] string search = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var token = authorization.Replace("Bearer ", "");
        var licenses = await _dtoolsApiService.GetLicensesAsync(token, search, page, pageSize);
        return Ok(licenses);
    }


    // 🔹 Endpoint para documentos GetFiles
    [HttpGet("documents/{id}")]
    public async Task<IActionResult> GetFile(string id, [FromHeader] string authorization)
    {
        var token = authorization.Replace("Bearer ", "");
        var file = await _dtoolsApiService.GetFileAsync(id, token);
        return Ok(file);
    }


}
