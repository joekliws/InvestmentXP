using Investment.Domain.DTOs;
using Investment.Infra.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investment.API.Controllers
{
    [Route("ativos")]
    [ApiController]
    [Authorize]
    public class AtivosController : ControllerBase
    {
        private readonly IAssetService _service;

        public AtivosController(IAssetService service)
        {
            _service = service;
            
        }

        [HttpGet("cliente/{cod-cliente}")]
        public async Task<ActionResult> GetAssetsByCustomer()
        {
            int.TryParse(Request.RouteValues["cod-cliente"].ToString(), out int customerId);
            var response = await _service.GetAssetsByCustomer(customerId);
            return Ok(response);
        }

        [HttpGet("{cod-ativo}")]
        public ActionResult GetAssetsById()
        {
            int.TryParse(Request.RouteValues["cod-ativo"].ToString(), out int assetId);
            var response = _service.GetAssetById(assetId);
            return Ok(response);
        }



    }
}
