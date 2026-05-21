// See https://aka.ms/new-console-template for more information

using BankingSystem.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using BankingSystem.Domain.Entity;
using BankingSystem.Tools;
using System.IO;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


builder.Services.AddDbContext<BankingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankingSystem"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BankingDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<MigrationSeeder>();

var app = builder.Build();

using var scope = app.Services.CreateScope();

var seeder = scope.ServiceProvider.GetRequiredService<MigrationSeeder>();
await seeder.SeedRolesAsync();

Console.WriteLine("Roles seeding completed successfully.");
