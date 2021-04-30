using backend.Domain.Entities;
using backend.Domain.Interfaces.Repositories.Base;

namespace backend.Domain.Interfaces.Repositories
{
    public interface IPatientRepository : IRepositoryBase<Patient>
    {
        Patient Login(string email, string password);
        bool Exists(string email, string cpf, string rg);
        int GetPatientsCount();
    }
}
