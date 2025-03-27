using Microsoft.EntityFrameworkCore;
using Stofipy.Common.Tests;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Enums;
using Stofipy.DAL.Tests.Seeds;
using Xunit.Abstractions;

namespace Stofipy.DAL.Tests;

public class AuthorTests (ITestOutputHelper output): DbContextTestsBase(output)
{
    [Fact]
    public async Task AddNew_Author_Persisted()
    {
        //Arrange
        AuthorEntity entity = new()
        {
            Id = Guid.Parse("36A4A959-0FF7-4500-8749-B7A74F392712"),
            AuthorName = "Test Author",
            ProfilePicture = null,
            Files = [],
            Albums = []
        };

        //Act
        DbContextSUT.Authors.Add(entity);
        await DbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Authors.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntities);
    }
    
    [Fact]
    public async Task GetAllFilesByAuthor()
    {
        var entity = await DbContextSUT.Authors
            .Include(e => e.Files)
            .SingleAsync(a => a.Id == AuthorTestSeeds.AuthorAbc.Id);
        
        Assert.True(entity.Files.Count == 2);
        Assert.Contains(entity.Files, f => f.Id == FileTestSeeds.File1OfAuthorAbc.Id);
        Assert.Contains(entity.Files, f => f.Id == FileTestSeeds.File2OfAuthorAbc.Id);
    }
}