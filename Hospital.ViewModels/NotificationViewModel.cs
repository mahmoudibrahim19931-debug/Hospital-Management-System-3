using Hospital.Models;
using System;

namespace Hospital.ViewModels
{
    public class NotificationViewModel
    {

        public int Id { get; set; }


        public string UserId { get; set; }


        public string Message { get; set; }


        public bool IsRead { get; set; }

        public string Action { get; set; }

        public DateTime CreatedDate { get; set; }


        public string Url { get; set; }

        public string Controller { get; set; }

        public string Area { get; set; }

        public NotificationViewModel()
        {

        }



        public NotificationViewModel(Notification model)
        {

            Id = model.Id;

            UserId = model.UserId;

            Message = model.Message;

            IsRead = model.IsRead;

            CreatedDate = model.CreatedDate;

            Url = model.Url;

            Action = model.Action;

            Controller = model.Controller;

            Area = model.Area;

        }


    }
}