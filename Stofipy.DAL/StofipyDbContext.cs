using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;

namespace Stofipy.DAL;

public class StofipyDbContext(DbContextOptions contextOptions, bool seedData) : DbContext(contextOptions)
{
    public DbSet<AlbumEntity> Albums { get; set; }
    public DbSet<AuthorEntity> Authors { get; set; }
    public DbSet<FileEntity> Files { get; set; }
    public DbSet<FilesInAlbumEntity> FilesInAlbums { get; set; }
    public DbSet<FilesInPlaylists> FilesInPlaylists { get; set; }
    public DbSet<PlaylistEntity> Playlists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AuthorEntity>()
            .HasMany(e => e.Files)
            .WithOne(e => e.Author)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<AuthorEntity>()
            .HasMany(e => e.Albums)
            .WithOne(e => e.Author)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<AlbumEntity>()
            .HasMany(e => e.FilesInAlbums)
            .WithOne(e => e.Album)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<FileEntity>()
            .HasMany(e => e.FilesInAlbums)
            .WithOne(e => e.File)
            .OnDelete(DeleteBehavior.Restrict);
    }

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