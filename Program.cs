using Microsoft.EntityFrameworkCore;
using BankingApp.Models;
using BankingApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BankingAppContext>(opt => opt.UseInMemoryDatabase("BankingApp"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<BankingAppContext>();

    if (!context.Users.Any())
    {
        context.Users.AddRange(
            new User { Id = 1, Username = "user1", Password = "password1" },
            new User { Id = 2, Username = "user2", Password = "password2" }
        );
        context.Accounts.AddRange(
            new Account { Id = 1, UserId = 1, IsCheckingAccount = true },
            new Account { Id = 2, UserId = 1, IsCheckingAccount = false },
            new Account { Id = 3, UserId = 2, IsCheckingAccount = true },
            new Account { Id = 4, UserId = 2, IsCheckingAccount = false }
        );

        context.SaveChanges();
    }
}

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
