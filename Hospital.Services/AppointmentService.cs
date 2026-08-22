using Azure.Core;
using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork, INotificationService notificationService  )
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public List<AppointmentViewModel> GetPatientAppointments(string patientId)
        {
            var appointments = _unitOfWork
                .GenericRepository<Appointment>()
                .GetAll(
                    x => x.PatientId == patientId,
                    includeProperties: "Doctor,Patient")
                .ToList();

            return appointments
                .Select(x => new AppointmentViewModel(x))
                .ToList();
        }

        public List<AppointmentViewModel> GetDoctorAppointments(string doctorId)
        {
            var appointments = _unitOfWork
                .GenericRepository<Appointment>()
                .GetAll(
                    x => x.DoctorId == doctorId,
                    includeProperties: "Doctor,Patient")
                .ToList();

            return appointments
                .Select(x => new AppointmentViewModel(x))
                .ToList();
        }

        public List<AppointmentViewModel> GetAllAppointments()
        {
            var appointments = _unitOfWork
                .GenericRepository<Appointment>()
                .GetAll(
                    includeProperties: "Doctor,Patient")
                .ToList();

            return appointments
                .Select(x => new AppointmentViewModel(x))
                .ToList();
        }

        public void CreateAppointment(AppointmentViewModel model)
        {
            var appointment = model.ConvertViewModel(model);

            _unitOfWork
                .GenericRepository<Appointment>()
                .Add(appointment);

            _unitOfWork.Save(); 
            _notificationService.Create(

appointment.PatientId,

"Appointment Request Sent",

"Patient",

"Bills",

"Index",

"/Patient/Bills"

);
        }

        public AppointmentViewModel GetById(int id)
        {
            var appointment = _unitOfWork
                .GenericRepository<Appointment>()
                .GetAll(
                    x => x.Id == id,
                    includeProperties: "Doctor,Patient")
                .FirstOrDefault();

            if (appointment == null)
                return null;

            return new AppointmentViewModel(appointment);
        }

        public void UpdateAppointment(AppointmentViewModel model)
        {
            var appointment = _unitOfWork
                .GenericRepository<Appointment>()
                .GetById(model.Id);

            appointment.Name = model.Name;
            appointment.Type = model.Type;
            appointment.Description = model.Description;
            appointment.DoctorId = model.DoctorId;
            appointment.PatientId = model.PatientId;
            appointment.Status = model.Status;
            appointment.AppointmentDate = model.AppointmentDate;

            _unitOfWork
                .GenericRepository<Appointment>()
                .Update(appointment);

            _unitOfWork.Save();
            if (model.Status ==
 Appointment.AppointmentStatus.Approved)
            {


                appointment.Status =
                    Appointment.AppointmentStatus.PatientPending;



                _unitOfWork
                    .GenericRepository<Appointment>()
                    .Update(appointment);



                _unitOfWork.Save();




                _notificationService.Create(

                    appointment.PatientId,

                    "Doctor approved appointment. Waiting for your confirmation.",

                    "Patient",

                    "Home",

                    "Index",

                    "/Patient"

                );



            }
        }

        public void DeleteAppointment(int id)
        {
            var appointment = _unitOfWork
                .GenericRepository<Appointment>()
                .GetById(id);

            if (appointment == null)
                return;

            _unitOfWork
                .GenericRepository<Appointment>()
                .Delete(appointment);

            _unitOfWork.Save();
        }

        public List<CalendarEventViewModel>GetCalendarEvents()
        {

            return _unitOfWork
                .GenericRepository<Appointment>().GetAll(
        x => x.AppointmentDate.HasValue,
        includeProperties: "Patient")
                .Select(x =>

                    new CalendarEventViewModel
                    {

                        Id = x.Id,



                        Title =

        x.Patient.Name,




                        Start =

        x.AppointmentDate
        .Value
        .ToString("yyyy-MM-dd"),




                        Color =


        x.Status ==
        Appointment.AppointmentStatus.Approved


        ?


        "green"



        :



        x.Status ==
        Appointment.AppointmentStatus.Pending



        ?



        "orange"



        :



        "red",




                        Url =


"/Admin/Appointments/Details/"


+


x.Id



                    }

                )
                .ToList();

        }

        public List<CalendarEventViewModel>GetDoctorCalendarEvents(
    string doctorId)
        {


            return _unitOfWork
                .GenericRepository<Appointment>()
                .GetAll(

                    includeProperties:
                    "Patient"

                )

                .Where(x =>


                    x.DoctorId == doctorId


                    &&


                    x.AppointmentDate.HasValue


                )



                .Select(x =>


                    new CalendarEventViewModel
                    {


                        Id = x.Id,



                        Title =

                            x.Patient.Name,




                        Start =

                            x.AppointmentDate
                            .Value
                            .ToString("yyyy-MM-dd"),




                        Color =



                            x.Status ==
                            Appointment.AppointmentStatus.Approved



                            ?



                            "green"




                            :



                            x.Status ==
                            Appointment.AppointmentStatus.Pending




                            ?



                            "orange"




                            :



                            "red",





                        Url =



                            "/Doctor/Patients/Details/"


                            +



                            x.PatientId


                    }



                )

                .ToList();


        }

        public AppointmentViewModel GetLastAppointment(string patientId)
        {
            var appointment = _unitOfWork
                .GenericRepository<Appointment>()
                .GetAll(x => x.PatientId == patientId)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();


            if (appointment == null)
                return null;


            return new AppointmentViewModel
            {
                Id = appointment.Id,

                PatientId = appointment.PatientId,

                DoctorId = appointment.DoctorId,

                AppointmentDate = appointment.AppointmentDate,

                Description = appointment.Description,

                Type = appointment.Type,

                Status = appointment.Status
            };
        }
    }
}