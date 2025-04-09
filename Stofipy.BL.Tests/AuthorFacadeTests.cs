using Microsoft.EntityFrameworkCore;
using Stofipy.BL.Facades;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers;
using Stofipy.BL.Models;
using Stofipy.Common.Tests;
using Stofipy.Common.Tests.Seeds;
using Stofipy.DAL.Repositories;
using Xunit.Abstractions;

namespace Stofipy.BL.Tests;

public class AuthorFacadeTests : FacadeTestsBase
{
    private readonly IAuthorFacade _authorFacade;

    public AuthorFacadeTests(ITestOutputHelper output) : base(output)
    {
        var stofipyDbContext = DbContextFactory.CreateDbContext();
        var authorRepository = new AuthorRepository(stofipyDbContext);
        _authorFacade = new AuthorFacade(authorRepository, AuthorModelMapper);
    }

    [Fact]
    public async Task CreateNewAuthor_CreatesNewAuthor()
    {
        AuthorDetailModel author = new()
        {
            Id = Guid.NewGuid(),
            AuthorName = "new author",
            ProfilePicture = null,
            Files = [],
            Albums = []
        };
        
        await _authorFacade.CreateAsync(author);
        
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var authorFromDb = await dbxAssert.Authors.SingleAsync(i => i.Id == author.Id);
        DeepAssert.Equal(author, AuthorModelMapper.MapToDetailModel(authorFromDb));
    }

    [Fact]
    public async Task GetAllAuthors_ReturnsAllAuthors()
    {
        var authors = await _authorFacade.GetAllAsync();
        Assert.True(authors.Count >= 9);
    }

    [Fact]
    public async Task GetAuthorById_ReturnsAuthor()
    {
        var author = await _authorFacade.GetByIdAsync(AuthorTestSeeds.AuthorAbc.Id);
        DeepAssert.Equal(AuthorModelMapper.MapToDetailModel(AuthorTestSeeds.AuthorAbc), author);
    }

    [Fact]
    public async Task GetNonExistingAuthor_ReturnsNull()
    {
        var author = await _authorFacade.GetByIdAsync(Guid.NewGuid());
        Assert.Null(author);
    }

    [Fact]
    public async Task UpdateAuthor_UpdatesAuthor()
    {
        //Arrange
        // var author = await _authorFacade.GetByIdAsync(AuthorTestSeeds.AuthorAbc.Id);
        // Assert.NotNull(author);
        var author = new AuthorDetailModel()
        {
            Id = AuthorTestSeeds.AuthorAbc.Id,
            AuthorName = "changed name",
        };
        
        //Act
        await _authorFacade.UpdateAsync(author);
        
        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var authorFromDb = await dbxAssert.Authors.SingleAsync(i => i.Id == author.Id);
        DeepAssert.Equal(author, AuthorModelMapper.MapToDetailModel(authorFromDb));
    }

    [Fact]
    public async Task DeleteAuthor_DeletesAuthor()
    {
        var author = AuthorTestSeeds.AuthorWithoutContent;
        await _authorFacade.DeleteAsync(author.Id);
        
        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Authors.AnyAsync(i => i.Id == author.Id));
    }
    
    [Fact]
    public async Task DeleteAuthorWithFiles_Throws()
    {
        var author = AuthorTestSeeds.AuthorAbc;
        
        //Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _authorFacade.DeleteAsync(author.Id));
    }

    [Fact]
    public async Task DeleteAuthorWithAlbums_Throws()
    {
        var author = AuthorTestSeeds.AuthorWithEmptyAlbum;
        
        //Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _authorFacade.DeleteAsync(author.Id));
    }
}