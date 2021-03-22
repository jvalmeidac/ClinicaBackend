using prmToolkit.NotificationPattern;
using System.Collections.Generic;

namespace backend.Domain.Commands
{
    public class Response
    {
        public Response(INotifiable notifiable)
        {
            Success = notifiable.IsValid();
            Notifications = notifiable.Notifications;
        }

        public Response(INotifiable notifiable, object data)
        {
            Success = notifiable.IsValid();
            Data = data;
            Notifications = notifiable.Notifications;
        }

        public Response(INotifiable notifiable, object data, int pages)
        {
            Success = notifiable.IsValid();
            Data = data;
            Notifications = notifiable.Notifications;
            Pages = pages;
        }

        public IEnumerable<Notification> Notifications { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
        public int Pages { get; set; }
    }
}
