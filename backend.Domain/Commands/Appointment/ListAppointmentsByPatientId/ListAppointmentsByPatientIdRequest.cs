using MediatR;
using System;

namespace backend.Domain.Commands.Appointment.ListAppointmentsByPatientId
{
    public class ListAppointmentsByPatientIdRequest : IRequest<Response>
    {
        public ListAppointmentsByPatientIdRequest(Guid id)
        {
            PatientId = id;
        }
        public Guid PatientId { get; set; }
    }
}
