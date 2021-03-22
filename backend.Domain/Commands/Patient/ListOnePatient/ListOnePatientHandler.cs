using backend.Domain.Commands.Patient.Base;
using backend.Domain.Interfaces.Repositories;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Patient.ListOnePatient
{
    public class ListOnePatientHandler : Notifiable, IRequestHandler<RequestBase, Response>
    {
        private readonly IPatientRepository _patientRepository;

        public ListOnePatientHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Response> Handle(RequestBase request, CancellationToken cancellationToken)
        {
            //Verifica se a requisição é válida
            if (request == null)
            {
                AddNotification("Request", "A requisição é inválida!");
                return new Response(this);
            }

            //Verifica se o paciente informado existe no banco
            if (!_patientRepository.Exists(request.Id))
            {
                AddNotification("Paciente", "Paciente não encontrado");
                return new Response(this);
            }

            if (IsInvalid())
            {
                return new Response(this);
            }

            //Busca o paciente pelo ID
            Entities.Patient patient 
                = _patientRepository.GetOne(request.Id);

            //Cria o objeto da resposta
            var response = new Response(this, patient);

            //Retorna o resultado da ação
            return await Task.FromResult(response);
        }
    }
}
