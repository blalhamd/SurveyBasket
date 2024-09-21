

using Survey.Core.IRepositories.Generic;
using Survey.Entities.entities;

namespace Survey.DataAccess.Repositories.Generic
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : BaseEntity
    {
    }
}
