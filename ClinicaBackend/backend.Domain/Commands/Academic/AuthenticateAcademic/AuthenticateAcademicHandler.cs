using backend.Domain.Extensions;
using backend.Domain.Interfaces.Repositories;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Academic.AuthenticateAcademic
{
    public class AuthenticateAcademicHandler : Notifiable, 
        IRequestHandler<AuthenticateAcademicRequest, AuthenticateAcademicReponse>
    {
        private readonly IAcademicRepository _academicRepository;

        public AuthenticateAcademicHandler(IAcademicRepository academicRepository)
        {
            _academicRepository = academicRepository;
        }

        public async Task<AuthenticateAcademicReponse> Handle(AuthenticateAcademicRequest request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                AddNotification("Request", "A requisição não pode ser nula!");
                return null;
            }

            request.Password = request.Password.Encrypt();

            Entities.Academic academic = 
                _academicRepository.Login(request.Email, request.Password);

            if(academic == null)
            {
                AddNotification("Autenticação", "Email ou senha inválidos!");
                return new AuthenticateAcademicReponse
                {
                    Authenticated = false
                };
            }

            var response = (AuthenticateAcademicReponse)academic;

            return await Task.FromResult(response);
        }
    }
}
