using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entity;
using BankingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Infrastructure.Repository
{
    public class AccountRepository : Repository<BankAccount>, IAccountRepository
    {
        private readonly BankingDbContext _context;
        public AccountRepository(BankingDbContext context) : base(context)
        {
            _context=context;
        }
        public Task<BankAccount?> GetAccountDetailsAsync(int accountId)
        {
            var account = _context.BankAccounts
                .Include(a=>a.Customer)
                .Include(a=>a.Transactions)
                .FirstOrDefaultAsync(a=>a.Id == accountId);

            return account;
        }
    }
}
