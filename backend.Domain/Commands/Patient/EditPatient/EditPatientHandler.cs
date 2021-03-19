using backend.Domain.Interfaces.Repositories;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Patient.EditPatient
{
    public class EditPatientHandler : Notifiable, IRequestHandler<EditPatientRequest, Response>
    {
        private readonly IPatientRepository _patientRepository;

        public EditPatientHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Response> Handle(EditPatientRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Request", "A requisição é nula");
                return new Response(this);
            }

            if (!_patientRepository.Exists(x => x.Id == request.Id))
            {
                AddNotification("Paciente inexistente", "O paciente informado não foi encontrado!");
                return new Response(this);
            }

            Entities.Patient patient = _patientRepository.GetOne(request.Id);
            patient.EditPatient(request.FirstName, request.LastName, request.Email,
                request.Password, request.Phone, request.BirthDate, request.CPF, request.RG);

            if (IsInvalid())
            {
                return new Response(this);
            }

            patient = _patientRepository.Edit(patient);

            var result = new { Patient = patient };

            var response = new Response(this, result);

            return await Task.FromResult(response);
        }
    }
}
