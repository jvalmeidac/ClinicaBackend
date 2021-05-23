using System;
using MediatR;

namespace backend.Domain.Commands.Admin.DelegateAcademic
{
    public class DelegateAcademicRequest : IRequest<Response>
    {
        public Guid IdAcademic { get; private set; }
        public Guid IdSubject { get; private set; }
    }
}