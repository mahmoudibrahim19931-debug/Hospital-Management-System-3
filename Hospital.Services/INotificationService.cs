using Hospital.ViewModels;
using System.Collections.Generic;

namespace Hospital.Services
{

    public interface INotificationService
    {


        void Create(

    string userId,

    string message,

    string area,

    string controller,

    string action,

    string url

);



        List<NotificationViewModel>
            GetUserNotifications(string userId);



        int UnreadCount(string userId);



        void MarkAsRead(int id);



        void MarkAllAsRead(string userId);
        NotificationViewModel GetById(int id);

    }


}