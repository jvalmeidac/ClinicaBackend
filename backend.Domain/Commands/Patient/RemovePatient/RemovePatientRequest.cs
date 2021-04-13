using backend.Domain.Commands.Base;
using MediatR;
using System;

namespace backend.Domain.Commands.Patient.RemovePatient
{
    public class RemovePatientRequest : RequestBase, IRequest<Response>
    {
        public RemovePatientRequest(Guid id) : base(id)
        {

        }
    }
}
