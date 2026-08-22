using Hospital.Models;
using Hospital.Repositories;
using Hospital.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Hospital.Services
{
    public class DoctorDashboardService : IDoctorDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DoctorDashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public DoctorDashboardViewModel GetDashboard(string doctorId)
        {

            var appointments = _context.Appointments

                .Include(x => x.Patient)

                .Where(x => x.DoctorId == doctorId)

                .ToList();



            var vm = new DoctorDashboardViewModel();



            vm.TotalAppointments = appointments.Count();




            vm.TodayAppointments = appointments.Count(

                x =>

                x.AppointmentDate.HasValue

                &&

                x.AppointmentDate.Value.Date == DateTime.Today

            );





            vm.TotalPatients = appointments

                .Select(x => x.PatientId)

                .Distinct()

                .Count();





            vm.PendingAppointments = appointments

                .Count(

                    x =>

                    x.Status ==

                    Appointment.AppointmentStatus.Pending

                );





            vm.CompletedVisits = appointments

                .Count(

                    x =>

                    x.Status ==

                    Appointment.AppointmentStatus.Done

                );





            vm.PendingLabs = _context.Labs

                .Count(

                    x =>

                    x.DoctorId == doctorId

                    &&

                    !x.IsDone

                );






            vm.Revenue = _context.Bills

                .Where(

                    x =>

                    x.DoctorId == doctorId

                    &&

                    x.IsPaid

                )

                .Sum(

                    x => (decimal?)x.Amount

                ) ?? 0;







            vm.UpcomingAppointments = appointments

                .Where(

                    x =>

                    x.AppointmentDate.HasValue

                    &&

                    x.AppointmentDate > DateTime.Now

                    &&

                    x.Status ==

                    Appointment.AppointmentStatus.Approved

                )

                .OrderBy(

                    x => x.AppointmentDate

                )

                .Take(10)

                .Select(

                    x => new AppointmentViewModel(x)

                )

                .ToList();




            return vm;

        }

    }
}