using backend.Domain.Entities;
using backend.Domain.Interfaces.Repositories.Base;

namespace backend.Domain.Interfaces.Repositories
{
    public interface ISubjectRepository : IRepositoryBase<Subject>
    {
        int GetSubjectsCount();
    }
}
