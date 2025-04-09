using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Facades.Interfaces;

public interface IFilesInQueueFacade : IFacade<FilesInQueueEntity, FilesInQueueModel, FilesInQueueModel>
{
    Task<Guid> AddToQueue(Guid fileId, string fileName, string authorName);
    Task DeleteAsync(Guid id, int deletedIndex);
    Task<Task> AddPlaylistToQueue(Guid playlistId, bool randomShuffle);
}