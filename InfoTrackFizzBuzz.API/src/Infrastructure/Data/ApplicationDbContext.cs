using System.Reflection;
using InfoTrackFizzBuzz.Application.Common.Interfaces;
using InfoTrackFizzBuzz.Domain.Entities;
using InfoTrackFizzBuzz.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InfoTrackFizzBuzz.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<GameRound> GameRounds => Set<GameRound>();
    public DbSet<GameRule> GameRules => Set<GameRule>();
    public DbSet<GameSession> GameSessions => Set<GameSession>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
