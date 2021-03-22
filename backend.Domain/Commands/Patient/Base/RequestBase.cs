using MediatR;
using System;

namespace backend.Domain.Commands.Patient.Base
{
    public class RequestBase: IRequest<Response>
    {
        public Guid Id { get; set; }
    }
}
