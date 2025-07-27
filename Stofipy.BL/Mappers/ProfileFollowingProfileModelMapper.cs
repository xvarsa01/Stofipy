using Stofipy.BL.Mappers.InterfaceBase;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Mappers;

public class ProfileFollowingProfileModelMapper()
    : ModelMapperBase<ProfileFollowingProfileEntity, ProfileFollowingProfileModel, ProfileFollowingProfileModel>
{
    public override ProfileFollowingProfileModel MapToListModel(ProfileFollowingProfileEntity? entity)
    {
        if (entity is null)
        {
            return ProfileFollowingProfileModel.Empty;
        }

        return new ProfileFollowingProfileModel
        {
            Id = entity.Id,
            FollowerId = entity.FollowerId,
            FollowedId = entity.FollowedId,
        };
    }

    public override ProfileFollowingProfileModel MapToDetailModel(ProfileFollowingProfileEntity? entity)
    {
        return MapToListModel(entity);
    }

    public override ProfileFollowingProfileEntity MapToEntity(ProfileFollowingProfileModel model)
    {
        return new ProfileFollowingProfileEntity
        {
            Id = model.Id,
            FollowerId = model.FollowerId,
            FollowedId = model.FollowedId,
            Follower = null!,
            Followed = null!,
        };
    }
}