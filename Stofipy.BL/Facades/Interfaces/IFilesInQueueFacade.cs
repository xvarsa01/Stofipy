using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Facades.Interfaces;

public interface IFilesInQueueFacade : IFacade<FilesInQueueEntity, FilesInQueueModel, FilesInQueueModel>
{
    Task<FilesInQueueModel?> GetCurrentAsync();
    Task<List<FilesInQueueModel>> GetAllPriorityFilesInQueueAsync();
    Task<List<FilesInQueueModel>> GetAllNonPriorityFilesInQueueAsync();
    Task<List<FilesInQueueModel>> GetRecentFilesInQueueAsync(int numberOfFiles);
    Task NextSong();
    Task PreviousSong();
    
    Task<Guid> AddFileToQueue(Guid fileId, string fileName, string authorName);
    Task<Task> AddPlaylistToQueue(Guid playlistId, bool randomShuffle);
    Task AddAuthorToQueue(Guid authorId);
    Task AddAlbumToQueue(Guid albumId, bool randomShuffle);
    Task DeleteAsync(Guid id, int deletedIndex);
    Task RemoveAllActiveFromQueue(bool priority);

    Task PlayItemAsync(FilesInQueueModel file);
    Task ReorderQueue(int oldIndex, int newIndex, bool oldPriority, bool newPriority);
}