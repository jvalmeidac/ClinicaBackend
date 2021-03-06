using backend.Domain.Interfaces.Repositories;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Patient.RemovePatient
{
    public class RemovePatientHandler : Notifiable, IRequestHandler<RemovePatientRequest, Response>
    {
        private readonly IPatientRepository _patientRepository;

        public RemovePatientHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Response> Handle(RemovePatientRequest request, CancellationToken cancellationToken)
        {
            //Verifica se a requisição é nula
            if (request == null)
            {
                AddNotification("Request", "A requisição é inválida!");
                return new Response(this);
            }

            //Verifica se o paciente informado existe no banco
            if (!_patientRepository.Exists(request.Id))
            {
                AddNotification("Paciente Inexistente", "O paciente informado não existe!");
                return new Response(this);
            }

            //Valida a requisição
            if (IsInvalid())
            {
                return new Response(this);
            }

            //Remove o paciente do banco
            _patientRepository.Remove(request.Id);

            //Cria o objeto da resposta
            var result = new { request.Id };
            var response = new Response(this, result);

            //Retorna o resultado da ação
            return await Task.FromResult(response);
        }
    }
}
