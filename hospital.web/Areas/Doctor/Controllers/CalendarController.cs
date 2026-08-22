using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace hospital.web.Areas.Doctor.Controllers
{


    [Area("Doctor")]
    [Authorize(Roles = "Doctor")]


    public class CalendarController
        : Controller
    {


        private readonly
            IAppointmentService
            _appointmentService;



        public CalendarController(

            IAppointmentService
            appointmentService)

        {

            _appointmentService =
                appointmentService;

        }





        public IActionResult Index()
        {
            var doctorId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            var events =
                _appointmentService
                .GetDoctorCalendarEvents(doctorId);

            return View(events);
        }


    }


}