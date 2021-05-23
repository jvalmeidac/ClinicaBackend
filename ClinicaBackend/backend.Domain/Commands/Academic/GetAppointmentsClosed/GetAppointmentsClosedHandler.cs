using backend.Domain.Interfaces.Repositories;
using MediatR;
using prmToolkit.NotificationPattern;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Academic.GetAppointmentsClosed
{
    public class GetAppointmentsClosedHandler :
        Notifiable, IRequestHandler<GetAppointmentsClosedRequest, Response>
    {
        private readonly IAcademicRepository _academicRepository;

        public GetAppointmentsClosedHandler( 
            IAcademicRepository academicRepository)
        {
            _academicRepository = academicRepository;
        }

        public async Task<Response> Handle(GetAppointmentsClosedRequest request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                AddNotification("Request", "A requisição não pode ser nula!");
                return new Response(this);
            }

            if (!_academicRepository.Exists(request.AcademicId))
            {
                AddNotification("Inexistente", "Acadêmico não encontrado!");
                return new Response(this);
            }

            List<Entities.Appointment> appointments = 
                _academicRepository.GetAppointmentsClosed(request.AcademicId);

            var response = new Response(this, appointments);

            return await Task.FromResult(response);
        }
    }
}
