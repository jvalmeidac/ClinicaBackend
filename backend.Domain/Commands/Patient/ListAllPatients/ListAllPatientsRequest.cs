using backend.Domain.Pagination;
using MediatR;

namespace backend.Domain.Commands.Patient.ListAllPatients
{
    public class ListAllPatientsRequest : IRequest<Response>
    {
        public PageParameters pageParameters { get; set; }
    }
}
