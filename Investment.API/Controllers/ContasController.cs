using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investment.API.Controllers
{
    [Route("conta")]
    [ApiController]
    public class ContasController : ControllerBase
    {

        [HttpGet("{cod-cliente}")]
        public async Task<ActionResult> GetCustomerById (int codCliente, decimal caldo)
        {
            return Ok();
        }

        
        [HttpPost("deposito")]
        public async Task<ActionResult> Deposit(decimal depositValue)
        {
            return Ok(depositValue);
        }

        [HttpPost("saque")]
        public async Task<ActionResult> Withdraw(decimal depositValue)
        {
            return Ok(depositValue);
        }
    }
}
