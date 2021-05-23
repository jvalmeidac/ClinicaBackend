using backend.Domain.Extensions;
using backend.Domain.Interfaces.Repositories;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Patient.AuthenticatePatient
{
    public class AuthenticatePatientHandler : Notifiable,
        IRequestHandler<AuthenticatePatientRequest, AuthenticatePatientResponse>
    {
        private readonly IPatientRepository _patientRepository;

        public AuthenticatePatientHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<AuthenticatePatientResponse> Handle(AuthenticatePatientRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", "A requisição não pode ser nula");
                return null;
            }

            request.Password = request.Password.Encrypt();

            Entities.Patient patient = _patientRepository.Login(request.Email, request.Password);

            if (patient == null)
            {
                AddNotification("Autenticação", "Email ou senha inválidos!");
                return new AuthenticatePatientResponse
                {
                    Authenticated = false
                };
            }

            var response = (AuthenticatePatientResponse)patient;

            return await Task.FromResult(response);
        }
    }
}
