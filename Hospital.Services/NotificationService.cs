using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;


        public NotificationService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        public void Create(

            string userId,

            string message,

            string area,

            string controller,

            string action,

            string url)

        {

            var notification = new Notification
            {

                UserId = userId,

                Message = message,

                Area = area,

                Controller = controller,

                Action = action,

                Url = url,

                CreatedDate = DateTime.Now,

                IsRead = false

            };



            _unitOfWork
                .GenericRepository<Notification>()
                .Add(notification);



            _unitOfWork.Save();

        }







        public List<NotificationViewModel>
            GetUserNotifications(string userId)
        {

            return _unitOfWork
                .GenericRepository<Notification>()
                .GetAll(x => x.UserId == userId)

                .OrderByDescending(x => x.CreatedDate)

                .Select(x => new NotificationViewModel(x))

                .ToList();

        }






        public int UnreadCount(string userId)
        {

            return _unitOfWork
                .GenericRepository<Notification>()

                .GetAll(

                    x =>

                    x.UserId == userId

                    &&

                    !x.IsRead

                )

                .Count();

        }







        public void MarkAsRead(int id)
        {

            var notification =

                _unitOfWork
                .GenericRepository<Notification>()
                .GetById(id);



            if (notification == null)
                return;



            notification.IsRead = true;



            _unitOfWork
                .GenericRepository<Notification>()
                .Update(notification);



            _unitOfWork.Save();

        }







        public void MarkAllAsRead(string userId)
        {

            var notifications =

                _unitOfWork
                .GenericRepository<Notification>()

                .GetAll(

                    x =>

                    x.UserId == userId

                    &&

                    !x.IsRead

                )

                .ToList();




            foreach (var item in notifications)
            {

                item.IsRead = true;

            }



            _unitOfWork.Save();

        }







        public NotificationViewModel GetById(int id)
        {

            var notification =

                _unitOfWork
                .GenericRepository<Notification>()
                .GetById(id);



            if (notification == null)
                return null;



            return new NotificationViewModel(notification);

        }


    }
}