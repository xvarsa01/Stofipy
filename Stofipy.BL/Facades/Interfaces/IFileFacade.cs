using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Facades.Interfaces;

public interface IFileFacade : IFacade<FileEntity, FileListModel, FileDetailModel>
{
    Task<List<FileListModel>> GetMostPopularFiles(Guid authorId, int pageNumber, int pageSize);
}