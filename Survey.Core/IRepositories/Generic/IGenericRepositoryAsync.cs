namespace Survey.Core.IRepositories.Generic
{
    public interface IGenericRepositoryAsync<T> where T : BaseEntity
    {
        Task<IList<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IList<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity);
        Task DeleteAsync(object id, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity);

    }
}
