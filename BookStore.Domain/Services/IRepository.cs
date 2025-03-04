namespace BookStore.Domain.Services;

public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    IList<TEntity> GetAll();
    TEntity? Get(TKey key);
    bool Add(TEntity entity);
    bool Update(TEntity entity);
    bool Delete(TKey key);
}