using System.Reflection;
using System.Runtime.CompilerServices;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stofipy.App.ViewModels;
using Stofipy.App.Views;
using Stofipy.BL;
using Stofipy.DAL;
using Stofipy.DAL.Migrator;
using Stofipy.DAL.Seeds;

namespace Stofipy.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("FuturaStdExtraBold.otf", "FuturaBold");
                fonts.AddFont("CircularSpotifyText-Bold.otf", "Circular");
            });
        
        ConfigureAppSettings(builder);


#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddTransient<PlaylistDetailVM>();
        
        builder.Services.AddSingleton<MainLayout>();
        builder.Services.AddTransient<FilesInQueueVM>();
        builder.Services.AddTransient<ListOfPlaylistsVM>();
            
        builder.Services.AddSingleton<AppShell>();
            
        var options = GetDALOptions(builder.Configuration);
        builder.Services
            .AddAppServices()
            .AddBLServices(options)
            .AddDALServices(options);
            
        var app = builder.Build();
        
        MigrateDb(app.Services.GetRequiredService<IDbMigrator>());
        SeedDb(app.Services.GetRequiredService<IDbSeeder>());
        return app;
    }
    
    private static void ConfigureAppSettings(MauiAppBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder();

        var assembly = Assembly.GetExecutingAssembly();
        const string appSettingsFilePath = "Stofipy.App.appsettings.json";
        using var appSettingsStream = assembly.GetManifestResourceStream(appSettingsFilePath);
        if (appSettingsStream is not null)
        {
            configurationBuilder.AddJsonStream(appSettingsStream);
        }

        var configuration = configurationBuilder.Build();
        builder.Configuration.AddConfiguration(configuration);
    }
    
    private static DALOptions GetDALOptions(IConfiguration configuration, [CallerFilePath] string sourceFilePath = "")
    {
        var relativePath = Path.Combine(Path.GetDirectoryName(sourceFilePath)!,"../Stofipy.DAL");
        DALOptions dalOptions = new()
        {
            DatabaseDirectory = Path.GetFullPath(relativePath)
        };
        configuration.GetSection("Stofipy:DAL").Bind(dalOptions);
        return dalOptions;
    }
    
    private static void MigrateDb(IDbMigrator migrator) => migrator.Migrate();
    private static void SeedDb(IDbSeeder dbSeeder) => dbSeeder.Seed();
}