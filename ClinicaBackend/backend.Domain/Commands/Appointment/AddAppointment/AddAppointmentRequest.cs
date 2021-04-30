using backend.Domain.Enums;
using MediatR;
using System;

namespace backend.Domain.Commands.Appointment.AddAppointment
{
    public class AddAppointmentRequest : IRequest<Response>
    {
        public DateTime Schedule { get; set; }
        public string Description { get; set; }
        public Guid PatientId { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
    }
}
