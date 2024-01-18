namespace rovic_rating_app.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();


        Task<bool> Add(TEntity enity);
        Task<bool> Update(TEntity enity);
        Task<bool> Remove(TEntity enity);
    }
}
