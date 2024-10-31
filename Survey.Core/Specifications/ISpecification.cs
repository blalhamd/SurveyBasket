namespace Survey.Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Predicate { get; set; }
        public List<string> Includes { get; set; }
    }
}
