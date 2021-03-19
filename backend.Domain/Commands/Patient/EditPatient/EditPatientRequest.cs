using backend.Domain.Commands.Patient.Base;
using MediatR;
using System;

namespace backend.Domain.Commands.Patient.EditPatient
{
    public class EditPatientRequest : RequestBase, IRequest<Response>
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
