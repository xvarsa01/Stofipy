using Stofipy.BL.Models.InterfaceBase;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Models;

public record GlobalSearchModel()
{
    public List<FileListModel> Files { get; set; } = [];
    public List<AuthorListModel> Authors { get; set; } = [];
    public List<AlbumListModel> Albums { get; set; } = [];
    public List<PlaylistListModel> Playlists { get; set; } = [];

    public TopResultType TopResultType { get; set; } = TopResultType.None;
    public FileListModel? TopResultFile { get; set; }
    public AuthorListModel? TopResultAuthor { get; set; }
    public AlbumListModel? TopResultAlbum { get; set; }
    public PlaylistListModel? TopResultPlaylist { get; set; }
}

public enum TopResultType
{
    None,
    File,
    Author,
    Album,
    Playlist,
}