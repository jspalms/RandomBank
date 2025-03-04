using System.Reflection;
using FluentValidation;
using Accounts.Api.EndPoints;
using Accounts.Api.Extensions;
using Accounts.Application.Handlers.CommandHandlers;
using Accounts.Infrastructure.Data;
using Accounts.Infrastructure.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfiguredSwagger();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(OpenAccountCommandHandler).Assembly));
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();

}

app.UseHttpsRedirection();

app.RegisterAccountEndpoints();

app.Run();