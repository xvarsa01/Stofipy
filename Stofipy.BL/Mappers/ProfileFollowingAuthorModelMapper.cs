using Stofipy.BL.Mappers.InterfaceBase;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Mappers;

public class ProfileFollowingAuthorModelMapper()
    : ModelMapperBase<ProfileFollowingAuthorEntity, ProfileFollowingAuthorModel, ProfileFollowingAuthorModel>
{
    public override ProfileFollowingAuthorModel MapToListModel(ProfileFollowingAuthorEntity? entity)
    {
        if (entity is null)
        {
            return ProfileFollowingAuthorModel.Empty;
        }

        return new ProfileFollowingAuthorModel
        {
            Id = entity.Id,
            FollowerId = entity.FollowerId,
            AuthorId = entity.AuthorId,
        };
    }

    public override ProfileFollowingAuthorModel MapToDetailModel(ProfileFollowingAuthorEntity? entity)
    {
        return MapToListModel(entity);
    }

    public override ProfileFollowingAuthorEntity MapToEntity(ProfileFollowingAuthorModel model)
    {
        return new ProfileFollowingAuthorEntity
        {
            Id = model.Id,
            FollowerId = model.FollowerId,
            AuthorId = model.AuthorId,
            Follower = null!,
            Author = null!,
        };
    }
}