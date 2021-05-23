using backend.Domain.Entities;
using backend.Domain.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;

namespace backend.Domain.Interfaces.Repositories
{
    public interface IAcademicRepository : IRepositoryBase<Academic>
    {
        Academic Login(string email, string password);
        int GetAcademicsCount();
        List<Appointment> GetAppointmentsClosed(Guid academicId);
        List<Appointment> GetAppointmentsOpened();
    }
}
