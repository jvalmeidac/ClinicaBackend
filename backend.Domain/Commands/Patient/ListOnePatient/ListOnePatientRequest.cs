using backend.Domain.Commands.Patient.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Domain.Commands.Patient.ListOnePatient
{
    public class ListOnePatientRequest : RequestBase, IRequest
    {
        public ListOnePatientRequest(Guid id) : base(id)
        {

        }
    }
}
