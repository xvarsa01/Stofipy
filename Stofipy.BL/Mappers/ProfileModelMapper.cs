using Stofipy.BL.Mappers.InterfaceBase;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Mappers;

public class ProfileModelMapper(PlaylistModelMapper playlistModelMapper,
    ProfileFollowingProfileModelMapper profileFollowingProfileModelMapper, ProfileFollowingAuthorModelMapper profileFollowingAuthorModelMapper)
    : ModelMapperBase<ProfileEntity, ProfileListModel, ProfileDetailModel>
{
    public override ProfileListModel MapToListModel(ProfileEntity? entity)
    {
        if (entity is null)
        {
            return ProfileListModel.Empty;
        }

        return new ProfileListModel
        {
            Id = entity.Id,
            Name = entity.Name,
            ProfilePicture = entity.ProfilePicture
        };
    }
    

    public override ProfileDetailModel MapToDetailModel(ProfileEntity? entity)
    {
        if (entity is null)
        {
            return ProfileDetailModel.Empty;
        }

        return new ProfileDetailModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Following = profileFollowingProfileModelMapper.MapToListModel(entity.Followings),
            Followers = profileFollowingProfileModelMapper.MapToListModel(entity.Followers),
            FollowingAuthors = profileFollowingAuthorModelMapper.MapToListModel(entity.FollowingAuthors),
            CreatedPlaylists = playlistModelMapper.MapToListModel(entity.CreatedPlaylists),
        };
    }

    public override ProfileEntity MapToEntity(ProfileDetailModel model)
    {
        return new ProfileEntity
        {
            Id = model.Id,
            Name = model.Name,
            ProfilePicture = model.ProfilePicture
        };
    }
}