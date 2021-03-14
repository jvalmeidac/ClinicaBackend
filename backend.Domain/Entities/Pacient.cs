using backend.Domain.Entities.Base;
using backend.Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace backend.Domain.Entities
{
    public class Pacient : EntityBase
    {
        public Pacient(string firstName, string lastName, string email, string password, string phone, DateTime birthDate, string cpf, string cns) 
            : base(firstName, lastName, email, password)
        {
            Phone = phone;
            BirthDate = birthDate;
            CPF = cpf;
            CNS = cns;
        }

        public string Phone { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string CPF { get; private set; }
        public string CNS { get; private set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
