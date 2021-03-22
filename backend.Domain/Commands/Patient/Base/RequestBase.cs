using MediatR;
using System;

namespace backend.Domain.Commands.Patient.Base
{
    public class RequestBase: IRequest<Response>
    {
        public RequestBase(Guid id)
        {
            Id = id;
        }

        protected RequestBase()
        {

        }
        public Guid Id { get; set; }
    }
}
