using Stofipy.BL.Mappers.InterfaceBase;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Mappers;

public class PlaylistModelMapper(FilesInPlaylistModelMapper filesInPlaylistModelMapper) : ModelMapperBase<PlaylistEntity, PlaylistListModel, PlaylistDetailModel>
{
    public override PlaylistListModel MapToListModel(PlaylistEntity? entity)
    {
        if (entity is null)
        {
            return PlaylistListModel.Empty;
        }

        return new PlaylistListModel
        {
            Id = entity.Id,
            PlaylistName = entity.PlaylistName,
            Picture = entity.Picture,
            Authors = entity.FilesInPlaylists?
                .Select(f => f.File.Author.AuthorName)
                .Distinct()
                .ToList() ?? []
        };
    }
    

    public override PlaylistDetailModel MapToDetailModel(PlaylistEntity? entity)
    {
        if (entity is null)
        {
            return PlaylistDetailModel.Empty;
        }

        return new PlaylistDetailModel
        {
            Id = entity.Id,
            PlaylistName = entity.PlaylistName,
            Description = entity.Description,
            Picture = entity.Picture,
            Length = entity.FilesInPlaylists?.Sum(file => file.File.Length) ?? 0,
            FilesInPlaylists = filesInPlaylistModelMapper.MapToListModel(entity.FilesInPlaylists),
        };
    }

    public override PlaylistEntity MapToEntity(PlaylistDetailModel model)
    {
        return new PlaylistEntity
        {
            Id = model.Id,
            PlaylistName = model.PlaylistName,
            Description = model.Description,
            Picture = model.Picture,
        };
    }
}