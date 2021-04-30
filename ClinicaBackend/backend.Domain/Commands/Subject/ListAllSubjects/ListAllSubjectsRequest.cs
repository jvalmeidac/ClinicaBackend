using backend.Domain.Pagination;
using MediatR;

namespace backend.Domain.Commands.Subject.ListAllSubjects
{
    public class ListAllSubjectsRequest : IRequest<Response>
    {
        public ListAllSubjectsRequest(PageParameters pageParameters)
        {
            PageParameters = pageParameters;
        }
        public PageParameters PageParameters { get; set; }
    }
}
