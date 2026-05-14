using BankingSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Infrastructure.Data
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options) 
        {
       
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

        public DbSet<Transaction> Transactions=> Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>()
                .Property(x => x.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaction>()
                .Property(x => x.Amount)
                .HasColumnType("decimal(18,2)");
        }
    }
}
