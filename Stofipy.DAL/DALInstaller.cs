using CookBook.DAL.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stofipy.DAL.Factories;
using System.Reflection;
using Stofipy.DAL.Repositories;

namespace Stofipy.DAL;

public static class DALInstaller
{
    public static IServiceCollection AddDALServices(this IServiceCollection services, DALOptions options)
    {
        services.AddSingleton(options);

        if (options is null)
        {
            throw new InvalidOperationException("No persistence provider configured");
        }

        if (string.IsNullOrEmpty(options.DatabaseDirectory))
        {
            throw new InvalidOperationException($"{nameof(options.DatabaseDirectory)} is not set");
        }
        if (string.IsNullOrEmpty(options.DatabaseName))
        {
            throw new InvalidOperationException($"{nameof(options.DatabaseName)} is not set");
        }

        services.AddSingleton<IDbContextFactory<StofipyDbContext>>(_ =>
            new DbContextSqLiteFactory($"Data Source={options.DatabaseFilePath}", options.SeedDemoData));
        services.AddDbContext<StofipyDbContext>(contextOptions =>
            contextOptions.UseSqlite($"Data Source={options.DatabaseFilePath}"));
        
        services.Scan(scan => scan
            .FromAssemblyOf<StofipyDbContext>()
            .AddClasses(c => c.AssignableTo(typeof(IRepository<>))
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition))
            .AsSelf()                 // Register concrete type
            .AsImplementedInterfaces() // Register as IRepository<T>
            .WithSingletonLifetime());

        // services.AddSingleton<IDbMigrator, DbMigrator>();
        // services.AddSingleton<IDbSeeder, DbSeeder>();

        return services;
    }
}
