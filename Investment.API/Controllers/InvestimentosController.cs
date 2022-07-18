using Investment.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investment.API.Controllers
{
    [Route("investimentos")]
    [ApiController]
    public class InvestimentosController : ControllerBase
    {
        [HttpPost("comprar")]
        public async Task<ActionResult> BuyAsset(AssetCreateDTO asset)
        {
            return Ok();
        }
    }
}
