namespace Accounts.Infrastructure.Extenstions;

using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Register other infrastructure services, e.g., repositories
        // services.AddScoped<IRepository, Repository>();

        return services;
    }
}