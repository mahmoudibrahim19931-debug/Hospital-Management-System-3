using Hospital.Repositories;
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hospital.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IApplicationUserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientReportService _patientReportService;

        public UsersController(
    IApplicationUserService userService,
    IAppointmentService appointmentService,
    ApplicationDbContext context,
    IPatientReportService patientReportService)
        {
            _userService = userService;
            _appointmentService = appointmentService;
            _context = context;
            _patientReportService = patientReportService;
        }

        public IActionResult Index(int PageNumber = 1, int PageSize = 10)
        {
            return View(_userService.GetAll(PageNumber, PageSize));
        }

        public IActionResult AllDoctors(int PageNumber = 1, int PageSize = 10)
        {
            return View(_userService.GetAllDoctor(PageNumber, PageSize));
        }

        public IActionResult DoctorAppointments(string id)
        {
            var appointments =
                _appointmentService.GetDoctorAppointments(id);

            return View(appointments);
        }

        [HttpGet]
        public IActionResult DoctorDetails(string id)
        {
            var doctor = _userService.GetById(id);

            if (doctor == null)
                return NotFound();

            doctor.AppointmentsCount = _context.Appointments
                .Count(x => x.DoctorId == id);

            doctor.PatientsCount = _context.Appointments
                .Where(x => x.DoctorId == id)
                .Select(x => x.PatientId)
                .Distinct()
                .Count();

            doctor.LatestAppointments = _context.Appointments
                .Include(x => x.Patient)
                .Where(x => x.DoctorId == id)
                .OrderByDescending(x => x.CreatedDate)
                .Take(10)
                .ToList();

            return View(doctor);
        }

        [HttpGet]
        public IActionResult PatientDetails(string id)
        {
            var patient = _userService.GetById(id);

            if (patient == null)
                return NotFound();

            patient.AppointmentsCount = _context.Appointments
                .Count(x => x.PatientId == id);

            var vm = new PatientDetailsViewModel
            {
                Patient = patient,

                Appointments = _appointmentService
                    .GetPatientAppointments(id),

                Reports = _patientReportService
                    .GetPatientReports(id)
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var user = _userService.GetById(id);

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(ApplicationUserViewModel vm)
        {
            _userService.UpdateDoctor(vm.Id, vm.IsDoctor);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult UpdateDoctor(string id)
        {
            var user = _userService.GetById(id);

            _userService.UpdateDoctor(id, !user.IsDoctor);

            return RedirectToAction(nameof(Index));
        }
    }
}