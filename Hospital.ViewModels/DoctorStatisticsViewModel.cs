using System;

namespace Hospital.ViewModels
{
    public class DoctorStatisticsViewModel
    {

        public int TotalPatients { get; set; }


        public int TotalAppointments { get; set; }


        public int CompletedVisits { get; set; }


        public int PendingAppointments { get; set; }


        public decimal Revenue { get; set; }


        public int PendingLabs { get; set; }





        public int PatientsThisMonth { get; set; }


        public int PatientsThisWeek { get; set; }


        public int NewPatientsThisMonth { get; set; }


        public double AveragePatientsPerDay { get; set; }


        public double CompletionRate { get; set; }


        public string MostCommonVisitType { get; set; }


    }
}