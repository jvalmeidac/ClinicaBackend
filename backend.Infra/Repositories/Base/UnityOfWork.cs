namespace backend.Infra.Repositories.Base
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly DbSession _session;

        public UnityOfWork(DbSession session)
        {
            _session = session;
        }

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }
        public void Dispose() => _session.Transaction?.Dispose();
    }
}
