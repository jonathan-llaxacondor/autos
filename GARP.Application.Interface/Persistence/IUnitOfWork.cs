using System;

namespace GARP.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        #region WS_General
        IAutoRepository Auto { get; }

        #endregion
    }
}
