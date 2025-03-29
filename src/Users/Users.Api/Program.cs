using System.Configuration;
using Users.Api.Endponts;
using Users.Api.Extensions;
using Users.Application.Handlers.CommandHandlers;
using Users.Infrastructure.Configuration;
using Users.Infrastructure.Data;
using Users.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddConfiguredSwagger();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateUserCommandHandler).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var keycloakOptions = builder.Configuration.GetSection("Keycloak")
        .Get<KeycloakOptions>() ?? throw new ConfigurationErrorsException();
    app.UseSwagger();
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

// app.UseHttpsRedirection();

app.RegisterUsersEndpoints();
app.Run();
