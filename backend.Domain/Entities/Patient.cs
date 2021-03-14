using backend.Domain.Entities.Base;
using backend.Domain.Value_Objects;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;

namespace backend.Domain.Entities
{
    public class Patient : EntityBase
    {
        public Patient(string firstName, string lastName, string email, string password, string phone, DateTime birthDate, string cpf, string cns) 
            : base(firstName, lastName, email, password)
        {
            Phone = phone;
            BirthDate = birthDate;
            CPF = cpf;
            CNS = cns;

            //Valida as informações do paciente
            new AddNotifications<Patient>(this)
                .IfNullOrInvalidLength(x => x.FirstName, 3, 150)
                .IfNullOrInvalidLength(x => x.LastName, 3, 150)
                .IfNotEmail(x => x.Email)
                .IfNotCpf(x => x.CPF)
                .IfNotRange(x => x.CNS.Length, 7, 15, "O número inserido não tem o valor adequado!")
                .IfNotRange(x => x.Password.Length, 6, 10, "A senha precisa ter entre 6 e 10 caracteres!");
        }

        public string Phone { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string CPF { get; private set; }
        public string CNS { get; private set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
