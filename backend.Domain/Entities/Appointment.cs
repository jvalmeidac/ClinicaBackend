using System;

namespace backend.Domain.Entities
{
    public class Appointment
    {
        public Appointment(DateTime schedule, Guid idPatient)
        {
            AppointmentId = Guid.NewGuid().ToString();
            Schedule = schedule;
            Description = null;
            Completed = false;
            PatientId = idPatient;
        }

        public Appointment() { }

        public string AppointmentId { get; private set; }
        public DateTime Schedule { get; private set; }
        public string Description { get; private set; }
        public bool Completed { get; private set; }
        public Guid PatientId { get; private set; }

        public Patient Patient { get; set; }
        public Operator Operator { get; set; }

        public void AddDescription(string description)
        {
            Description = description;
        }
    }
}
