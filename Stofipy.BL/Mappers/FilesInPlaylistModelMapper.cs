using Stofipy.BL.Mappers.InterfaceBase;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Mappers;

public class FilesInPlaylistModelMapper : ModelMapperBase<FilesInPlaylistEntity, FilesInPlaylistModel, FilesInPlaylistModel>
{
    public override FilesInPlaylistModel MapToListModel(FilesInPlaylistEntity? entity)
    {
        if (entity is null)
        {
            return FilesInPlaylistModel.Empty;
        }

        return new FilesInPlaylistModel
        {
            Id = entity.Id,
            FileId = entity.FileId,
            FileName = entity.File.FileName,
            AuthorName = entity.File.Author.AuthorName,
            DefaultAlbumName = entity.File.DefaultAlbum != null
                ? entity.File.DefaultAlbum.AlbumName
                : string.Empty,
            Picture = entity.File.Picture ?? entity.File.DefaultAlbum?.Picture,
            Length = entity.File.Length,
            IndexActual = entity.IndexActual,
            IndexCustom = entity.IndexCustom,
        };
    }

    public override FilesInPlaylistModel MapToDetailModel(FilesInPlaylistEntity? entity)
    {
        return MapToListModel(entity);
    }

    public override FilesInPlaylistEntity MapToEntity(FilesInPlaylistModel model)
    {
        throw new System.NotImplementedException("This method is unsupported. Use the other overload.");
    }

    public FilesInPlaylistEntity MapToEntity(FilesInPlaylistModel model, Guid playlistId)
    {
        return new FilesInPlaylistEntity
        {
            Id = model.Id,
            PlaylistId = playlistId,
            FileId = model.FileId,
            Playlist = null!,
            File = null!,
            IndexActual = model.IndexActual,
            IndexCustom = model.IndexCustom,
        };
    }
}