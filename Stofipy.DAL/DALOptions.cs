namespace Stofipy.DAL;

public record DALOptions
{
    public required string DatabaseDirectory { get; init; }
    public string DatabaseName { get; init; } = null!;
    public string DatabaseFilePath => Path.Combine(DatabaseDirectory, DatabaseName!);
    private static readonly string SystemAppDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public readonly string StofipyAppDataDirectory = Path.Combine(SystemAppDataDirectory, "Stofipy");
    public string MediaFilesDirectory => Path.Combine(StofipyAppDataDirectory, "Media");
    public string ContentFilesDirectory => Path.Combine(MediaFilesDirectory, "Content");
    public string ImageFilesDirectory => Path.Combine(MediaFilesDirectory, "Images");
    
    public bool MauiApp = true; 

    /// <summary>
    /// Deletes database before application startup
    /// </summary>
    public bool RecreateDatabaseEachTime { get; init; } = false;

    /// <summary>
    /// Seeds DemoData from DbContext on database creation.
    /// </summary>
    public bool SeedDemoData { get; init; } = false;
}
