using backend.Domain.Interfaces.Repositories;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Appointment.ListAppointmentsByPatientId
{
    class ListAppointmentsByPatientIdHandler : Notifiable, 
        IRequestHandler<ListAppointmentsByPatientIdRequest, Response>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public ListAppointmentsByPatientIdHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Response> Handle(ListAppointmentsByPatientIdRequest request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                AddNotification("Request", "A requisição não pode ser nula!");
                return new Response(this);
            }

            var appointments = _appointmentRepository.GetAppointmentsByPatientId(request.PatientId);

            var response = new Response(this, appointments);

            return await Task.FromResult(response);
        }
    }
}
