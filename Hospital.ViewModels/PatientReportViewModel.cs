using Hospital.Models;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.ViewModels
{
    public class PatientReportViewModel
    {
        public int Id { get; set; }

        public string Diagnose { get; set; }

        public string DoctorId { get; set; }

        public string PatientId { get; set; }

        public string Prescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<int> SelectedMedicineIds { get; set; }
    = new List<int>();

        public List<MedicineViewModel> AvailableMedicines { get; set; }
            = new List<MedicineViewModel>();

        public string DoctorName { get; set; }

        public string PatientName { get; set; }

       

        public List<string> Medicines { get; set; }
            = new List<string>();

        public PatientReportViewModel()
        {
        }

        public PatientReportViewModel(PatientReport report)
        {
            Id = report.Id;

            Diagnose = report.Diagnose;

            DoctorId = report.DoctorId;

            PatientId = report.PatientId;

            DoctorName = report.Doctor?.Name;

            PatientName = report.Patient?.Name;

            Prescription = report.Prescription;
            CreatedDate = report.CreatedDate;

            if (report.PrescribedMedicines != null)
            {
                Medicines = report.PrescribedMedicines
                    .Where(x => x.Medicine != null)
                    .Select(x => x.Medicine.Name)
                    .ToList();
            }
        }
    }
}