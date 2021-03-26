using System;

namespace backend.Domain.Commands.Patient.AuthenticatePatient
{
    public class AuthenticatePatientResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public bool Authenticated { get; set; }

        public static explicit operator AuthenticatePatientResponse(Entities.Patient patient)
        {
            return new AuthenticatePatientResponse()
            {
                Id = Guid.Parse(patient.PatientId),
                FirstName = patient.FirstName,
                Authenticated = true
            };
        }
    }
}
