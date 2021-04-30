using backend.Domain.Interfaces.Repositories;
using backend.Domain.Pagination;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Appointment.ListAppointmentsByPatientId
{
    public class ListAppointmentsByPatientIdHandler : Notifiable, 
        IRequestHandler<ListAppointmentsByPatientIdRequest, Response>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public ListAppointmentsByPatientIdHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Response> Handle(ListAppointmentsByPatientIdRequest request, 
            CancellationToken cancellationToken)
        {
            if(request == null)
            {
                AddNotification("Request", "A requisição não pode ser nula!");
                return new Response(this);
            }

            List<Entities.Appointment> appointments = 
                _appointmentRepository.GetAppointmentsByPatientId(request.PatientId, request.PageParameters).ToList();
            int count = 
                _appointmentRepository.GetAppointmentsCount(request.PatientId);
            var paginationInfo = new PagedList<Entities.Appointment>
                (appointments, count, request.PageParameters.PageNumber, request.PageParameters.PageSize);

            var response = new Response(this, appointments, paginationInfo);

            return await Task.FromResult(response);
        }
    }
}
