using BankingSystem.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using BankingSystem.Domain.Entity.Authentication;

namespace BankingSystem.Infrastructure.Data.DbContext
{
    public class BankingDbContext : IdentityDbContext<ApplicationUser>
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options) 
        {
       
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

        public DbSet<Transaction> Transactions=> Set<Transaction>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.Property(x => x.Balance)
                      .HasColumnType("decimal(18,2)");

                entity.Property(x => x.AccountNumber)
                .IsRequired()
                .HasDefaultValueSql("NEXT VALUE FOR AccountNumberSequence");
            });


            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)");

                entity.Property(x=>x.TransactionID)
                .IsRequired()
                .HasDefaultValueSql("NEXT VALUE FOR TransactionIdSequence");
            });
                

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(x => x.Address)
                .HasMaxLength(250);

                entity.Property(x => x.PhoneNumber)
                .HasMaxLength(10);

                entity.Property(x => x.ZipCode)
                 .HasMaxLength(6);

                entity.Property(x => x.CustomerId)
                .HasDefaultValueSql("NEXT VALUE FOR CustomerIdSequence")
                .IsRequired();
            });

            modelBuilder.HasSequence<long>("CustomerIdSequence")
                .StartsAt(100000)
                .IncrementsBy(1);

            modelBuilder.HasSequence<long>("AccountNumberSequence")
                .StartsAt(10800000)
                .IncrementsBy(1);

            modelBuilder.HasSequence<long>("TransactionIdSequence")
                .StartsAt(100000000)
                .IncrementsBy(1);

        }
    }
}
