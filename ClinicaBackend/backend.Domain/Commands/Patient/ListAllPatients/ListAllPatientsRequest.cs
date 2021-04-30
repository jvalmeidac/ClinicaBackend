using backend.Domain.Pagination;
using MediatR;

namespace backend.Domain.Commands.Patient.ListAllPatients
{
    public class ListAllPatientsRequest : IRequest<Response>
    {
        public ListAllPatientsRequest(PageParameters pageParameters)
        {
            PageParameters = pageParameters;
        }
        public PageParameters PageParameters { get; private set; }
    }
}
