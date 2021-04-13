using backend.Domain.Commands.Base;
using MediatR;

namespace backend.Domain.Commands.Academic.RemoveAcademic
{
    public class RemoveAcademicRequest : RequestBase, IRequest<Response>
    {
        public RemoveAcademicRequest()
        {
        }
    }
}
