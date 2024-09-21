

namespace Survey.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Predicate { get; set; }
        public List<string> Includes { get; set; } = new List<string>();

        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T,bool>> predicate)
        {
            Predicate = predicate;
        }
    }
}
