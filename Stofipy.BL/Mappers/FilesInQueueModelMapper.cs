using Stofipy.BL.Mappers.InterfaceBase;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;

namespace Stofipy.BL.Mappers;

public class FilesInQueueModelMapper : ModelMapperBase<FilesInQueueEntity, FilesInQueueModel, FilesInQueueModel>
{
    public override FilesInQueueModel MapToListModel(FilesInQueueEntity? entity)
    {
        if (entity is null)
        {
            return FilesInQueueModel.Empty;
        }

        return new FilesInQueueModel
        {
            Id = entity.Id,
            FileName = entity.File.FileName,
            AuthorName = entity.File.Author.AuthorName,
            Index = entity.Index,
            FileId = entity.FileId,
        };
    }

    public override FilesInQueueModel MapToDetailModel(FilesInQueueEntity? entity)
    {
        return MapToListModel(entity);
    }

    public override FilesInQueueEntity MapToEntity(FilesInQueueModel model)
    {
        return new FilesInQueueEntity
        {
            Id = model.Id,
            FileId = model.FileId,
            File = null!,
            Index = model.Index,
        };
    }

}