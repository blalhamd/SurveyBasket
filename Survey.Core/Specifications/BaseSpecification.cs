
using Survey.Entities.entities;

namespace Survey.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
    }
}
