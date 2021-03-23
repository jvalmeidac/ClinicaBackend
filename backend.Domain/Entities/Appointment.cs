using backend.Domain.Entities.Base;
using System;

namespace backend.Domain.Entities
{
    public class Appointment : EntityBase
    {
        public Appointment(DateTime schedule, Guid patientId)
        {
            Schedule = schedule;
            Description = null;
            Completed = false;
            PatientId = patientId;
        }

        public DateTime Schedule { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public Guid PatientId { get; set; }

        public Patient Patient { get; set; }
        public Operator Operator { get; set; }

        public void AddDescription(string description)
        {
            Description = description;
        }
    }
}
