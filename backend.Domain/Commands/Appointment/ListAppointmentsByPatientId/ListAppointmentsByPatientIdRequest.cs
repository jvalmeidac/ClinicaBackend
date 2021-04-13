using backend.Domain.Pagination;
using MediatR;
using System;

namespace backend.Domain.Commands.Appointment.ListAppointmentsByPatientId
{
    public class ListAppointmentsByPatientIdRequest : IRequest<Response>
    {
        public ListAppointmentsByPatientIdRequest(Guid id, PageParameters pageParameters)
        {
            PatientId = id;
            PageParameters = pageParameters;
        }
        public Guid PatientId { get; set; }
        public PageParameters PageParameters { get; set; }
    }
}
