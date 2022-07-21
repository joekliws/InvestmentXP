using Investment.Domain.DTOs;
using Investment.Infra.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investment.API.Controllers
{
    [Route("investimentos")]
    [ApiController]
    public class InvestimentosController : ControllerBase
    {
        private readonly IAssetService _service;

        public InvestimentosController(IAssetService service)
        {
            _service = service;
        }

        [HttpPost("comprar")]
        public async Task<ActionResult> BuyAsset(AssetCreateDTO asset)
        {
            bool bought = await _service.Buy(asset);
            if (bought) return Ok();

            return BadRequest();
        }
    }
}
