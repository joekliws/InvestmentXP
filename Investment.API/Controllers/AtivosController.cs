using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investment.API.Controllers
{
    [Route("ativos")]
    [ApiController]
    public class AtivosController : ControllerBase
    {
        [HttpGet("{cod-cliente}")]
        public async Task<ActionResult> GetAssetsByCustomer(int customerId)
        {
            return Ok();
        }

        [HttpGet("{cod-ativo}")]
        public async Task<ActionResult> GetAssetsById(int assetId)
        {
            return Ok();
        }

    }
}
