using BankingSystem.Application.Interfaces;
using BankingSystem.Infrastructure.Data.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace BankingSystem.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankingDbContext _bankingDbContext;
        private IDbContextTransaction _transaction;
        public UnitOfWork(BankingDbContext bankingDbContext)
        {
            _bankingDbContext = bankingDbContext;

        }
        public async Task BeginTransactionAsync()
        {
            _transaction=await _bankingDbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }

        public Task SaveChangesAsync()
        {
            return _bankingDbContext.SaveChangesAsync();
        }
    }
}
