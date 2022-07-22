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
            await _service.Buy(asset);
            return Ok(new {message = "Compra efetuada com sucesso"});
        }

        [HttpPost("vender")]
        public async Task<ActionResult> SellAsset(AssetCreateDTO asset)
        {
            await _service.Sell(asset);
            return Ok(new { message = "Venda efetuada com sucesso" });

        }
    }
}
