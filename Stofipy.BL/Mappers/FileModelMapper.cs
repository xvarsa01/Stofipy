using Stofipy.BL.Mappers.InterfaceBase;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Mappers;

public class FileModelMapper : ModelMapperBase<FileEntity, FileListModel, FileDetailModel>
{
    public override FileListModel MapToListModel(FileEntity? entity)
    {
        if (entity == null)
        {
            return FileListModel.Empty;
        }

        return new FileListModel
        {
            Id = entity.Id,
            FileName = entity.FileName,
            PlayCount = entity.PlayCount,
            Length = entity.Length,
            AuthorId = entity.AuthorId,
            AuthorName = entity.Author.AuthorName,
            Picture = entity.Picture ?? entity.DefaultAlbum?.Picture,
        };
    }

    public override FileDetailModel MapToDetailModel(FileEntity? entity)
    {
        if (entity is null)
        {
            return FileDetailModel.Empty;
        }

        return new FileDetailModel
        {
            Id = entity.Id,
            FileName = entity.FileName,
            Description = entity.Description,
            Picture = entity.Picture,
            Lyrics = entity.Lyrics,
            Size = entity.Size,
            Length = entity.Length,
            PlayCount = entity.PlayCount,
            Category = entity.Category,
            AuthorId = entity.AuthorId,
            AuthorName = entity.Author.AuthorName,
            DefaultAlbumId = entity.DefaultAlbumId,
            DefaultAlbumName = entity.DefaultAlbum?.AlbumName,
        };
    }

    public override FileEntity MapToEntity(FileDetailModel model)
    {
        return new FileEntity
        {
            Id = model.Id,
            FileName = model.FileName,
            Description = model.Description,
            Picture = model.Picture,
            Lyrics = model.Lyrics,
            Size = model.Size,
            Length = model.Length,
            PlayCount = model.PlayCount,
            Category = model.Category,
            AuthorId = model.AuthorId,
            Author = null!,
            DefaultAlbumId = model.DefaultAlbumId,
            DefaultAlbum = null!,
        };
    }
}