using backend.Domain.Interfaces.Repositories;
using backend.Domain.Interfaces.Repositories.Base;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Academic.RemoveAcademic
{
    public class RemoveAcademicHandler : Notifiable, IRequestHandler<RemoveAcademicRequest, Response>
    {
        private readonly IAcademicRepository _academicRepository;
        private readonly IUnityOfWork _unityOfWork;

        public RemoveAcademicHandler(IAcademicRepository academicRepository, IUnityOfWork unityOfWork)
        {
            _academicRepository = academicRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task<Response> Handle(RemoveAcademicRequest request, CancellationToken cancellationToken)
        {
            //Verifica se a requisição é nula
            if (request == null)
            {
                AddNotification("Resquest", "A requisição não pode ser nula!");
                return new Response(this);
            }

            //Verifica se o acadêmico existe
            if (!_academicRepository.Exists(request.Id))
            {
                AddNotification("Acadêmico inválido", "O acadêmico informado não foi encontrado!");
                return new Response(this);
            }

            //Valida a requisição
            if (IsInvalid())
            {
                return new Response(this);
            }

            //Remove o acadêmico do banco
            _unityOfWork.BeginTransaction();
            _academicRepository.Remove(request.Id);
            _unityOfWork.Commit();

            //Cria o objeto da resposta
            var result = new { request.Id };
            var response = new Response(this, result);

            //Retorna a resposta
            return await Task.FromResult(response);
        }
    }
}
