namespace Stofipy.App.Messages;

public record RefreshQueueMessage(Guid FileId, Guid? AlbumId = null, Guid? AuthorId = null, Guid? PlaylistId = null);