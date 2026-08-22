using Hospital.ViewModels;
using System.Collections.Generic;

namespace Hospital.Services
{
    public interface IPatientReportService
    {
        List<PatientReportViewModel> GetPatientReports(string patientId);
        PatientReportViewModel GetById(int id);
        void CreateReport(
            string doctorId,
            string patientId,
            string diagnose,
            string prescription,
            List<int> medicineIds);
    }
}