﻿namespace Accounts.Infrastructure.Data;

using Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Accounts");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
