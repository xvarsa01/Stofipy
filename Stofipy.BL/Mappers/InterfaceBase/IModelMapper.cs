namespace Stofipy.BL.Mappers.InterfaceBase;

public interface IModelMapper<TEntity, TListModel, TDetailModel>
{
    TListModel MapToListModel(TEntity? entity);

    List<TListModel> MapToListModel(IEnumerable<TEntity> entities);
    List<TDetailModel> MapToDetailModel(IEnumerable<TEntity> entities);

    TDetailModel MapToDetailModel(TEntity? entity);
    TEntity MapToEntity(TDetailModel model);
}
