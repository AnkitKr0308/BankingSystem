using BankingSystem.Application.Interfaces;
using BankingSystem.Application.Services;
using BankingSystem.Infrastructure.Data;
using BankingSystem.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using BankingSystem.Application.Validators;
using BankingSystem.Application.Validators.Transaction;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding DbContext and connection string
builder.Services.AddDbContext<BankingDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("BankingSystem")));


// Injecting Validators
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddValidatorsFromAssemblyContaining<TransactionValidator>();

// Adding Dependency Injection
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
