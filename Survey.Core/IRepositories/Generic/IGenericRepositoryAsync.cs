
namespace Survey.Core.IRepositories.Generic
{
    public interface IGenericRepositoryAsync<T> where T : BaseEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<IList<T>> GetAllAsync(ISpecification<T> specification);
        Task<T> GetByIdAsync(object id);
        Task<T> GetByIdAsync(ISpecification<T> specification);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(object id);
        Task DeleteAsync(T entity);

    }
}
