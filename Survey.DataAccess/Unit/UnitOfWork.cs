
using Survey.Core.IUnit;

namespace Survey.DataAccess.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
