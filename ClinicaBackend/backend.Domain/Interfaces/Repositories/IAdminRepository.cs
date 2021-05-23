using System;

namespace backend.Domain.Interfaces.Repositories
{
    public interface IAdminRepository
    {

        void DelegateAcademic(Guid academicId, Guid subjectId);
        bool ExistsRelationship(Guid idAcademic, Guid idSubject);
    }
}
