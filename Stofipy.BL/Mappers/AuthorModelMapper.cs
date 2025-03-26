using Stofipy.BL.Mappers.InterfaceBase;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Mappers;

public class AuthorModelMapper(AlbumModelMapper albumModelMapper, FileModelMapper fileModelMapper) : ModelMapperBase<AuthorEntity, AuthorListModel, AuthorDetailModel>
{
    public override AuthorListModel MapToListModel(AuthorEntity? entity)
    {
        if (entity is null)
        {
            return AuthorListModel.Empty;
        }

        return new AuthorListModel
        {
            Id = entity.Id,
            AuthorName = entity.AuthorName,
            ProfilePicture = entity.ProfilePicture,
        };
    }

    public override AuthorDetailModel MapToDetailModel(AuthorEntity? entity)
    {
        if (entity is null)
        {
            return AuthorDetailModel.Empty;
        }

        return new AuthorDetailModel
        {
            Id = entity.Id,
            AuthorName = entity.AuthorName,
            ProfilePicture = entity.ProfilePicture,
            Files = fileModelMapper.MapToListModel(entity.Files),
            Albums = albumModelMapper.MapToListModel(entity.Albums),
        };
    }

    public override AuthorEntity MapToEntity(AuthorDetailModel model)
    {
        return new AuthorEntity()
        {
            Id = model.Id,
            AuthorName = model.AuthorName,
            ProfilePicture = model.ProfilePicture,
        };
    }
}