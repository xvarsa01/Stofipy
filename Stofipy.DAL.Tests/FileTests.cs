using Microsoft.EntityFrameworkCore;
using Stofipy.Common.Tests;
using Stofipy.Common.Tests.Seeds;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Enums;
using Xunit.Abstractions;

namespace Stofipy.DAL.Tests;

public class FileTests (ITestOutputHelper output): DbContextTestsBase(output)
{
    [Fact]
    public async Task AddNew_File_Persisted()
    {
        //Arrange
        FileEntity fileEntity = new()
        {
            Id = Guid.Parse("B3C79C0D-25AA-4ECE-8F3D-F95F31F076DB"),
            FileName = "Test File",
            Description = "Test Description",
            Size = 0,
            Length = 0,
            Category = Category.Pop,
            Author = new AuthorEntity
            {
                Id = Guid.Parse("C8420D58-010C-4FBD-9904-59045DE27562"),
                AuthorName = "",
                ProfilePicture = null,
            },
            AuthorId = Guid.Parse("C8420D58-010C-4FBD-9904-59045DE27562"),
        };

        //Act
        DbContextSUT.Files.Add(fileEntity);
        await DbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Files
            .Include(entity => entity.Author)
            .SingleAsync(i => i.Id == fileEntity.Id);
        DeepAssert.Equal(fileEntity, actualEntities);
        Assert.NotNull(actualEntities.Author);
    }

    [Fact]
    public async Task Update_File_Persisted()
    {
        //Arrange
        var baseEntity = FileTestSeeds.FileForUpdate;
        var updatedEntity = FileTestSeeds.FileForUpdate with
        {
            FileName = baseEntity.FileName + "Updated",
            Description = baseEntity.Description + "Updated",
            Lyrics = baseEntity.Lyrics + "Updated",
            Size = baseEntity.Size + 100,
            Length = baseEntity.Length + 100,
            Author = null!
        };
        
        //Act
        DbContextSUT.Files.Update(updatedEntity);
        await DbContextSUT.SaveChangesAsync();
        
        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Files.SingleAsync(i => i.Id == baseEntity.Id);
        DeepAssert.Equal(updatedEntity, actualEntity);
    }

    [Fact]
    public async Task Delete_File_Deleted()
    {
        //Arrange
        var baseEntityAuthor = FileTestSeeds.FileForDelete.Author;
        var baseEntity = FileTestSeeds.FileForDelete with{Author = null!};
        
        //Act
        DbContextSUT.Files.Remove(baseEntity);
        await DbContextSUT.SaveChangesAsync();
        
        //Assert
        Assert.False(await DbContextSUT.Files.AnyAsync(i => i.Id == baseEntity.Id));
        Assert.True(await DbContextSUT.Authors.AnyAsync(i => i.Id == baseEntityAuthor.Id));
    }
    
    [Fact]
    public async Task DeleteById_File_Deleted()
    {
        //Arrange
        var baseEntityAuthor = FileTestSeeds.FileForDelete.Author;
        var baseEntity = FileTestSeeds.FileForDelete with{Author = null!};
        
        //Act
        DbContextSUT.Files.Remove(DbContextSUT.Files.Single(i => i.Id == baseEntity.Id));
        await DbContextSUT.SaveChangesAsync();
        
        //Assert
        Assert.False(await DbContextSUT.Files.AnyAsync(i => i.Id == baseEntity.Id));
        Assert.True(await DbContextSUT.Authors.AnyAsync(i => i.Id == baseEntityAuthor.Id));
    }
    
    [Fact]
    public async Task GetById_File()
    {
        //Act
        var entity = await DbContextSUT.Files
            .SingleAsync(i => i.Id == FileTestSeeds.FileBasic.Id);

        //Assert
        DeepAssert.Equal(FileTestSeeds.FileBasic with { Author = null!, FilesInAlbums = [], FilesInPlaylists = []}, entity);
    }
    
    [Fact]
    public async Task GetById_IncludingAuthorAndAlbum()
    {
        //Act
        var actualEntity = await DbContextSUT.Files
            .Include(e => e.Author)
            .Include(e => e.FilesInAlbums)
            .ThenInclude(e => e.Album)
            .SingleAsync(i => i.Id == FileTestSeeds.FileInAlbum.Id);
    
        var expected = FileTestSeeds.FileInAlbum;

        //Assert
        DeepAssert.Equal(expected with { Author = null!, FilesInAlbums = [], FilesInPlaylists = []}, FileTestSeeds.FileInAlbum with { Author = null!, FilesInAlbums = [], FilesInPlaylists = []});
        Assert.Equal(expected.Author.Id, actualEntity.Author.Id);
        Assert.Equal(actualEntity.FilesInAlbums.Single().Album.Id, AlbumTestSeeds.BasicAlbum.Id);
    }


    
}