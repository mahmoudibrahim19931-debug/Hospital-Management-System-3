using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Hospital.Models.Appointment;

namespace hospital.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IApplicationUserService _userService;
        private readonly INotificationService _notificationService;

        public AppointmentsController(
            IAppointmentService appointmentService,
            IApplicationUserService userService,
            INotificationService notificationService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
            _notificationService = notificationService;
        }

        public IActionResult Index(
    string search,
    string status)
        {

            var appointments =
                _appointmentService
                .GetAllAppointments();




            if (!string.IsNullOrEmpty(search))
            {

                appointments = appointments
                    .Where(x =>

                        x.PatientName.Contains(search)

                        ||

                        x.DoctorName.Contains(search)

                    )
                    .ToList();

            }




            if (!string.IsNullOrEmpty(status))
            {

                appointments = appointments
                    .Where(x =>

                        x.Status.ToString() == status

                    )
                    .ToList();

            }




            return View(appointments);

        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new AppointmentViewModel();

            vm.Doctors = _userService
                .GetDoctors()
                .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id
                })
                .ToList();

            vm.Patients = _userService
                .GetPatients()
                .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id
                })
                .ToList();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(AppointmentViewModel model)
        {
            model.CreatedDate = DateTime.Now;
            model.Status = AppointmentStatus.Pending;

            _appointmentService.CreateAppointment(model);

            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vm = _appointmentService.GetById(id);

            vm.Doctors = _userService
                .GetDoctors()
                .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id
                })
                .ToList();

            vm.Patients = _userService
                .GetPatients()
                .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id
                })
                .ToList();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(AppointmentViewModel model)
        {
            _appointmentService.UpdateAppointment(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var appointment = _appointmentService.GetById(id);

            return View(appointment);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _appointmentService.DeleteAppointment(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Approve(int id)
        {

            var appointment =
                _appointmentService.GetById(id);



            appointment.Status =
                AppointmentStatus.Approved;



            _appointmentService
                .UpdateAppointment(appointment);




            _notificationService.Create(

    appointment.PatientId,

    "Doctor approved your appointment",

    "Patient",

    "Home",

    "Index",

    "/Patient/Home"

);



            return RedirectToAction(nameof(Index));

        }

        public IActionResult Complete(int id)
        {
            var appointment = _appointmentService.GetById(id);

            appointment.Status = AppointmentStatus.Completed;

            _appointmentService.UpdateAppointment(appointment);


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Cancel(int id)
        {
            var appointment = _appointmentService.GetById(id);

            appointment.Status = AppointmentStatus.Cancelled;

            _appointmentService.UpdateAppointment(appointment);

            _notificationService.Create(

    appointment.PatientId,

    "Appointment has been cancelled",

    "Patient",

    "Home",

    "Index",

    "/Patient/Home"

);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Calendar()
        {

            var events =

                _appointmentService
                .GetCalendarEvents();



            return View(events);

        }

    }
}