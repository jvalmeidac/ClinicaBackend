using backend.Domain.Interfaces.Repositories;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Operator.AddOperator
{
    public class AddAcademicHandler : Notifiable, IRequestHandler<AddAcademicRequest, Response>
    {
        private readonly IAcademicRepository _academicRepository;

        public AddAcademicHandler( IAcademicRepository academicRepository)
        {
            _academicRepository = academicRepository;
        }

        public async Task<Response> Handle(AddAcademicRequest request, CancellationToken cancellationToken)
        {
            //Verifica se a requisição é nula
            if(request == null)
            {
                AddNotification("Request", "A requisição não pode ser nula!");
                return new Response(this);
            }

            //Instancia o acadêmico e valida suas informações
            Entities.Academic academic = 
                new(request.FirstName, request.LastName, request.Email, request.Registration);
            AddNotifications(academic);

            if (IsInvalid())
            {
                return new Response(this);
            }

            //Salva os dados no banco
            _academicRepository.Add(academic);

            //Cria o objeto da resposta
            var response =  new Response(this, academic);

            //Retorna a resposta
            return await Task.FromResult(response);
        }
    }
}
