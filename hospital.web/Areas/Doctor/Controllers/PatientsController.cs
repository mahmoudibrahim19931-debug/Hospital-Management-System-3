
using Hospital.Models;
using Hospital.Repositories;
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace hospital.web.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    [Authorize(Roles = "Doctor")]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppointmentService _appointmentService;
        private readonly IMedicineService _medicineService;
        private readonly IPatientReportService _patientReportService;
        private readonly ILabService _labService;
        private readonly IBillService _billService;
        private readonly IDoctorNoteService _noteService;
        private readonly IMedicalFileService _medicalFileService;

        public PatientsController(

ApplicationDbContext context,

IAppointmentService appointmentService,

IPatientReportService patientReportService,

IMedicineService medicineService,

ILabService labService,

IBillService billService,

IDoctorNoteService noteService,
IMedicalFileService medicalFileService)
        {
            _context = context;
            _appointmentService = appointmentService;
            _patientReportService = patientReportService;
            _medicineService = medicineService;
            _labService = labService;
            _billService = billService;
            _noteService = noteService;
            _medicalFileService = medicalFileService;
        }

        public IActionResult Index(string search)
        {

            var doctorId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);



            var patients =

                _appointmentService
                .GetDoctorAppointments(doctorId)
                .Select(x => new PatientListViewModel
                {

                    Id = x.PatientId,

                    Name = x.PatientName

                })
                .DistinctBy(x => x.Id)
                .ToList();




            if (!string.IsNullOrEmpty(search))
            {

                patients = patients
                    .Where(x =>

                        x.Name != null

                        &&

                        x.Name.Contains(
                            search,
                            StringComparison.OrdinalIgnoreCase)

                    )
                    .ToList();

            }




            return View(patients);

        }


        public IActionResult Details(string id)
        {
            var patient = _context.ApplicationUsers
                .FirstOrDefault(x => x.Id == id);

            if (patient == null)
                return NotFound();

            var vm = new PatientDetailsViewModel
            {
                Patient =
        new ApplicationUserViewModel(patient),

                Appointments =
        _appointmentService.GetPatientAppointments(id),

                Reports =
        _patientReportService.GetPatientReports(id),

                Labs =
        _labService.GetPatientLabs(id),

                Notes =
        _noteService.GetPatientNotes(id),

                Bills =
        _billService.GetPatientBills(id),

                Files =
        _medicalFileService.GetPatientFiles(id),
               

            };




            foreach (var item in vm.Appointments)
            {

                vm.Timeline.Add(

                    new TimelineItemViewModel
                    {

                        Date = item.CreatedDate,

                        Title = "Appointment",

                        Description = item.Status.ToString()

                    });

            }




            foreach (var item in vm.Reports)
            {

                vm.Timeline.Add(

                    new TimelineItemViewModel
                    {

                        Date = item.CreatedDate,

                        Title = "Medical Report",

                        Description = item.Diagnose

                    });

            }




            foreach (var item in vm.Labs)
            {

                vm.Timeline.Add(

                    new TimelineItemViewModel
                    {

                        Date = item.CreatedDate,

                        Title = "Lab",

                        Description = item.TestType

                    });

            }




            foreach (var item in vm.Bills)
            {

                vm.Timeline.Add(

                    new TimelineItemViewModel
                    {

                        Date = item.CreatedDate,

                        Title = "Bill",

                        Description = $"{item.Amount} EGP"

                    });

            }




            foreach (var item in vm.Notes)
            {

                vm.Timeline.Add(

                    new TimelineItemViewModel
                    {

                        Date = item.CreatedDate,

                        Title = "Doctor Note",

                        Description = item.Note

                    });

            }

            foreach (var item in vm.Files)
            {

                vm.Timeline.Add(

                    new TimelineItemViewModel
                    {

                        Date = item.UploadDate,

                        Title = "Medical File",

                        Description = item.FileName

                    });

            }




            vm.Timeline =

                vm.Timeline
                .OrderByDescending(

                    x => x.Date)

                .ToList();

            vm.Timeline = BuildTimeline(vm);

            return View(vm);
        }



        [HttpGet]
        public IActionResult CreateReport(string patientId)
        {

            var vm = new PatientReportViewModel
            {

                PatientId = patientId,



                AvailableMedicines =

                    _medicineService
                    .GetAll()
                    .Where(x => x.Quantity > 0)
                    .ToList()


            };


            return View(vm);

        }



        [HttpPost]
        public IActionResult CreateReport(
            PatientReportViewModel model)
        {
            var doctorId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            _patientReportService.CreateReport(

        doctorId,

        model.PatientId,

        model.Diagnose,

        model.Prescription,

        model.SelectedMedicineIds

);

            return RedirectToAction(
                nameof(Details),
                new { id = model.PatientId });
        }



        [HttpGet]
        public IActionResult ReportDetails(int id)
        {
            var report =
                _patientReportService.GetById(id);

            if (report == null)
                return NotFound();

            return View(report);
        }



        [HttpGet]
        public IActionResult RequestLab(string patientId)
        {
            var vm = new LabViewModel
            {
                PatientId = patientId
            };

            return View(vm);
        }



        [HttpPost]
        public IActionResult RequestLab(
            LabViewModel model)
        {
            model.DoctorId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            _labService.Create(model);

            return RedirectToAction(
                nameof(Details),
                new
                {
                    id = model.PatientId
                });
        }



        public IActionResult ApproveAppointment(int id)
        {
            var appointment =
                _appointmentService.GetById(id);

            appointment.Status =
                Appointment.AppointmentStatus.Approved;

            _appointmentService
                .UpdateAppointment(appointment);

            return RedirectToAction(
                nameof(Details),
                new
                {
                    id = appointment.PatientId
                });
        }



        public IActionResult RejectAppointment(int id)
        {
            var appointment =
                _appointmentService.GetById(id);

            appointment.Status =
                Appointment.AppointmentStatus.Cancelled;

            _appointmentService
                .UpdateAppointment(appointment);

            return RedirectToAction(
                nameof(Details),
                new
                {
                    id = appointment.PatientId
                });
        }



        public IActionResult CompleteAppointment(int id)
        {
            var appointment =
                _appointmentService.GetById(id);

            appointment.Status =
                Appointment.AppointmentStatus.Completed;

            _appointmentService
                .UpdateAppointment(appointment);

            return RedirectToAction(
                nameof(Details),
                new
                {
                    id = appointment.PatientId
                });
        }



        [HttpGet]
        public IActionResult ScheduleAppointment(int id)
        {
            var appointment =
                _appointmentService.GetById(id);

            if (appointment == null)
                return NotFound();

            return View(appointment);
        }



        [HttpPost]
        public IActionResult ScheduleAppointment(
            AppointmentViewModel model)
        {
            var appointment =
                _appointmentService.GetById(model.Id);

            appointment.AppointmentDate =
                model.AppointmentDate;

            appointment.Status =
                Appointment.AppointmentStatus.Approved;

            _appointmentService
                .UpdateAppointment(appointment);

            return RedirectToAction(
                nameof(Details),
                new
                {
                    id = appointment.PatientId
                });
        }

        
        [HttpGet]
        public IActionResult CreateBill(int appointmentId)
        {

            var appointment =
                _appointmentService.GetById(appointmentId);



            if (appointment == null)
                return NotFound();




            if (appointment.Status !=
    Appointment.AppointmentStatus.Done)
            {

                TempData["Error"] =

                    "Patient has not confirmed appointment yet";


                return RedirectToAction(

                    nameof(Details),

                    new
                    {
                        id = appointment.PatientId
                    });

            }





            var vm = new BillViewModel
            {

                AppointmentId = appointment.Id,


                PatientId = appointment.PatientId,


                DoctorId = appointment.DoctorId,


                Amount = 500

            };



            return View(vm);

        }



        [HttpPost]
        public IActionResult CreateBill(BillViewModel model)
        {

            var appointment =


                _appointmentService
                .GetById(model.AppointmentId);




            if (appointment == null)
                return NotFound();





            if (appointment.Status !=
    Appointment.AppointmentStatus.Done)
            {


                TempData["Error"] =
"Visit is not finished yet";


                return RedirectToAction(

                    nameof(Details),

                    new
                    {
                        id = appointment.PatientId
                    });


            }





            _billService.Create(model);




            return RedirectToAction(

                nameof(Details),

                new
                {
                    id = model.PatientId
                });

        }
        public IActionResult StartVisit(int id)
        {

            var appointment =
                _appointmentService.GetById(id);


            appointment.Status =
                Appointment.AppointmentStatus.InProgress;


            _appointmentService
                .UpdateAppointment(appointment);



            return RedirectToAction(

                nameof(Details),

                new
                {
                    id = appointment.PatientId
                });

        }

        public IActionResult FinishVisit(int id)
        {

            var appointment =
                _appointmentService.GetById(id);


            appointment.Status =
                Appointment.AppointmentStatus.Done;



            _appointmentService
                .UpdateAppointment(appointment);



            return RedirectToAction(

                nameof(Details),

                new
                {
                    id = appointment.PatientId
                });

        }

        [HttpGet]
        public IActionResult AppointmentRequest(int id)
        {
            var appointment =
                _appointmentService.GetById(id);

            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        [HttpPost]
        public IActionResult AddNote(
string patientId,
string note)
        {

            var doctorId =
            User.FindFirstValue(
            ClaimTypes.NameIdentifier);



            _noteService.Create(

            patientId,

            doctorId,

            note);



            return RedirectToAction(

            nameof(Details),

            new { id = patientId });

        }

        [HttpPost]
        public IActionResult UploadFile(
    MedicalFileViewModel model)
        {

            var doctorId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);



            model.DoctorId = doctorId;



            _medicalFileService.Upload(model);




            return RedirectToAction(

                nameof(Details),

                new
                {
                    id = model.PatientId
                });

        }

        private List<TimelineItemViewModel> BuildTimeline(
        PatientDetailsViewModel vm)
        {

            var timeline =
                    new List<TimelineItemViewModel>();



            foreach (var item in vm.Appointments)
            {

                timeline.Add(

                    new TimelineItemViewModel
                    {

                        Date = item.CreatedDate,

                        Title = "Appointment",

                        Description =
                            item.Status.ToString()

                    });

            }




            foreach (var item in vm.Reports)
            {

                timeline.Add(

                    new TimelineItemViewModel
                    {

                        Date = DateTime.Now,

                        Title = "Medical Report",

                        Description = item.Diagnose

                    });

            }




            foreach (var item in vm.Labs)
            {

                timeline.Add(

                    new TimelineItemViewModel
                    {

                        Date = item.ResultDate ?? DateTime.Now,

                        Title = "Lab",

                        Description = item.TestType

                    });

            }




            foreach (var item in vm.Bills)
            {

                timeline.Add(

                    new TimelineItemViewModel
                    {

                        Date = item.CreatedDate,

                        Title = "Bill",

                        Description =
                            item.IsPaid
                            ? "Paid"
                            : "Pending"

                    });

            }





            foreach (var item in vm.Notes)
            {

                timeline.Add(

                    new TimelineItemViewModel
                    {

                        Date = item.CreatedDate,

                        Title = "Doctor Note",

                        Description = item.Note

                    });

            }






            return timeline
                    .OrderByDescending(

                        x => x.Date)

                    .ToList();

        }
    }
}

