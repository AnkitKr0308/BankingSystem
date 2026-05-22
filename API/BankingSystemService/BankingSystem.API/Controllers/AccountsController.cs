using BankingSystem.Application.DTOs.Account;
using BankingSystem.Application.DTOs.Transactions;
using BankingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles ="Admin")]
        [HttpPost("createaccount")]
        public async Task<IActionResult> CreateAccount([FromBody]CreateAccountDTO createAccountDTO) 
        {
            var account = await _accountService.CreateAccountAsync(createAccountDTO);
            return Ok(account);
        }

        [HttpGet("{accountNumber}")]
        public async Task<IActionResult> GetAccountDetail(string accountNumber)
        {
            var account = await _accountService.GetAccountAsync(accountNumber);
            return Ok(account);
        }

        

        [Authorize(Roles ="Admin")]
        [HttpPatch("closeAccount")]
        public async Task<IActionResult> CloseAccount(string accountNumber)
        {
            await _accountService.CloseAccount(accountNumber);
            return Ok($"Account {accountNumber} deleted successfully");
        }
       
    }
}
