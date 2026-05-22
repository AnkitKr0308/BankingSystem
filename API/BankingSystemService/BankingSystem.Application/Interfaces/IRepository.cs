using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
        Task AddAsync(T entity);
        void Update (T entity);
        void Delete(T entity);
        //Task SaveChangesAsync();
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        //Task BeginTransactionAsync();
        //Task CommitAsync();
        //Task RollbackAsync();

    }
}
