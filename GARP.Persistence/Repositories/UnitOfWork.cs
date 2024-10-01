using GARP.Application.Interface.Persistence;

namespace GARP.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAutoRepository Auto { get; }

        public UnitOfWork(IAutoRepository auto)
        {
            Auto = auto;
        }
        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
