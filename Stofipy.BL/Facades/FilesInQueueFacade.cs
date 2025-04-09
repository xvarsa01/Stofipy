using Microsoft.EntityFrameworkCore;
using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Repositories;

namespace Stofipy.BL.Facades;

public class FilesInQueueFacade : FacadeBase<FilesInQueueRepository, FilesInQueueEntity, FilesInQueueModel, FilesInQueueModel>, IFilesInQueueFacade
{
    private readonly FilesInQueueRepository _repository;
    private readonly FilesInQueueModelMapper _modelMapper;
    private readonly IPlaylistFacade _playlistFacade;
    private readonly IFilesInPlaylistFacade _filesInPlaylistFacade;
    public FilesInQueueFacade(FilesInQueueRepository repository, FilesInQueueModelMapper modelMapper,
        IPlaylistFacade playlistFacade, IFilesInPlaylistFacade filesInPlaylistFacade) : base(repository, modelMapper)
    {
        _modelMapper = modelMapper;
        _repository = repository;
        _playlistFacade = playlistFacade;
        _filesInPlaylistFacade = filesInPlaylistFacade;
    }

    public async Task<Guid> AddToQueue(Guid fileId, string fileName, string authorName)
    {
        var newFile = new FilesInQueueModel()
        {
            Id = Guid.NewGuid(),
            FileId = fileId,
            FileName = fileName,
            AuthorName = authorName,
            Index = GetMaxIndex() + 1
        };
        
        FilesInQueueEntity entity = _modelMapper.MapToEntity(newFile);
        
        Guid createdEntityId = await _repository.InsertAsync(entity);
        return createdEntityId;
    }

    public override Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException("Use overload with index");
    }

    public async Task DeleteAsync(Guid id, int deletedIndex)
    {
        try
        {
            await _repository.DeleteAsync(id);
            
            var filesInQueueEntity = await _repository.GetAllAsync();
            var itemsToUpdate = filesInQueueEntity
                .Where(f => f.Index > deletedIndex)
                .ToList();
            
            foreach (var item in itemsToUpdate)
            {
                item.Index--;
                await _repository.UpdateAsync(item);
            }
        }
        catch (DbUpdateException e)
        {
            throw new InvalidOperationException("Entity deletion failed.", e);
        }
    }

    public async Task<Task> AddPlaylistToQueue(Guid playlistId, bool randomShuffle)
    {
        PlaylistDetailModel? playlist = await _playlistFacade.GetByIdAsync(playlistId);
        if (playlist == null)
        {
            throw new InvalidDataException("Playlist not found");
        }
        
        await RemoveAllFromQueue();

        const int pageSize = 10;

        // Step 1: Get the first page immediately
        var firstPage = await _filesInPlaylistFacade.GetAllAsync(playlistId, 1, pageSize);
        
        if (randomShuffle)
        {
            firstPage = firstPage.OrderBy(_ => Guid.NewGuid()).ToList();
        }
        foreach (var item in firstPage)
        {
            await AddToQueue(item.FileId, item.FileName, item.AuthorName);
        }
        
        var backgroundTask = Task.Run(async () =>
        {
            var remainingItems = new List<FilesInPlaylistModel>();
            int page = 2;
            while (true)
            {
                var pageItems = await _filesInPlaylistFacade.GetAllAsync(playlistId, page, pageSize);
                if (pageItems.Count == 0) break;

                remainingItems.AddRange(pageItems);

                if (pageItems.Count < pageSize) break;
                page++;
            }

            if (randomShuffle)
            {
                remainingItems = remainingItems.OrderBy(_ => Guid.NewGuid()).ToList();
            }
            foreach (var item in remainingItems)
            {
                await AddToQueue(item.FileId, item.FileName, item.AuthorName);
            }
        });
        return backgroundTask;
    }

    private async Task RemoveAllFromQueue()
    {
        var queue = await _repository.GetAllAsync();

        foreach (var item in queue)
        {
            await _repository.DeleteAsync(item.Id);
        }
    }
    private int GetMaxIndex()
    {
        return _repository.GetMaxIndex();
    }
}