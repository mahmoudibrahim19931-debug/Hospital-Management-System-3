using Hospital.Models;
using System.Collections.Generic;

namespace Hospital.ViewModels
{
    public class ApplicationUserViewModel
    {

        public string Id { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public Gender Gender { get; set; }

        public string Specilist { get; set; }

        public bool IsDoctor { get; set; }

        public int AppointmentsCount { get; set; }

        public int PatientsCount { get; set; }



        /*
         * Multiple insurance support
         */

        public List<int> SelectedInsuranceIds
        {
            get;
            set;
        }
        =
        new List<int>();



        public List<Insurance> AvailableInsurances
        {
            get;
            set;
        }
        =
        new List<Insurance>();




        public List<Appointment> LatestAppointments
        {
            get;
            set;
        }
        =
        new List<Appointment>();




        public List<ApplicationUser> Doctors
        {
            get;
            set;
        }
        =
        new List<ApplicationUser>();




        public ApplicationUserViewModel()
        {

        }




        public ApplicationUserViewModel(ApplicationUser user)
        {

            Id = user.Id;

            Name = user.Name;

            City = user.City;

            Address = user.Address;

            Gender = user.Gender;

            Specilist = user.Specilist;

            UserName = user.UserName;

            Email = user.Email;

            IsDoctor = user.IsDoctor;

        }





        public ApplicationUser ConvertViewModelToModel(
            ApplicationUserViewModel user)
        {

            return new ApplicationUser
            {

                Id = user.Id,

                Name = user.Name,

                City = user.City,

                Address = user.Address,

                Gender = user.Gender,

                Specilist = user.Specilist,

                Email = user.Email,

                UserName = user.UserName,

                IsDoctor = user.IsDoctor

            };

        }


    }
}