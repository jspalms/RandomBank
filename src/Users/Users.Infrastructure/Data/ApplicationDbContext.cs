using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain;
using Users.Domain.Entities;
using Users.Infrastructure.Events.DomainEvents;

namespace Users.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users => Set<User>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Users");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        //Generic method to apply soft delete filter isn't nice so don't at the derived class level
      
    }
    
}