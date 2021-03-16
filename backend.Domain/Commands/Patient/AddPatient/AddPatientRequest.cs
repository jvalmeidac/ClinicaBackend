using MediatR;
using System;

namespace backend.Domain.Commands.Patient.AddPatient
{
    public class AddPatientRequest : IRequest<Response>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
    }
}
