using backend.Domain.Interfaces.Repositories;
using backend.Domain.Interfaces.Repositories.Base;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Patient.EditPatient
{
    public class EditPatientHandler : Notifiable, IRequestHandler<EditPatientRequest, Response>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnityOfWork _unityOfWork;

        public EditPatientHandler(IPatientRepository patientRepository,
            IUnityOfWork unityOfWork
            )
        {
            _patientRepository = patientRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task<Response> Handle(EditPatientRequest request, CancellationToken cancellationToken)
        {
            //Verifica se a requisição é nula
            if (request == null)
            {
                AddNotification("Request", "A requisição é nula");
                return new Response(this);
            }

            //Verifica se existe o paciente no banco
            if (!_patientRepository.Exists(request.Id))
            {
                AddNotification("Paciente inexistente", "O paciente informado não foi encontrado!");
                return new Response(this);
            }

            Entities.Patient patient = _patientRepository.GetOne(request.Id);
            patient.EditPatient(request.FirstName, request.LastName, request.Email,
                request.Password, request.Phone, request.BirthDate, request.CPF, request.RG, request.CEP,
                request.Address, request.District, request.Complement, request.City, request.State);

            //Valida a requisição
            if (IsInvalid())
            {
                return new Response(this);
            }

            //Salva as alterações no banco
            _unityOfWork.BeginTransaction();
            patient = _patientRepository.Edit(patient);
            _unityOfWork.Commit();

            //Cria o objeto da resposta
            var result = new { Patient = patient };
            var response = new Response(this, result);

            //Retorna a resposta
            return await Task.FromResult(response);
        }
    }
}
