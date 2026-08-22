using Hospital.ViewModels;

namespace Hospital.Services
{
    public interface IPatientDashboardService
    {
        PatientDashboardViewModel GetDashboard(string patientId);
    }
}