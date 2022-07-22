﻿using Investment.Domain.DTOs;
using Investment.Domain.Helpers;
using Investment.Infra.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investment.API.Controllers
{
    [Route("conta")]
    [ApiController]
    [Authorize]
    public class ContasController : ControllerBase
    {
        private readonly IAccountService _service;

        public ContasController(IAccountService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AccountReadDTO>> CreateAccount (AccountCreateDTO request)
        {
            string url = $@"{Request.Scheme}://{Request.Host}";
            var response = await _service.CreateAccount(request);
            return Created(url, response);
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
            await _service.Deposit(operation);
            return Ok(new {message = "Operação realizada com sucesso"});
        }

        [HttpPost("saque")]
        public async Task<ActionResult> Withdraw(Operation operation)
        {
            await _service.Withdraw(operation);
            return Ok(new { message = "Operação realizada com sucesso" });
        }
    }
}
