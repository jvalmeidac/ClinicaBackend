using MediatR;
using System;

namespace backend.Domain.Commands.Academic.GetAppointmentsClosed
{
    public class GetAppointmentsClosedRequest : IRequest<Response>
    {
        public GetAppointmentsClosedRequest(Guid academicId)
        {
            AcademicId = academicId;
        }
        public Guid AcademicId { get; set; }
    }
}
