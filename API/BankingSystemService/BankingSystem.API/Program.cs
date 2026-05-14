using BankingSystem.Application.Interfaces;
using BankingSystem.Application.Services;
using BankingSystem.Infrastructure.Data;
using BankingSystem.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding DbContext and connection string
builder.Services.AddDbContext<BankingDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("BankingSystem")));

// Adding Dependency Injection
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

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
