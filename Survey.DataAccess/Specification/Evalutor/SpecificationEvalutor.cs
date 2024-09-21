
namespace Survey.DataAccess.Specification.Evalutor
{
    public static class SpecificationEvalutor<T> where T : BaseEntity
    {
        public static IQueryable<T> BuildQuery(DbSet<T> dbSet,ISpecification<T> spec)
        {
            var query = dbSet.AsQueryable();

            if(spec.Includes is not null)
            {
                foreach(var include in spec.Includes)
                {
                    query = query.Include(include);
                }
            }

            if(spec.Predicate is not null)
            {
                query = query.Where(spec.Predicate);
            }

            return query;
        }
    }
}
