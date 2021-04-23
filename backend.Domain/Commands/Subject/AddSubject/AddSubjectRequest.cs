using backend.Domain.Enums.Subject;
using MediatR;

namespace backend.Domain.Commands.Subject.AddSubject
{
    public class AddSubjectRequest : IRequest<Response>
    {
        public AddSubjectRequest(string name, string description, string code,
            SubjectWeekDay weekDay, SubjectDayPeriod dayPeriod)
        {
            Name = name;
            Description = description;
            Code = code;
            WeekDay = weekDay;
            DayPeriod = dayPeriod;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Code { get; private set; }
        public SubjectWeekDay WeekDay { get; private set; }
        public SubjectDayPeriod DayPeriod { get; private set; }
    }
}
