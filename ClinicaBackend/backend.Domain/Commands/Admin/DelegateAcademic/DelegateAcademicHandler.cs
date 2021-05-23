using System.Threading;
using System.Threading.Tasks;
using backend.Domain.Interfaces.Repositories;
using MediatR;
using prmToolkit.NotificationPattern;

namespace backend.Domain.Commands.Admin.DelegateAcademic
{
    public class DelegateAcademicHandler : Notifiable, IRequestHandler<DelegateAcademicRequest, Response>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAcademicRepository _academicRepository;
        private readonly ISubjectRepository _subjectRepository;

        public DelegateAcademicHandler(IAdminRepository adminRepository,
            IAcademicRepository academicRepository, ISubjectRepository subjectRepository)
        {
            _adminRepository = adminRepository;
            _academicRepository = academicRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<Response> Handle(DelegateAcademicRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", "A requisição não pode ser nula!");
                return new Response(this);
            }

            if (!_academicRepository.Exists(request.IdAcademic)
                || !_subjectRepository.Exists(request.IdSubject))
            {
                AddNotification("Inexistente", "Acadêmico ou matéria não existente!");
                return new Response(this);
            }

            if (_adminRepository.ExistsRelationship(request.IdAcademic, request.IdSubject))
            {
                AddNotification("Relacionamento", "O acadêmico já foi inserido nessa matéria!");
                return new Response(this);
            }

            _adminRepository.DelegateAcademic(request.IdAcademic, request.IdSubject);

            var response = new Response(this, new { Message = "Acadêmico inserido com sucesso!" });

            return await Task.FromResult(response);
        }
    }
}