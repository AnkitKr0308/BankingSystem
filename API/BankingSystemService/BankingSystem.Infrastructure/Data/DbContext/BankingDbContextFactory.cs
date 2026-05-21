using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; 
using Microsoft.Extensions.Configuration.Json; 

namespace BankingSystem.Infrastructure.Data.DbContext
{
    public class BankingDbContextFactory : IDesignTimeDbContextFactory<BankingDbContext>
    {
        public BankingDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("BankingSystem");

            var optionsBuilder = new DbContextOptionsBuilder<BankingDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            var context = new BankingDbContext(optionsBuilder.Options);

            return context;
        }
    }
}
