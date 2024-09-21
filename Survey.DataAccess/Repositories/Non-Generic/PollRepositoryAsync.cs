

using Survey.Core.IRepositories.NonGeneric;
using Survey.DataAccess.Repositories.Generic;
using Survey.Entities.entities;

namespace Survey.DataAccess.Repositories.Non_Generic
{
    public class PollRepositoryAsync : GenericRepositoryAsync<Poll>,IPollRepositoryAsync
    {
    }
}
