using Microsoft.EntityFrameworkCore;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Enums;
using Stofipy.DAL.Tests.Common;
using Stofipy.DAL.Tests.Seeds;
using Xunit.Abstractions;

namespace Stofipy.DAL.Tests;

public class AlbumTests (ITestOutputHelper output): DbContextTestsBase(output)
{
    [Fact]
    public async Task AddNewEmpty_Album_Persisted()
    {
        //Arrange
        AlbumEntity albumEntity = new()
        {
            Id = Guid.Parse("910B9FB2-D88F-40E8-BFDD-87106D9DBFBD"),
            AlbumName = "New Album",
            Description = "New Description",
            Author = new AuthorEntity
            {
                Id = Guid.Parse("21975F27-99B8-4415-9298-BEC4A9DC8815"),
                AuthorName = "",
            },
            AuthorId = Guid.Parse("21975F27-99B8-4415-9298-BEC4A9DC8815"),
        };

        //Act
        DbContextSUT.Albums.Add(albumEntity);
        await DbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Albums
            .Include(entity => entity.Author)
            .SingleAsync(i => i.Id == albumEntity.Id);
        DeepAssert.Equal(albumEntity, actualEntities);
        Assert.NotNull(actualEntities.Author);
    }

    [Fact]
    public async Task Update_Album_Persisted()
    {
        //Arrange
        var baseEntity = AlbumTestSeeds.AlbumForUpdate;
        var updatedEntity = AlbumTestSeeds.AlbumForUpdate with
        {
            AlbumName = baseEntity.AlbumName + "Updated",
            Description = baseEntity.Description + "Updated",
            Author = null!
        };
        
        //Act
        DbContextSUT.Albums.Update(updatedEntity);
        await DbContextSUT.SaveChangesAsync();
        
        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Albums.SingleAsync(i => i.Id == baseEntity.Id);
        DeepAssert.Equal(updatedEntity, actualEntity);
    }
    
    [Fact]
    public async Task GetById_Album()
    {
        //Act
        var entity = await DbContextSUT.Albums
            .SingleAsync(i => i.Id == AlbumTestSeeds.BasicAlbum.Id);
    
        //Assert
        DeepAssert.Equal(AlbumTestSeeds.BasicAlbum with { Author = null!, FilesInAlbums = []}, entity);
    }
    
    [Fact]
    public async Task GetById_IncludingAuthorAndAlbum()
    {
        //Act
        var actualEntity = await DbContextSUT.Albums
            .Include(e => e.Author)
            .Include(e => e.FilesInAlbums)
            .ThenInclude(e => e.File)
            .SingleAsync(i => i.Id == AlbumTestSeeds.BasicAlbum.Id);
    
        var expected = AlbumTestSeeds.BasicAlbum;
    
        //Assert
        DeepAssert.Equal(expected with { Author = null!, FilesInAlbums = []}, AlbumTestSeeds.BasicAlbum with { Author = null!, FilesInAlbums = []});
        Assert.Equal(expected.Author.Id, actualEntity.Author.Id);
        Assert.Equal(actualEntity.FilesInAlbums.Single().File.Id, FileTestSeeds.FileInAlbum.Id);
    }
    
    [Fact]
    public async Task Delete_Album_Deleted()
    {
        //Arrange
        var baseEntityAuthor = AlbumTestSeeds.AlbumForDelete.Author;
        var baseEntity = AlbumTestSeeds.AlbumForDelete with{Author = null!};
        
        //Act
        DbContextSUT.Albums.Remove(baseEntity);
        await DbContextSUT.SaveChangesAsync();
        
        //Assert
        Assert.False(await DbContextSUT.Albums.AnyAsync(i => i.Id == baseEntity.Id));
        Assert.True(await DbContextSUT.Authors.AnyAsync(i => i.Id == baseEntityAuthor.Id));
    }
    
    [Fact]
    public async Task DeleteById_Album_Deleted()
    {
        //Arrange
        var baseEntityAuthor = AlbumTestSeeds.AlbumForDelete.Author;
        var baseEntity = AlbumTestSeeds.AlbumForDelete;
        
        //Act
        DbContextSUT.Albums.Remove(DbContextSUT.Albums.Single(i => i.Id == baseEntity.Id));
        await DbContextSUT.SaveChangesAsync();
        
        //Assert
        Assert.False(await DbContextSUT.Albums.AnyAsync(i => i.Id == baseEntity.Id));
        Assert.True(await DbContextSUT.Authors.AnyAsync(i => i.Id == baseEntityAuthor.Id));
    }
    [Fact]
    public async Task Delete_AlbumWithFiles_NotDeleted()
    {
        //Arrange
        var baseEntity = AlbumTestSeeds.BasicAlbum with{Author = null!, FilesInAlbums = []};
        
        // Act & Assert
        await Assert.ThrowsAsync<DbUpdateException>(async () =>
        {
            DbContextSUT.Albums.Remove(baseEntity);
            await DbContextSUT.SaveChangesAsync();
        });
    }
}