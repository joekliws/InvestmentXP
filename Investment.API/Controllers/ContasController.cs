using Investment.Domain.Helpers;
using Investment.Infra.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investment.API.Controllers
{
    [Route("conta")]
    [ApiController]
    public class ContasController : ControllerBase
    {
        private readonly IAccountService _service;

        public ContasController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet("{cod-cliente}")]
        public async Task<ActionResult> GetCustomerById ()
        {
            int.TryParse(Request.RouteValues["cod-cliente"].ToString(), out int customerId);
            var response = await _service.GetBalance(customerId);
            return Ok(response);
        }

        
        [HttpPost("deposito")]
        public async Task<ActionResult> Deposit(Operation operation)
        {
            bool deposited = await _service.Deposit(operation);
            return Ok();
        }

        [HttpPost("saque")]
        public async Task<ActionResult> Withdraw(Operation operation)
        {
            bool withdrawn = await _service.Withdraw(operation);
            return Ok();
        }
    }
}
