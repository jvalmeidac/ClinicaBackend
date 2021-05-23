using MediatR;

namespace backend.Domain.Commands.Academic.AuthenticateAcademic
{
    public class AuthenticateAcademicRequest : IRequest<AuthenticateAcademicReponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
