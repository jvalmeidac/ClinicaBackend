using backend.Domain.Extensions;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;

namespace backend.Domain.Entities
{
    public class Academic : Notifiable
    {
        public Academic(string firstName, string lastName, string email, string password, string registration)
        {
            IdAcademic = Guid.NewGuid().ToString();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Registration = registration;
            CreatedAt = DateTime.Now;

            //Criptografa a senha do acadêmico
            if (!string.IsNullOrWhiteSpace(Password))
            {
                Password = Password.Encrypt();
            }

            //Valida as informações do acadêmico
            new AddNotifications<Academic>(this)
                .IfNullOrInvalidLength(x => x.FirstName, 3, 150)
                .IfNullOrInvalidLength(x => x.LastName, 3, 150)
                .IfNullOrEmpty(x => x.Password, "A senha não pode ser nula ou vazia!")
                .IfNotEmail(x => x.Email);
        }

        protected Academic() { }

        public string IdAcademic { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Registration { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Appointment> Appointments { get; set; }
    }

}
