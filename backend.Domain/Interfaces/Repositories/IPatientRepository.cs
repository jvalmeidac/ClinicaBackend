using backend.Domain.Entities;
using backend.Domain.Interfaces.Repositories.Base;
using backend.Domain.Pagination;
using System.Collections.Generic;

namespace backend.Domain.Interfaces.Repositories
{
    public interface IPatientRepository : IRepositoryBase<Patient>
    {
        Patient Login(string email, string password);
        IEnumerable<Patient> GetPatients(PageParameters pageParameters);
    }
}
