using BankingSystem.Application.DTOs.Transactions;
using BankingSystem.Application.Interfaces;
using BankingSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> DepositAmount([FromBody] CreateTransactionDTO depositDTO)
        {
            await _transactionService.DepositAsync(depositDTO);
            return Ok();
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> WithdrawAmount([FromBody] CreateTransactionDTO withdrawDTO)
        {
            await _transactionService.WithdrawAsync(withdrawDTO);
            return Ok();
        }
    }
}
