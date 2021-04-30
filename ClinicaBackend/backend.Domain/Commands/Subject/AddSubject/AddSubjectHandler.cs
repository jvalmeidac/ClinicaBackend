using backend.Domain.Interfaces.Repositories;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Subject.AddSubject
{
    public class AddSubjectHandler : Notifiable, IRequestHandler<AddSubjectRequest, Response>
    {
        private readonly ISubjectRepository _subjectRepository;

        public AddSubjectHandler(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Response> Handle(AddSubjectRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", "A requisição é inválida!");
                return new Response(this);
            }

            Entities.Subject subject = new(request.Name, request.Description,
                request.Code, request.WeekDay, request.DayPeriod);
            AddNotifications(subject);

            if (IsInvalid())
            {
                return new Response(this);
            }

            _subjectRepository.Add(subject);

            var response = new Response(this, subject);

            return await Task.FromResult(response);
        }
    }
}
