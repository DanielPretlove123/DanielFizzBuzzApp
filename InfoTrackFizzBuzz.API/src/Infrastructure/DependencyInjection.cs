using InfoTrackFizzBuzz.Application.Common.Interfaces;
using InfoTrackFizzBuzz.Application.Interfaces;
using InfoTrackFizzBuzz.Domain.Constants;
using InfoTrackFizzBuzz.Infrastructure.Data;
using InfoTrackFizzBuzz.Infrastructure.Data.Interceptors;
using InfoTrackFizzBuzz.Infrastructure.Identity;
using InfoTrackFizzBuzz.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("InfoTrackFizzBuzzDb");
        Guard.Against.Null(connectionString, message: "Connection string 'InfoTrackFizzBuzzDb' not found.");

        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });


        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        builder.Services.AddScoped<ApplicationDbContextInitialiser>();

        builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        builder.Services.AddScoped<IGameSessionRepository, GameSessionRepository>();
        builder.Services.AddScoped<IGameRuleRepository, GameRuleRepository>();
        builder.Services.AddScoped<IGameRoundRepository, GameRoundRepository>();

        builder.Services.AddSingleton(TimeProvider.System);
    }
}
