using backend.Domain.Entities.Base;
using backend.Domain.Value_Objects;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;

namespace backend.Domain.Entities
{
    public class Patient : EntityBase
    {
        public Patient(string firstName, string lastName, string email, string password, string phone, DateTime birthDate, string cpf, string rg) 
            : base(firstName, lastName, email, password)
        {
            Phone = phone;
            BirthDate = birthDate;
            CPF = cpf;
            RG = rg;

            //Valida as informações do paciente
            new AddNotifications<Patient>(this)
                .IfNullOrInvalidLength(x => x.FirstName, 3, 150)
                .IfNullOrInvalidLength(x => x.LastName, 3, 150)
                .IfNotEmail(x => x.Email)
                .IfNotCpf(x => x.CPF)
                .IfNotRange(x => x.RG.Length, 8, 13, "O RG não possui os caracteres adequados!");
        }

        public string Phone { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string CPF { get; private set; }
        public string RG { get; private set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
