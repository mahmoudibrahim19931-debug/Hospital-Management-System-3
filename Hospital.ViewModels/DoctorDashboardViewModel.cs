using System.Collections.Generic;

namespace Hospital.ViewModels
{
    public class DoctorDashboardViewModel
    {
        public int TotalAppointments { get; set; }

        public int TodayAppointments { get; set; }

        public int TotalPatients { get; set; }


        public int PendingAppointments { get; set; }

        public int CompletedVisits { get; set; }

        public int PendingLabs { get; set; }

        public decimal Revenue { get; set; }



        public List<AppointmentViewModel> UpcomingAppointments { get; set; }
            = new();
    }
}