using System;

namespace backend.Domain.Interfaces.Repositories.Base
{
    public interface IUnityOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
