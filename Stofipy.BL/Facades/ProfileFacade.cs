using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Repositories;

namespace Stofipy.BL.Facades;

public class ProfileFacade : FacadeBase<ProfileRepository, ProfileEntity, ProfileListModel, ProfileDetailModel>, IProfileFacade
{
    private readonly ProfileRepository _repository;
    private readonly ProfileModelMapper _modelMapper;
    private readonly AuthorRepository _authorRepository;
    private readonly ProfileFollowingProfileRepository _profileFollowingProfileRepository;
    private readonly ProfileFollowingAuthorRepository _profileFollowingAuthorRepository;
    private readonly ProfileFollowingProfileModelMapper _profileFollowingProfileModelMapper;
    private readonly ProfileFollowingAuthorModelMapper _profileFollowingAuthorModelMapper;

    public ProfileFacade(ProfileRepository repository, ProfileModelMapper modelMapper, AuthorRepository authorRepository,
        ProfileFollowingProfileRepository profileFollowingProfileRepository,
        ProfileFollowingAuthorRepository profileFollowingAuthorRepository,
        ProfileFollowingProfileModelMapper profileFollowingProfileModelMapper,
        ProfileFollowingAuthorModelMapper profileFollowingAuthorModelMapper)
        : base(repository, modelMapper)
    {
        _modelMapper = modelMapper;
        _authorRepository = authorRepository;
        _profileFollowingProfileRepository = profileFollowingProfileRepository;
        _profileFollowingAuthorRepository = profileFollowingAuthorRepository;
        _profileFollowingProfileModelMapper = profileFollowingProfileModelMapper;
        _profileFollowingAuthorModelMapper = profileFollowingAuthorModelMapper;
        _repository = repository;
    }

    public async Task FollowProfile(Guid selectedProfileId, Guid userId)
    {
        var profileToUpdate = await _repository.GetByIdAsync(selectedProfileId);
        if (profileToUpdate == null)
        {
            throw new KeyNotFoundException($"Profile with id {selectedProfileId} not found");
        }
        var profileToFollow = await _repository.GetByIdAsync(userId);
        if (profileToFollow == null)
        {
            throw new KeyNotFoundException($"Profile with id {userId} not found");
        }

        var newModel = ProfileFollowingProfileModel.Empty with
        {
            FollowerId = selectedProfileId,
            FollowedId = userId
        };
        
        var newEntity = _profileFollowingProfileModelMapper.MapToEntity(newModel);
        newEntity.Id = await _profileFollowingProfileRepository.InsertAsync(newEntity);
            
        profileToUpdate.FollowNewProfile(newEntity);
        profileToFollow.AddFollower(newEntity);
        
        await _repository.UpdateAsync(profileToUpdate);
        await _repository.UpdateAsync(profileToFollow);
    }

    public async Task UnFollowProfile(Guid selectedProfileId, Guid userId)
    {        
        var profileToUpdate = await _repository.GetByIdAsync(selectedProfileId);
        if (profileToUpdate == null)
        {
            throw new KeyNotFoundException($"Profile with id {selectedProfileId} not found");
        }
        var profileToUnFollow = await _repository.GetByIdAsync(userId);
        if (profileToUnFollow == null)
        {
            throw new KeyNotFoundException($"Profile with id {userId} not found");
        }

        var entityToDelete = await _profileFollowingProfileRepository.GetByIdAsync(selectedProfileId, userId);
        if (entityToDelete == null)
        {
            throw new KeyNotFoundException(($"Profile with id {selectedProfileId} does not follow {userId}"));
        }
        await _profileFollowingProfileRepository.DeleteAsync(entityToDelete.Id);
            
        profileToUpdate.UnfollowProfile(entityToDelete);
        profileToUnFollow.RemoveFollower(entityToDelete);
        
        await _repository.UpdateAsync(profileToUpdate);
        await _repository.UpdateAsync(profileToUnFollow);
    }

    public async Task FollowArtist(Guid selectedProfileId, Guid artistId)
    {
        var profileToUpdate = await _repository.GetByIdAsync(selectedProfileId);
        if (profileToUpdate == null)
        {
            throw new KeyNotFoundException($"Profile with id {selectedProfileId} not found");
        }
        var artistToFollow = await _authorRepository.GetByIdAsync(artistId);
        if (artistToFollow == null)
        {
            throw new KeyNotFoundException($"Artist with id {artistId} not found");
        }
        
        var newModel = ProfileFollowingAuthorModel.Empty with
        {
            FollowerId = selectedProfileId,
            AuthorId = artistId
        };
        
        var newEntity = _profileFollowingAuthorModelMapper.MapToEntity(newModel);
        newEntity.Id = await _profileFollowingAuthorRepository.InsertAsync(newEntity);
            
        profileToUpdate.FollowArtist(newEntity);
        await _repository.UpdateAsync(profileToUpdate);
    }

    public async Task UnFollowArtist(Guid selectedProfileId, Guid artistId)
    {
        var profileToUpdate = await _repository.GetByIdAsync(selectedProfileId);
        if (profileToUpdate == null)
        {
            throw new KeyNotFoundException($"Profile with id {selectedProfileId} not found");
        }
        var artistToUnfollow = await _authorRepository.GetByIdAsync(artistId);
        if (artistToUnfollow == null)
        {
            throw new KeyNotFoundException($"Artist with id {artistId} not found");
        }
        
        var entityToDelete = await _profileFollowingAuthorRepository.GetByIdAsync(selectedProfileId, artistId);
        if (entityToDelete == null)
        {
            throw new KeyNotFoundException(($"Profile with id {selectedProfileId} does not follow {artistId}"));
        }
        await _profileFollowingProfileRepository.DeleteAsync(entityToDelete.Id);
            
        profileToUpdate.UnfollowArtist(entityToDelete);
        await _repository.UpdateAsync(profileToUpdate);
    }
}