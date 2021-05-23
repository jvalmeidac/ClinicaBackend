using backend.Domain.Interfaces.Repositories;
using MediatR;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Academic.GetAppointmentsOpened
{
    public class GetAppointmentsOpenedHandler : Notifiable,
        IRequestHandler<GetAppointmentsOpenedRequest, Response>
    {
        private readonly IAcademicRepository _academicRepository;

        public GetAppointmentsOpenedHandler(IAcademicRepository academicRepository)
        {
            _academicRepository = academicRepository;
        }

        public async Task<Response> Handle(GetAppointmentsOpenedRequest request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                AddNotification("Request", "A requisição não pode ser nula!");
                return new Response(this);
            }

            List<Entities.Appointment> appointments = 
                _academicRepository.GetAppointmentsOpened();

            var response = new Response(this, appointments);

            return await Task.FromResult(response);
        }
    }
}
