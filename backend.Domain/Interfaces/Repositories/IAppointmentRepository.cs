using backend.Domain.Entities;
using backend.Domain.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;

namespace backend.Domain.Interfaces.Repositories
{
    public interface IAppointmentRepository : IRepositoryBase<Appointment>
    {
        IEnumerable<Appointment> GetAppointmentsByPatientId(Guid id);
    }
}
