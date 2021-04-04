using backend.Domain.Enums;
using prmToolkit.NotificationPattern;
using System;

namespace backend.Domain.Entities
{
    public class Appointment : Notifiable
    {
        public Appointment(DateTime schedule, Guid patientId, AppointmentType appointmentType)
        {
            AppointmentId = Guid.NewGuid().ToString();
            Description = null;
            Schedule = schedule;
            PatientId = patientId.ToString();
            AppointmentType = appointmentType;
            AppointmentStatus = AppointmentStatus.Scheduled;
        }

        public Appointment() { }

        public string AppointmentId { get; private set; }
        public string Description { get; private set; }
        public DateTime Schedule { get; private set; }
        public string PatientId { get; private set; }
        public AppointmentType AppointmentType { get; private set; }
        public AppointmentStatus AppointmentStatus { get; private set; }

        public Patient Patient { get; set; }
        public Operator Operator { get; set; }

        public void AddDescription(string description)
        {
            Description = description;
        }
    }
}
