using backend.Domain.Entities.Base;
using backend.Domain.Extensions;
using backend.Domain.Value_Objects;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;

namespace backend.Domain.Entities
{
    public class Patient : EntityBase
    {
        public Patient(string firstName, string lastName, string email, 
            string password, string phone, DateTime birthDate, string cpf, string rg)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Phone = phone;
            BirthDate = birthDate;
            CPF = cpf;
            RG = rg;

            if (!string.IsNullOrWhiteSpace(Password))
            {
                Password = Password.Encrypt();
            }

            //Valida as informações do paciente
            new AddNotifications<Patient>(this)
                .IfNullOrInvalidLength(x => x.FirstName, 3, 150)
                .IfNullOrInvalidLength(x => x.LastName, 3, 150)
                .IfNullOrEmpty(x => x.Password, "A senha não pode ser nula ou vazia!")
                .IfNotEmail(x => x.Email)
                .IfNotCpf(x => x.CPF)
                .IfNotRange(x => x.RG.Length, 8, 13, "O RG não possui os caracteres adequados!");
        }

        protected Patient()
        {

        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Phone { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string CPF { get; private set; }
        public string RG { get; private set; }

        public ICollection<Appointment> Appointments { get; set; }

        public void EditPatient(string firstName, string lastName, string email, 
            string password, string phone, DateTime birthDate, string cpf, string rg)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Phone = phone;
            BirthDate = birthDate;
            CPF = cpf;
            RG = rg;
        }
    }
}
