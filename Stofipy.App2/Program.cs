using System.IO;
using System.Runtime.CompilerServices;
using DotVVM.Framework.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using Stofipy.App2;
using Stofipy.BL;
using Stofipy.DAL;
using Stofipy.DAL.Migrator;
using Stofipy.DAL.Seeds;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddAuthentication();
        builder.Services.AddDotVVM<DotvvmStartup>();
        
        var options = GetDALOptions(builder.Configuration);
        builder.Services
            // .AddAppServices()
            .AddBLServices(options)
            .AddDALServices(options);

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthorization();

        app.UseDotVVM<DotvvmStartup>();
        app.MapDotvvmHotReload();

        
        //MigrateDb(app.Services.GetRequiredService<IDbMigrator>());
        //SeedDb(app.Services.GetRequiredService<IDbSeeder>());

        app.Run();
    }
    
    private static DALOptions GetDALOptions(IConfiguration configuration, [CallerFilePath] string sourceFilePath = "")
    {
        var relativePath = Path.Combine(Path.GetDirectoryName(sourceFilePath)!,"../Stofipy.DAL");
        DALOptions dalOptions = new()
        {
            DatabaseDirectory = Path.GetFullPath(relativePath),
            MauiApp = false,
        };
        configuration.GetSection("Stofipy:DAL").Bind(dalOptions);
        return dalOptions;
    }

    private static void MigrateDb(IDbMigrator migrator) => migrator.Migrate();
    private static void SeedDb(IDbSeeder dbSeeder) => dbSeeder.Seed();
}


