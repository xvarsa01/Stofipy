using DotVVM.Framework.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stofipy.AppDotVVM;
using Stofipy.DAL.Migrator;
using Stofipy.DAL.Seeds;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddAuthentication();
        builder.Services.AddDotVVM<DotvvmStartup>();

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

    private static void MigrateDb(IDbMigrator migrator) => migrator.Migrate();
    private static void SeedDb(IDbSeeder dbSeeder) => dbSeeder.Seed();
}


