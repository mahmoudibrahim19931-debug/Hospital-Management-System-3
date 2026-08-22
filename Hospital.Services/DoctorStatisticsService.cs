using Hospital.Models;
using Hospital.Repositories;
using Hospital.ViewModels;
using System.Linq;

namespace Hospital.Services
{
    public class DoctorStatisticsService
        : IDoctorStatisticsService
    {

        private readonly ApplicationDbContext _context;


        public DoctorStatisticsService(
            ApplicationDbContext context)
        {
            _context = context;
        }



        public DoctorStatisticsViewModel GetStatistics(
            string doctorId)
        {


            var appointments =

                _context.Appointments

                .Where(

                    x =>

                    x.DoctorId == doctorId

                )
                .ToList();




            var vm =
                new DoctorStatisticsViewModel();




            vm.TotalPatients =

                appointments

                .Select(

                    x => x.PatientId

                )

                .Distinct()

                .Count();





            vm.TotalAppointments =

                appointments.Count();





            vm.CompletedVisits =

                appointments.Count(

                    x =>


                    x.Status ==
                    Appointment.AppointmentStatus.Done


                    ||

                    x.Status ==
                    Appointment.AppointmentStatus.Completed


                );





            vm.PendingAppointments =

                appointments.Count(

                    x =>

                    x.Status ==
                    Appointment.AppointmentStatus.Pending

                );






            vm.PendingLabs =

                _context.Labs

                .Count(

                    x =>


                    x.DoctorId == doctorId


                    &&


                    !x.IsDone


                );






            vm.Revenue =

                _context.Bills


                .Where(

                    x =>


                    x.DoctorId == doctorId


                    &&


                    x.IsPaid


                )


                .Sum(

                    x => (decimal?)x.Amount

                )

                ?? 0;


            vm.PatientsThisMonth =

            appointments

            .Where(

            x =>


            x.CreatedDate.Month
            ==
            DateTime.Now.Month


            &&


            x.CreatedDate.Year
            ==
            DateTime.Now.Year

            )

            .Select(

            x => x.PatientId

            )

            .Distinct()

            .Count();






            var startOfWeek =

            DateTime.Today.AddDays(

            -(int)DateTime.Today.DayOfWeek

            );





            vm.PatientsThisWeek =

            appointments

            .Where(

            x =>


            x.CreatedDate >= startOfWeek


            )

            .Select(

            x => x.PatientId

            )

            .Distinct()

            .Count();








            vm.NewPatientsThisMonth =


            appointments


            .Where(

            x =>


            x.CreatedDate.Month
            ==
            DateTime.Now.Month


            &&


            x.CreatedDate.Year
            ==
            DateTime.Now.Year

            )

            .Select(

            x => x.PatientId

            )

            .Distinct()

            .Count();








            vm.AveragePatientsPerDay =



            appointments.Any()



            ?


            Math.Round(



            (double)

            appointments.Count()


            /


            30



            , 2)



            :


            0;









            vm.CompletionRate =



            appointments.Any()


            ?


            Math.Round(



            (double)


            appointments.Count(

            x =>


            x.Status
            ==
            Appointment.AppointmentStatus.Done


            ||

            x.Status
            ==
            Appointment.AppointmentStatus.Completed


            )



            * 100



            /


            appointments.Count()



            , 2)



            :



            0;









            vm.MostCommonVisitType =



            appointments


            .GroupBy(

            x => x.Type

            )


            .OrderByDescending(

            x => x.Count()

            )


            .Select(

            x => x.Key

            )


            .FirstOrDefault()

            ??


            "N/A";


            return vm;

        }



    }
}