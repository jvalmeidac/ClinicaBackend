using MediatR;
using System;

namespace backend.Domain.Commands.Patient.AddPatient
{
    public class AddPatientRequest : IRequest<Response>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Phone { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string CPF { get; private set; }
        public string CNS { get; private set; }
    }
}
