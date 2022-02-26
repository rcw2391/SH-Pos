namespace DataLayer.Repositories
{
    public interface IRepository<T> where T : ICrudObject
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> CreateAsync(IEnumerable<T> entities);
        Task<bool> UpdateAsync(T entity);
        Task<bool> UpdateAsync(IEnumerable<T> entities);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAsync(IEnumerable<T> entities);
        Task<bool> DeleteByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
