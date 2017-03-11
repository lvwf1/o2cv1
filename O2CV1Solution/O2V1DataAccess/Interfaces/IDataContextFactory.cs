using System;

namespace O2V1DataAccess.Interfaces
{
    public interface IDataContextFactory<out TContext> : IDisposable where TContext : IDisposable
    {
        TContext GetContext();
    }
}
