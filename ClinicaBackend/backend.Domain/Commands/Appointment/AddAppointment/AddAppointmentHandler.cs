using backend.Domain.Interfaces.Repositories;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Appointment.AddAppointment
{
    public class AddAppointmentHandler : Notifiable, IRequestHandler<AddAppointmentRequest, Response>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AddAppointmentHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Response> Handle(AddAppointmentRequest request, CancellationToken cancellationToken)
        {
            //Verifica se a requisição é nula
            if(request == null)
            {
                AddNotification("Request", "A requisição não pode ser nula!");
                return new Response(this);
            }

            //Instancia uma nova consulta e valida os dados inseridos
            Entities.Appointment appointment = new(request.Schedule, request.PatientId,
                request.AppointmentType);
            AddNotifications(appointment);

            if (IsInvalid())
            {
                return new Response(this);
            }

            //Insere os dados no banco
            _appointmentRepository.Add(appointment);

            //Cria o objeto da resposta
            var response = new Response(this, appointment);

            //Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
