namespace Survey.DataAccess.Repositories.Generic
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : BaseEntity
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<T> _entity;

        public GenericRepositoryAsync(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _entity = _appDbContext.Set<T>();
        }
        public DbSet<T> Entity { get => _entity; set => _entity = value; }
        

        public async Task<IList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _entity.ToListAsync(cancellationToken);
        }

        public async Task<IList<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).ToListAsync(cancellationToken);

        }

        public async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await _entity.FindAsync(id,cancellationToken);
        }

        public async Task<T> GetByIdAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _entity.AddAsync(entity, cancellationToken);
        }


        public async Task UpdateAsync(T entity)
        {
            await Task.Run(() =>
            {
                _entity.Update(entity);
            }); 
        }
        public async Task DeleteAsync(object id, CancellationToken cancellationToken = default)
        {
            var entity = await _entity.FindAsync(id,cancellationToken);

            if (entity != null)
            {
                _entity.Remove(entity);
            }

        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() =>
            {
                _entity.Remove(entity);
            });
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvalutor<T>.BuildQuery(_entity, specification);
        }
    }
}
