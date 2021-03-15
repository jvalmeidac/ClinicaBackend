using backend.Domain.Interfaces.Repositories;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Patient.AddPatient
{
    public class AddPatientHandler : Notifiable, IRequestHandler<AddPatientRequest, Response>
    {
        //Injeção de Dependência
        private readonly IPatientRepository _patientRepository;

        public AddPatientHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Response> Handle(AddPatientRequest request, CancellationToken cancellationToken)
        {
            //Verifica se a requisição é válida
            if (request == null)
            {
                AddNotification("Request", "Informe os dados do paciente!");
                return new Response(this);
            }

            //Verifica se o paciente já está cadastrado
            if (_patientRepository.Exists(x => x.Email == request.Email || 
                x.CPF == request.CPF || x.CNS == request.CNS))
            {
                AddNotification("Paciente", "Paciente já cadastrado");
                return new Response(this);
            }

            //Instancia o paciente e verifica se existe algum dado inválido
            Entities.Patient patient = new(request.FirstName, request.LastName,
                request.Email, request.Password, request.Phone, request.BirthDate, request.CPF, request.CNS);
            AddNotifications(patient);

            if (IsInvalid())
            {
                return new Response(this);
            }

            //Insere os dados no banco
            patient = _patientRepository.Add(patient);

            //Cria o objeto da resposta
            var response = new Response(this, patient);

            //Retorna a resposta
            return await Task.FromResult(response);
        }
    }
}
