using MediatR;

namespace backend.Domain.Commands.Operator.AddOperator
{
    public class AddAcademicRequest : IRequest<Response>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Registration { get; set; }
    }
}
