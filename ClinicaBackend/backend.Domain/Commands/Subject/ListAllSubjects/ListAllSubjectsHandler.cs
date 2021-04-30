using backend.Domain.Interfaces.Repositories;
using backend.Domain.Pagination;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Subject.ListAllSubjects
{
    public class ListAllSubjectsHandler : Notifiable, IRequestHandler<ListAllSubjectsRequest, Response>
    {
        private readonly ISubjectRepository _subjectRepository;

        public ListAllSubjectsHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Response> Handle(ListAllSubjectsRequest request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                AddNotification("Request","A requisição é inválida!");
                return new Response(this);
            }

            List<Entities.Subject> subjects = _subjectRepository.GetAll(request.PageParameters);
            int count = _subjectRepository.GetSubjectsCount();

            var paginationInfo = new PagedList<Entities.Subject>(subjects, count, 
                request.PageParameters.PageNumber, request.PageParameters.PageSize);

            var response = new Response(this, subjects, paginationInfo);

            return await Task.FromResult(response);
        }
    }
}
