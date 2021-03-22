using prmToolkit.NotificationPattern;
using System;

namespace backend.Domain.Entities.Base
{
    public class EntityBase : Notifiable
    {
        public EntityBase()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
        }

        public string Id { get; private set; }
        public DateTime CreatedAt { get; set; }
    }
}
