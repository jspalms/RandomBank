using System.Reflection;
using FluentValidation;
using Accounts.Api.EndPoints;
using Accounts.Api.Extensions;
using Accounts.Application.Handlers.CommandHandlers;
using Accounts.Infrastructure.Data;
using Accounts.Infrastructure.Extensions;
using Accounts.Api.Models.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<KeycloakOptions>(builder.Configuration.GetSection("Keycloak"));
var keycloakOptions = builder.Configuration.GetSection("Keycloak").Get<KeycloakOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(jwtOptions =>
{
    jwtOptions.Authority = keycloakOptions.Authority;
    jwtOptions.Audience = keycloakOptions.ClientId;
    jwtOptions.MetadataAddress = keycloakOptions.MetadataAddress;
    jwtOptions.RequireHttpsMetadata = false;
});

builder.Services.AddAuthorization();

builder.Services.AddConfiguredSwagger();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(OpenAccountCommandHandler).Assembly));
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
        {
            c.OAuthClientId(keycloakOptions.ClientId); // Add your client ID here
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