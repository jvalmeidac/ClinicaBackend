using backend.Domain.Entities;
using backend.Domain.Enums;
using System;

namespace backend.Domain.Value_Objects
{
    public class Appointment
    {
        public Appointment(DateTime appointmentDate, Pacient pacient, AppointmentType appointmentType)
        {
            AppointmentDate = appointmentDate;
            Pacient = pacient;
            AppointmentType = appointmentType;
        }

        public DateTime AppointmentDate { get; private set; }
        public Pacient Pacient { get; private set; }
        public AppointmentType AppointmentType { get; private set; }
    }
}
