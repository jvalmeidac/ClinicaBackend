using backend.Domain.Extensions;
using prmToolkit.NotificationPattern;
using System;

namespace backend.Domain.Entities.Base
{
    public class EntityBase : Notifiable
    {
        public EntityBase(string firstName, string lastName, string email, string password)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            CreateAt = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(Password))
            {
                Password = Password.Encrypt();
            }
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public DateTime CreateAt { get; set; }

    }
}
