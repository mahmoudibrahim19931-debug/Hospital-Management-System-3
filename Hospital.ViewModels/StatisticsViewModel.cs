using Hospital.Models;
using System.Collections.Generic;

namespace Hospital.ViewModels
{
    public class StatisticsViewModel
    {
        public int DoctorsCount { get; set; }

        public int PatientsCount { get; set; }

        public int RoomsCount { get; set; }

        public int AppointmentsCount { get; set; }


        public int BillsCount { get; set; }

        public int PaidBills { get; set; }

        public int UnpaidBills { get; set; }

        public decimal Revenue { get; set; }



        public int LabsCount { get; set; }

        public int PendingLabs { get; set; }



        public List<ApplicationUser> LatestDoctors { get; set; }

        public List<DepartmentStatisticsViewModel> DepartmentStatistics { get; set; }

        public List<MedicineViewModel> LowStockMedicines { get; set; }
    = new();

    }
}