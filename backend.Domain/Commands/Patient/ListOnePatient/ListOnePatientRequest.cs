using backend.Domain.Commands.Base;
using MediatR;
using System;

namespace backend.Domain.Commands.Patient.ListOnePatient
{
    public class ListOnePatientRequest : RequestBase, IRequest
    {
        public ListOnePatientRequest(Guid id) : base(id)
        {

        }
    }
}
