using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entity;
using BankingSystem.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BankingDbContext _context;
        private readonly DbSet<T> _dbSet;
        private IDbContextTransaction _transaction;
        public Repository(BankingDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        //public  Task SaveChangesAsync()
        //{
        //    return _context.SaveChangesAsync();
        //}

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        //public async Task BeginTransactionAsync()
        //{
        //    _transaction = await _context.Database.BeginTransactionAsync();
        //}

        //public async Task CommitAsync()
        //{
        //    await _transaction.CommitAsync();
        //    await _transaction.DisposeAsync();
        //}

        //public async Task RollbackAsync()
        //{
        //    await _transaction.RollbackAsync();
        //    await _transaction.DisposeAsync();
        //}

    }
}
