using System;

namespace backend.Infra.Repositories.Base
{
    public interface IUnityOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
