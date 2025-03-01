using Microsoft.EntityFrameworkCore;
using Sotify.DAL.Entities;

namespace Sotify.DAL;

public class StofipyDbContext(DbContextOptions contextOptions, bool seedData) : DbContext(contextOptions)
{
    public DbSet<AlbumEntity> Albums { get; set; }
    public DbSet<AuthorEntity> Authors { get; set; }
    public DbSet<FileEntity> Files { get; set; }
    public DbSet<FilesInAlbumEntity> FilesInAlbums { get; set; }
    public DbSet<FilesInPlaylists> FilesInPlaylists { get; set; }
    public DbSet<PlaylistEntity> Playlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (seedData)
        {
            optionsBuilder
                .UseSeeding((context, _) =>
                {
                    context.SaveChanges();
                })
                .UseAsyncSeeding(async (context, _, cancellationToken) =>
                {
                    await context.SaveChangesAsync(cancellationToken);
                });
        }
    }
}