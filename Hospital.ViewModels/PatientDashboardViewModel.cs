using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.ViewModels
{
    public class PatientDashboardViewModel
    {
        public string PatientId { get; set; }

        public string PatientName { get; set; }

        public int TotalAppointments { get; set; }

        public int PendingAppointments { get; set; }

        public int ApprovedAppointments { get; set; }

        public int CompletedAppointments { get; set; }

        public int CancelledAppointments { get; set; }

        public string InsuranceName { get; set; }

        public decimal DiscountPercentage { get; set; }

        public bool HasPendingInsurance { get; set; }

        public string PendingInsuranceName { get; set; }

        public string ActiveInsuranceName { get; set; }



        public decimal ActiveDiscount { get; set; }
        public List<AppointmentViewModel> Appointments { get; set; }
            = new List<AppointmentViewModel>();

        public List<LabViewModel> Labs { get; set; }
    = new();

        public List<BillViewModel> Bills { get; set; }
    = new();

        public List<SelectListItem> AvailableInsurances { get; set; }
    = new();
    }
}