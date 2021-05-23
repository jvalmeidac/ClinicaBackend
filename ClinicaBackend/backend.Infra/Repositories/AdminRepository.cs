using backend.Domain.Interfaces.Repositories;
using backend.Infra.Repositories.Base;
using Dapper;
using System;

namespace backend.Infra.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DbSession _session;

        public AdminRepository(DbSession session)
        {
            _session = session;
        }

        public void DelegateAcademic(Guid academicId, Guid subjectId)
        {
            string sql = "INSERT INTO academicsXsubjects VALUES (@IdAcademic, @IdSubject)";
            _session.Connection.Execute(sql, new { academicId, subjectId }, _session.Transaction);
        }

        public bool ExistsRelationship(Guid idAcademic, Guid idSubject)
        {
            string sql = "SELECT COUNT(1) FROM academicsXsubjects " +
                $"WHERE IdAcamid='{idAcademic}' AND IdSubject='{idSubject}'";
            bool exists = _session.Connection.ExecuteScalar<bool>(sql);

            return exists;
        }
    }
}
