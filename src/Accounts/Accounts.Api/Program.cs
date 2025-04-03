using System.Configuration;
using System.Reflection;
using FluentValidation;
using Accounts.Api.EndPoints;
using Accounts.Api.Extensions;
using Accounts.Application.Handlers.CommandHandlers;
using Accounts.Infrastructure.Configuration;
using Accounts.Infrastructure.Data;
using Accounts.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddConfiguredSwagger();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(OpenAccountCommandHandler).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    var keycloakOptions = builder.Configuration.GetSection("Keycloak")
        .Get<KeycloakOptions>() ?? throw new ConfigurationErrorsException();

    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
        {
            c.OAuthClientId(keycloakOptions.ClientId);
            c.OAuthClientSecret(keycloakOptions.ClientSecret);
        }
    );

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.RegisterAccountEndpoints();

app.Run();