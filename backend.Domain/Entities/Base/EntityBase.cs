using prmToolkit.NotificationPattern;
using System;

namespace backend.Domain.Entities.Base
{
    public class EntityBase : Notifiable
    {
        public EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; set; }
    }
}
