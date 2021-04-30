using backend.Domain.Pagination;
using MediatR;

namespace backend.Domain.Commands.Academic.ListAllAcademics
{
    public class ListAllAcademicsRequest : IRequest<Response>
    {
        public ListAllAcademicsRequest(PageParameters pageParameters)
        {
            PageParameters = pageParameters;
        }

        public PageParameters PageParameters { get; private set; }
    }
}
