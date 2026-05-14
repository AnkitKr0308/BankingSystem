using BankingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("createaccount")]
        public async Task<IActionResult> CreateAccount(int customerId)
        {
            var account = await _accountService.CreateAccountAsync(customerId);
            return Ok(account);
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccountDetail(int accountId)
        {
            var account = await _accountService.GetAccountAsync(accountId);
            return Ok(account);
        }

        [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> DepositAmount (int accountId, decimal amount)
        {
            await _accountService.DepositAsync(accountId, amount);
            return Ok();
        }

        [HttpPost("{accountId}/withdraw")]
        public async Task<IActionResult> WithdrawAmount(int accountId, decimal amount)
        {
            await _accountService.WithdrawAsync(accountId, amount);
            return Ok();
        }

       
    }
}
