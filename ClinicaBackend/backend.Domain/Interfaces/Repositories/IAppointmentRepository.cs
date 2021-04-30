using backend.Domain.Entities;
using backend.Domain.Interfaces.Repositories.Base;
using backend.Domain.Pagination;
using System;
using System.Collections.Generic;

namespace backend.Domain.Interfaces.Repositories
{
    public interface IAppointmentRepository : IRepositoryBase<Appointment>
    {
        List<Appointment> GetAppointmentsByPatientId(Guid id, PageParameters pageParameters);
        int GetAppointmentsCount(Guid id);
    }
}
