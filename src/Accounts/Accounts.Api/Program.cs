using System.Configuration;
using FluentValidation;
using System.Reflection;
using Accounts.Api.EndPoints;
using Accounts.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddInfrastructureServices(builder.Configuration.GetConnectionString("PostgresConnection") ?? throw new ConfigurationErrorsException("Missing postgres connection string"));

var app = builder.Build();

// Configure the HTTP request pipeline.



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();

}

app.UseHttpsRedirection();

app.RegisterAccountEndpoints();

app.Run();