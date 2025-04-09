using Microsoft.EntityFrameworkCore;
using Stofipy.BL.Facades;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;
using Stofipy.Common.Tests;
using Stofipy.Common.Tests.Seeds;
using Stofipy.DAL.Enums;
using Stofipy.DAL.Repositories;
using Xunit.Abstractions;

namespace Stofipy.BL.Tests;

public class FileFacadeTests : FacadeTestsBase
{
    private readonly IFileFacade _fileFacade;
    
    public FileFacadeTests(ITestOutputHelper output) : base(output)
    {
        var stofipyDbContext = DbContextFactory.CreateDbContext();
        var fileRepository = new FileRepository(stofipyDbContext);
        _fileFacade = new FileFacade(fileRepository, FileModelMapper);
    }

    [Fact]
    public async Task CreateFile_CreatesFile()
    {
        FileDetailModel file = new()
        {
            Id = Guid.NewGuid(),
            FileName = "TestFile",
            Description = "TestFile description",
            Picture = null,
            Lyrics = null,
            Size = 500,
            Length = 100,
            Category = Category.Rock,
            AuthorId = AuthorTestSeeds.AuthorAbc.Id,
            AuthorName = AuthorTestSeeds.AuthorAbc.AuthorName,
            DefaultAlbumId = null,
            DefaultAlbumName = null
        };
        
        await _fileFacade.CreateAsync(file);
        
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var fileFromDb = await dbxAssert.Files
            .Include(e => e.Author)
            .SingleAsync(e => e.Id == file.Id);

        var modelFromDb = FileModelMapper.MapToDetailModel(fileFromDb);
        DeepAssert.Equal(file, modelFromDb);
    }

    [Fact]
    public async Task GetAllFiles_ReturnsException()
    {
        await Assert.ThrowsAsync<NotImplementedException>(async () =>
            await _fileFacade.GetAllAsync());
    }
    
    [Fact]
    public async Task GetAllFilesWithPaging_ReturnsAllFilesPaged()
    {
        var pageNumber = 1;
        const int pageSize = 5;
        var files = await _fileFacade.GetAllAsync(pageNumber, pageSize);
        pageNumber++;
        var files2 = await _fileFacade.GetAllAsync(pageNumber, pageSize);
        
        Assert.Equal(pageSize, files.Count);
        Assert.Equal(pageSize, files2.Count);
        
        var idsPage1 = files.Select(f => f.Id).ToHashSet();
        var idsPage2 = files2.Select(f => f.Id).ToHashSet();
        Assert.False(idsPage1.Overlaps(idsPage2));
    }
    
    [Fact]
    public async Task GetFileById_ReturnsFile()
    {
        var file = await _fileFacade.GetByIdAsync(FileTestSeeds.FileBasic.Id);
        DeepAssert.Equal(FileModelMapper.MapToDetailModel(FileTestSeeds.FileBasic), file);
    }

    [Fact]
    public async Task GetNonExistingFile_ReturnsNull()
    {
        var file = await _fileFacade.GetByIdAsync(Guid.NewGuid());
        Assert.Null(file);
    }
    
    [Fact]
    public async Task UpdateFile_UpdatesFile()
    {
        var file = new FileDetailModel
        {
            Id = FileTestSeeds.FileForUpdate.Id,
            AuthorName = "this should not change",
            FileName = "changed name",
            Description = "changed description",
            Size = 1234,
            Length = 5678,
            Category = Category.Chill,
            AuthorId = FileTestSeeds.FileForUpdate.AuthorId,
        };
        
        //Act
        await _fileFacade.UpdateAsync(file);
        
        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var fileFromDb = await dbxAssert.Files
            .Include(e => e.Author)
            .SingleAsync(e => e.Id == file.Id);
        
        var modelFromDb = FileModelMapper.MapToDetailModel(fileFromDb);
        Assert.NotEqual(file.AuthorName, modelFromDb.AuthorName);
        file.AuthorName = FileTestSeeds.FileForUpdate.Author.AuthorName;
        DeepAssert.Equal(file, modelFromDb);
    }
    
    [Fact]
    public async Task DeleteFile_DeletesFile()
    {
        var file = FileTestSeeds.FileForDelete;
        await _fileFacade.DeleteAsync(file.Id);
        
        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Files.AnyAsync(i => i.Id == file.Id));
    }
}