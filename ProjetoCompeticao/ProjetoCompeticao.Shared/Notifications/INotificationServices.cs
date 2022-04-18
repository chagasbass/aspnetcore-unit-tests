using Flunt.Notifications;
using ProjetoCompeticao.Shared.Enums;

namespace ProjetoCompeticao.Shared.Notifications
{
    public interface INotificationServices
    {
        public StatusCodeOperation StatusCode { get; set; }

        void AddNotification(Notification notification);
        void AddNotification(Notification notificacao, StatusCodeOperation statusCode);
        void AddNotifications(IEnumerable<Notification> notifications);
        void AddNotifications(IEnumerable<Notification> notifications, StatusCodeOperation statusCode);
        void AddStatusCode(StatusCodeOperation statusCode);
        IEnumerable<Notification> GetNotifications();
        bool HasNotifications();
        void ClearNotifications();
    }
}
