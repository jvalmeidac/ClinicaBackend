using backend.Domain.Entities;
using backend.Domain.Enums;
using System;

namespace backend.Domain.Value_Objects
{
    public class Appointment
    {
        public Appointment(DateTime appointmentDate, Patient patient, AppointmentType appointmentType)
        {
            AppointmentDate = appointmentDate;
            Patient = patient;
            AppointmentType = appointmentType;
        }

        public DateTime AppointmentDate { get; private set; }
        public Patient Patient { get; private set; }
        public AppointmentType AppointmentType { get; private set; }
    }
}
