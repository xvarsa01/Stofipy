using Stofipy.BL.Mappers.InterfaceBase;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Mappers;

public class FilesInAlumModelMapper : ModelMapperBase<FilesInAlbumEntity, FilesInAlbumModel, FilesInAlbumModel>
{
    public override FilesInAlbumModel MapToListModel(FilesInAlbumEntity? entity)
    {
        if (entity is null)
        {
            return FilesInAlbumModel.Empty;
        }

        return new FilesInAlbumModel
        {
            Id = entity.Id,
            FileId = entity.FileId,
            FileName = entity.File.FileName,
            Index = entity.Index,
        };
    }

    public override FilesInAlbumModel MapToDetailModel(FilesInAlbumEntity? entity)
    {
        return MapToListModel(entity);
    }

    public override FilesInAlbumEntity MapToEntity(FilesInAlbumModel model)
    {
        throw new System.NotImplementedException("This method is unsupported. Use the other overload.");
    }

    public FilesInAlbumEntity MapToEntity(FilesInAlbumModel model, Guid albumId)
    {
        return new FilesInAlbumEntity
        {
            Id = model.Id,
            AlbumId = albumId,
            FileId = model.FileId,
            Album = null!,
            File = null!,
            Index = model.Index,
        };
    }
}