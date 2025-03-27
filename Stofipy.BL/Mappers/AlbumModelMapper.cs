using Stofipy.BL.Mappers.InterfaceBase;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Mappers;

public class AlbumModelMapper(FilesInAlbumModelMapper filesInAlbumModelMapper) : ModelMapperBase<AlbumEntity, AlbumListModel, AlbumDetailModel>
{
    public override AlbumListModel MapToListModel(AlbumEntity? entity)
    {
        if (entity is null)
        {
            return AlbumListModel.Empty;
        }
        
        return new AlbumListModel
        {
            Id = entity.Id,
            AlbumName = entity.AlbumName,
            Picture = entity.Picture,
            Year = entity.Year,
            AuthorId = entity.AuthorId,
            AuthorName = entity.Author.AuthorName
        };
    }

    public override AlbumDetailModel MapToDetailModel(AlbumEntity? entity)
    {
        if (entity is null)
        {
            return AlbumDetailModel.Empty;
        }
        return new AlbumDetailModel
        {
            Id = entity.Id,
            AlbumName = entity.AlbumName,
            Description = entity.Description,
            Picture = entity.Picture,
            Year = entity.Year,
            Length = entity.FilesInAlbums?.Sum(file => file.File.Length) ?? 0,
            AuthorId = entity.AuthorId,
            AuthorName = entity.Author.AuthorName,
            FilesInAlbums = filesInAlbumModelMapper.MapToListModel(entity.FilesInAlbums),
        };
    }

    public override AlbumEntity MapToEntity(AlbumDetailModel model)
    {
        return new AlbumEntity
        {
            Id = model.Id,
            AlbumName = model.AlbumName,
            Description = model.Description,
            Picture = model.Picture,
            Year = model.Year,
            AuthorId = model.AuthorId,
            Author = null!,
        };
    }
}