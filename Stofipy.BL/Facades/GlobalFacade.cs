using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers;
using Stofipy.BL.Models;
using Stofipy.DAL.Repositories;

namespace Stofipy.BL.Facades;

public class GlobalFacade : IGlobalFacade
{
    private readonly FileRepository _fileRepository;
    private readonly AuthorRepository _authorRepository;
    private readonly AlbumRepository _albumRepository;
    private readonly PlaylistRepository _playlistRepository;

    private readonly FileModelMapper _fileModelMapper;
    private readonly AuthorModelMapper _authorModelMapper;
    private readonly AlbumModelMapper _albumModelMapper;
    private readonly PlaylistModelMapper _playlistModelMapper;

    public GlobalFacade(FileRepository fileRepository, AuthorRepository authorRepository, AlbumRepository albumRepository, PlaylistRepository playlistRepository, FileModelMapper fileModelMapper, AuthorModelMapper authorModelMapper, AlbumModelMapper albumModelMapper, PlaylistModelMapper playlistModelMapper)
    {
        _fileRepository = fileRepository;
        _authorRepository = authorRepository;
        _albumRepository = albumRepository;
        _playlistRepository = playlistRepository;
        _fileModelMapper = fileModelMapper;
        _authorModelMapper = authorModelMapper;
        _albumModelMapper = albumModelMapper;
        _playlistModelMapper = playlistModelMapper;
    }

    public async Task<GlobalSearchModel> SearchGloballyAsync(string searchTerm)
    {
        FileListModel? topResultFile = null;
        AuthorListModel? topResultAuthor = null;
        AlbumListModel? topResultAlbum = null;
        PlaylistListModel? topResultPlaylist = null;

        var fileTask = _fileRepository.SearchInFilesAsync(searchTerm);
        var authorTask = _authorRepository.SearchInAuthorsAsync(searchTerm);
        var albumTask = _albumRepository.SearchInAlbumsAsync(searchTerm);
        var playlistTask = _playlistRepository.SearchInPlaylistsAsync(searchTerm);
        await Task.WhenAll(fileTask, authorTask, albumTask, playlistTask);
        
        var resultFiles = fileTask.Result;
        var resultAuthors = authorTask.Result;
        var resultAlbums = albumTask.Result;
        var resultPlaylists = playlistTask.Result;
        
        var files = _fileModelMapper.MapToListModel(resultFiles.SimilarFiles);
        var authors = _authorModelMapper.MapToListModel(resultAuthors.SimilarAuthors);
        var albums = _albumModelMapper.MapToListModel(resultAlbums.SimilarAlbums);
        var playlists = _playlistModelMapper.MapToListModel(resultPlaylists.SimilarPlaylists);

        var candidates = new List<(TopResultType Type, int Difference)>
        {
            (TopResultType.File, resultFiles.BestDifference),
            (TopResultType.Author, resultAuthors.BestDifference),
            (TopResultType.Album, resultAlbums.BestDifference),
            (TopResultType.Playlist, resultPlaylists.BestDifference)
        };

        var best = candidates.OrderBy(c => c.Difference).FirstOrDefault();
        var topResultType = best.Difference != int.MaxValue ? best.Type : TopResultType.None;

        switch (topResultType)
        {
            case TopResultType.File:
                topResultFile = _fileModelMapper.MapToListModel(resultFiles.BestFile);
                break;
            case TopResultType.Album:
                topResultAlbum = _albumModelMapper.MapToListModel(resultAlbums.BestAlbum);
                break;
            case TopResultType.Author:
                topResultAuthor = _authorModelMapper.MapToListModel(resultAuthors.BestAuthor);
                break;
            case TopResultType.Playlist:
                topResultPlaylist = _playlistModelMapper.MapToListModel(resultPlaylists.BestPlaylist);
                break;
        }
        
        return new GlobalSearchModel()
        {
            Files = files,
            Authors = authors,
            Albums = albums,
            Playlists = playlists,
            TopResultType = topResultType,
            TopResultFile = topResultFile,
            TopResultAuthor = topResultAuthor,
            TopResultAlbum = topResultAlbum,
            TopResultPlaylist = topResultPlaylist,
        };
    }
}