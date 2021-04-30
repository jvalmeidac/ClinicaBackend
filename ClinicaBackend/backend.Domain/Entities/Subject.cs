using backend.Domain.Enums.Subject;
using prmToolkit.NotificationPattern;
using System;

namespace backend.Domain.Entities
{
    public class Subject : Notifiable
    {
        public Subject(string name, string description, string code, 
            SubjectWeekDay weekDay, SubjectDayPeriod dayPeriod)
        {
            SubjectId = Guid.NewGuid().ToString();
            Name = name;
            Description = description;
            Code = code;
            WeekDay = weekDay;
            DayPeriod = dayPeriod;

            new AddNotifications<Subject>(this)
                .IfLengthGreaterThan(Name, 150, "O nome não pode ser maior que 150 caracteres")
                .IfLengthGreaterThan(Description, 300, "A descrição não pode ser maior que 300 caracteres!")
                .IfEnumInvalid(WeekDay, "Insira um valor válido!")
                .IfEnumInvalid(DayPeriod, "Insira um valor válido!");
        }

        protected Subject() { }

        public string SubjectId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Code { get; private set; }
        public SubjectWeekDay WeekDay { get; private set; }
        public SubjectDayPeriod DayPeriod { get; private set; }
    }
}
