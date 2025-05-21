using System.Reflection;
using CommunityToolkit.Maui;
using CookBook.DAL.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stofipy.App.ViewModels;
using Stofipy.App.Views;
using Stofipy.BL;
using Stofipy.DAL;

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
            
        builder.Services
            .AddAppServices()
            .AddBLServices()
            .AddDALServices(GetDALOptions(builder.Configuration));
            
        return builder.Build();
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
    
    private static DALOptions GetDALOptions(IConfiguration configuration)
    {
        DALOptions dalOptions = new()
        {
            DatabaseDirectory = FileSystem.AppDataDirectory
        };
        configuration.GetSection("Stofipy:DAL").Bind(dalOptions);
        return dalOptions;
    }
}