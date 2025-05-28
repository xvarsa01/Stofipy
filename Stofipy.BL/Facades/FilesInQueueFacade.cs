using System.Diagnostics;
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
    private readonly IAlbumFacade _albumFacade;
    private readonly IAuthorFacade _authorFacade;
    private readonly IFilesInPlaylistFacade _filesInPlaylistFacade;
    private readonly IFilesInAlbumFacade _filesInAlbumFacade;
    private readonly IFileFacade _fileFacade;
    public FilesInQueueFacade(FilesInQueueRepository repository, FilesInQueueModelMapper modelMapper,
        IPlaylistFacade playlistFacade, IFilesInPlaylistFacade filesInPlaylistFacade, IAlbumFacade albumFacade, IFilesInAlbumFacade filesInAlbumFacade, IAuthorFacade authorFacade, IFileFacade fileFacade)
        : base(repository, modelMapper)
    {
        _modelMapper = modelMapper;
        _repository = repository;
        _playlistFacade = playlistFacade;
        _filesInPlaylistFacade = filesInPlaylistFacade;
        _albumFacade = albumFacade;
        _filesInAlbumFacade = filesInAlbumFacade;
        _authorFacade = authorFacade;
        _fileFacade = fileFacade;
    }

    public async Task<FilesInQueueModel?> GetCurrentAsync()
    {
        var entity = await _repository.GetByIndexAsync(0, true);
        if (entity == null)
        {
            entity = await _repository.GetByIndexAsync(0, false);
        }
        return entity is null ?
            null : _modelMapper.MapToDetailModel(entity);
    }

    public async Task<List<FilesInQueueModel>> GetAllPriorityFilesInQueueAsync()
    {
        List<FilesInQueueEntity> entities = await _repository.GetAllPriorityAsync();
        
        var models =  _modelMapper.MapToListModel(entities);
        return models;
    }

    public async Task<List<FilesInQueueModel>> GetAllNonPriorityFilesInQueueAsync()
    {
        List<FilesInQueueEntity> entities = await _repository.GetAllNonPriorityAsync();
        
        var models =  _modelMapper.MapToListModel(entities);
        return models;
    }

    public async Task<List<FilesInQueueModel>> GetRecentFilesInQueueAsync(int numberOfFiles)
    {
        List<FilesInQueueEntity> entities = await _repository.GetRecentAsync(numberOfFiles);
        
        var models =  _modelMapper.MapToListModel(entities);
        return models;
    }

    public async Task NextSong()
    {
        if (await _repository.GetByIndexAsync(1, true) != null)
        {
            await DecrementIndexes(null, null, true);
        }
        else
        {
            await DecrementIndexes(null, null, true);
            await DecrementIndexes(null, null, false);
        }
    }

    public async Task PreviousSong()
    {
        if (await _repository.GetByIndexAsync(0, false) != null)
        {
            await IncrementIndexes(null, null, true);
            await IncrementIndexes(null, null, false);
            return;
        }
        if (await _repository.GetByIndexAsync(-1, true) != null)
        {
            await IncrementIndexes(null, null, true);
        }
    }

    private async Task PlayFileNow(Guid fileId, string fileName, string authorName)
    {
        if (await _repository.GetByIndexAsync(0, true) != null)
        {
            await DecrementIndexes(null, 0, true);
        }
        else
        {
            await DecrementIndexes(null, 0, false);
        }

        FilesInQueueEntity entity = new()
        {
            Id = Guid.NewGuid(),
            Index = 0,
            PriorityQueue = true,
            File = null!,
            FileId = fileId
        };
        await _repository.InsertAsync(entity);
    }

    public async Task<Guid> AddFileToQueue(Guid fileId, string fileName, string authorName)
    {
        var newFile = new FilesInQueueModel
        {
            Id = Guid.NewGuid(),
            FileId = fileId,
            FileName = fileName,
            AuthorName = authorName,
            Index = GetMaxPriorityIndex() + 1,
            PriorityQueue = true
        };
        
        FilesInQueueEntity entity = _modelMapper.MapToEntity(newFile);
        
        Guid createdEntityId = await _repository.InsertAsync(entity);
        return createdEntityId;
    }
    private async Task AddFileToNonPriorityQueue(Guid fileId, string fileName, string authorName)
    {
        var newFile = new FilesInQueueModel
        {
            Id = Guid.NewGuid(),
            FileId = fileId,
            FileName = fileName,
            AuthorName = authorName,
            Index = GetMaxNonPriorityIndex() + 1,
            PriorityQueue = false
        };
        
        FilesInQueueEntity entity = _modelMapper.MapToEntity(newFile);
        
        await _repository.InsertAsync(entity);
    }
    public override Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException("Use overload with index");
    }

    public async Task DeleteAsync(Guid id, int deletedIndex)
    {
        try
        {
            var entityForDelete = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(id);

            Debug.Assert(entityForDelete != null, nameof(entityForDelete) + " != null");
            
            await DecrementIndexes(deletedIndex + 1, null, entityForDelete.PriorityQueue);
        }
        catch (DbUpdateException e)
        {
            throw new InvalidOperationException("Entity deletion failed.", e);
        }
    }

    public async Task AddAlbumToQueue(Guid albumId, bool randomShuffle)
    {
        AlbumDetailModel? album = await _albumFacade.GetByIdAsync(albumId);
        if (album == null)
        {
            throw new InvalidDataException("Album not found");
        }
        
        var files = await _filesInAlbumFacade.GetAllByAlbumIdAsync(album.Id);
        if(files.Count == 0) return;

        await RemoveAllFromQueue(false);

        if (randomShuffle)
        {
            files = files.OrderBy(_ => Guid.NewGuid()).ToList();
        }
        var first = files.First();
        files.Remove(first);
        
        await PlayFileNow(first.FileId, first.FileName, album.AuthorName);
        
        foreach (var item in files)
        {
            await AddFileToNonPriorityQueue(item.FileId, item.FileName, album.AuthorName);
        }
    }
    
    public async Task AddAuthorToQueue(Guid authorId)
    {
        AuthorDetailModel? author = await _authorFacade.GetByIdAsync(authorId);
        if (author == null)
        {
            throw new InvalidDataException("Author not found");
        }

        var files = await _fileFacade.GetMostPopularFiles(author.Id, 1, 20);
        if(files.Count == 0) return;
        
        await RemoveAllFromQueue(false);
        
        files = files.OrderBy(_ => Guid.NewGuid()).ToList();
        var first = files.First();
        files.Remove(first);
        
        await PlayFileNow(first.Id, first.FileName, first.AuthorName);
        
        foreach (var item in files)
        {
            await AddFileToNonPriorityQueue(item.Id, item.FileName, author.AuthorName);
        }
    }
    
    public async Task<Task> AddPlaylistToQueue(Guid playlistId, bool randomShuffle)
    {
        PlaylistDetailModel? playlist = await _playlistFacade.GetByIdAsync(playlistId);
        if (playlist == null)
        {
            throw new InvalidDataException("Playlist not found");
        }
        

        const int pageSize = 10;

        // Step 1: Get the first page immediately
        var firstPage = await _filesInPlaylistFacade.GetAllAsync(playlistId, 1, pageSize);
        if(firstPage.Count == 0) return Task.CompletedTask;

        await RemoveAllFromQueue(false);
        
        if (randomShuffle)
        {
            firstPage = firstPage.OrderBy(_ => Guid.NewGuid()).ToList();
        }
        var first = firstPage.First();
        firstPage.Remove(first);
        
        await PlayFileNow(first.FileId, first.FileName, first.AuthorName);
        foreach (var item in firstPage)
        {
            await AddFileToNonPriorityQueue(item.FileId, item.FileName, item.AuthorName);
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
                await AddFileToNonPriorityQueue(item.FileId, item.FileName, item.AuthorName);
            }
        });
        return backgroundTask;
    }

    public async Task RemoveAllFromQueue(bool priority)
    {
        var queue = priority
            ? await _repository.GetAllPriorityAsync()
            : await _repository.GetAllNonPriorityAsync();

        foreach (var item in queue)
        {
            await _repository.DeleteAsync(item.Id);
        }
    }
    
    public async Task ReorderQueue(int oldIndex, int newIndex, bool oldPriority, bool newPriority)
    {
        if (oldIndex == newIndex && oldPriority == newPriority) return;

        var item = await _repository.GetByIndexAsync(oldIndex, oldPriority);
        if (item == null)
        {
            throw new IndexOutOfRangeException("oldIndex is out of range");
        }
        
        if (oldPriority == newPriority)
        {
            if (oldIndex < newIndex)
            {
                await DecrementIndexes(oldIndex + 1, newIndex, oldPriority);
            }
            else
            {
                await IncrementIndexes(newIndex, oldIndex -1, oldPriority);
            }
            item.Index = newIndex;
            await _repository.UpdateAsync(item);
        }
        else if (oldPriority && !newPriority)
        {
            await DecrementIndexes(oldIndex + 1, null, true);
            await IncrementIndexes(newIndex, null, false);
            item.PriorityQueue = false;
        }
        else
        {
            await IncrementIndexes(newIndex, null, true);
            await DecrementIndexes(oldIndex + 1, null, false);
            item.PriorityQueue = true;
        }
        
        item.Index = newIndex;
        await _repository.UpdateAsync(item);
    }
    
    private int GetMaxNonPriorityIndex()
    {
        return _repository.GetMaxNonPriorityIndex();
    }

    private int GetMaxPriorityIndex()
    {
        return _repository.GetMaxPriorityIndex();
    }

    private async Task DecrementIndexes(int? startIndex, int? endIndex, bool priority)
    {
        startIndex ??= int.MinValue;
        endIndex ??= int.MaxValue;
        
        var filesInQueueEntity = await _repository.GetAllAsync();
        var itemsToUpdate = filesInQueueEntity
            .Where(f => f.Index >= startIndex)
            .Where(f => f.Index <= endIndex);

        itemsToUpdate = priority
            ? itemsToUpdate.Where(e => e.PriorityQueue)
            : itemsToUpdate.Where(e => e.PriorityQueue == false);
            
        foreach (var item in itemsToUpdate.ToList())
        {
            item.Index--;
            await _repository.UpdateAsync(item);
        }
    }
    
    private async Task IncrementIndexes(int? startIndex, int? endIndex, bool priority)
    {
        startIndex ??= int.MinValue;
        endIndex ??= int.MaxValue;
        
        var filesInQueueEntity = await _repository.GetAllAsync();
        var itemsToUpdate = filesInQueueEntity
            .Where(f => f.Index >= startIndex)
            .Where(f => f.Index <= endIndex);

        itemsToUpdate = priority
            ? itemsToUpdate.Where(e => e.PriorityQueue)
            : itemsToUpdate.Where(e => e.PriorityQueue == false);
            
        foreach (var item in itemsToUpdate.ToList())
        {
            item.Index++;
            await _repository.UpdateAsync(item);
        }
    }
}