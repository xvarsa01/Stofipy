using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Facades.Interfaces;

public interface IProfileFacade : IFacade<ProfileEntity, ProfileListModel, ProfileDetailModel>
{
    Task FollowProfile(Guid selectedProfileId, Guid userId);
    Task UnFollowProfile(Guid selectedProfileId, Guid userId);
    Task FollowArtist(Guid selectedProfileId, Guid artistId);
    Task UnFollowArtist(Guid selectedProfileId, Guid artistId);
}