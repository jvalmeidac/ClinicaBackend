using prmToolkit.NotificationPattern;
using System.Collections.Generic;

namespace backend.Domain.Commands
{
    public class Response
    {
        private object _pagination;
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

        public Response(INotifiable notifiable, object data, dynamic paginationInfo)
        {
            Success = notifiable.IsValid();
            Data = data;
            Notifications = notifiable.Notifications;
            Pagination = new
            {
                TotalPages = paginationInfo.TotalPages,
                CurrentPage = paginationInfo.CurrentPage,
                HasNext = paginationInfo.HasNext,
                HasPrevious = paginationInfo.HasPrevious,
                PageSize = paginationInfo.PageSize
            };
        }

        public IEnumerable<Notification> Notifications { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
        public object Pagination {
            get
            {
                return _pagination ?? "No pagination avaliable";
            }
            set
            {
                _pagination = value;
            }
        }
    }
}
