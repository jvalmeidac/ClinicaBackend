using MediatR;

namespace backend.Domain.Commands.Patient.AuthenticatePatient
{
    public class AuthenticatePatientRequest : IRequest<AuthenticatePatientResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
