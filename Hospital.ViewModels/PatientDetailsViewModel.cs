using System.Collections.Generic;

namespace Hospital.ViewModels
{
    public class PatientDetailsViewModel
    {
        public ApplicationUserViewModel Patient { get; set; }

        public List<AppointmentViewModel> Appointments { get; set; }
            = new List<AppointmentViewModel>();

        public List<PatientReportViewModel> Reports { get; set; }
            = new List<PatientReportViewModel>();

        public List<MedicineViewModel> AvailableMedicines { get; set; }

        public List<LabViewModel> Labs { get; set; }
            = new();

        public List<BillViewModel> Bills { get; set; }
            = new();

        public List<DoctorNoteViewModel> Notes { get; set; }
    = new();

        public List<TimelineItemViewModel> Timeline { get; set; }
        = new();

        public List<MedicalFileViewModel> Files { get; set; }

    = new();

    }
}