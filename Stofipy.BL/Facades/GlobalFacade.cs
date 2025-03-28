using Stofipy.BL.Mappers;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Repositories;

namespace Stofipy.BL.Facades;

public class GlobalFacade
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

    public async Task<GlobalSearchModel> SearchGloballyAsync(Guid playlistId,string searchTerm)
    {
        List<FileEntity> fileEntities = await _fileRepository.SearchInFilesAsync(searchTerm);
        List<FileListModel> files = _fileModelMapper.MapToListModel(fileEntities);
        
        List<AuthorEntity> authorEntities = await _authorRepository.SearchInAuthorsAsync(searchTerm);
        List<AuthorListModel> authors = _authorModelMapper.MapToListModel(authorEntities);
        
        List<AlbumEntity> albumEntities = await _albumRepository.SearchInalbumsAsync(searchTerm);
        List<AlbumListModel> albums = _albumModelMapper.MapToListModel(albumEntities);
        
        List<PlaylistEntity> playlistEntities = await _playlistRepository.SearchInPlaylistsAsync(searchTerm);
        List<PlaylistListModel> playlists = _playlistModelMapper.MapToListModel(playlistEntities);

        return new GlobalSearchModel()
        {
            Files = files,
            Authors = authors,
            Albums = albums,
            Playlists = playlists,
        };
    }
}