using Hospital.ViewModels;
using System.Collections.Generic;

namespace Hospital.Services
{
    public interface IAppointmentService
    {
        List<AppointmentViewModel> GetPatientAppointments(string patientId);

        List<AppointmentViewModel> GetDoctorAppointments(string doctorId);

        List<AppointmentViewModel> GetAllAppointments();

        List<CalendarEventViewModel>GetCalendarEvents();
        List<CalendarEventViewModel>GetDoctorCalendarEvents(string doctorId);
        AppointmentViewModel GetById(int id);
        AppointmentViewModel GetLastAppointment(string patientId);
        void CreateAppointment(AppointmentViewModel model);

        void UpdateAppointment(AppointmentViewModel model);

        void DeleteAppointment(int id);
    }
}