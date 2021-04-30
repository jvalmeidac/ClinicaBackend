using backend.Domain.Interfaces.Repositories;
using backend.Domain.Pagination;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Academic.ListAllAcademics
{
    public class ListAllAcademicsHandler : Notifiable, IRequestHandler<ListAllAcademicsRequest, Response>
    {
        private readonly IAcademicRepository _academicRepository;

        public ListAllAcademicsHandler(IAcademicRepository academicRepository)
        {
            _academicRepository = academicRepository;
        }

        public async Task<Response> Handle(ListAllAcademicsRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", "A requisição é inválida!");
                return new Response(this);
            }

            if (IsInvalid())
            {
                return new Response(this);
            }

            List<Entities.Academic> academics = _academicRepository.GetAll(request.PageParameters);
            int count = _academicRepository.GetAcademicsCount();
            var paginationInfo = new PagedList<Entities.Academic>(academics, count,
                request.PageParameters.PageNumber, request.PageParameters.PageSize);

            var response = new Response(this, academics, paginationInfo);

            return await Task.FromResult(response);
        }
    }
}
