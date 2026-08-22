using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hospital.web.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize(Roles = "Patient")]
    public class HomeController : Controller
    {
        private readonly IPatientDashboardService _dashboardService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppointmentService _appointmentService;
        private readonly IApplicationUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly IBillService _billService;
        private readonly IUnitOfWork _unitOfWork;



        public HomeController(
     IPatientDashboardService dashboardService,
     UserManager<ApplicationUser> userManager,
     IAppointmentService appointmentService,
     IApplicationUserService userService,
     INotificationService notificationService,
     IBillService billService,
     IUnitOfWork unitOfWork)
        {
            _dashboardService = dashboardService;
            _userManager = userManager;
            _appointmentService = appointmentService;
            _userService = userService;
            _notificationService = notificationService;
            _billService = billService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var patientId =
                _userManager.GetUserId(User);



            var model =
                _dashboardService
                .GetDashboard(patientId);




            model.AvailableInsurances =

                _unitOfWork
                .GenericRepository<Insurance>()
                .GetAll(

                    x => x.IsActive

                    )

                .Select(x =>

                    new SelectListItem
                    {

                        Text =
                            $"{x.Name} ({x.DiscountPercentage}%)",

                        Value =
                            x.Id.ToString()

                    })

                .ToList();




            return View(model);
        }

        [HttpPost]
        public IActionResult SelectInsurance(int insuranceId)
        {

            var patientId =
                _userManager.GetUserId(User);



            var activeInsurance =

                _unitOfWork
                .GenericRepository<PatientInsurance>()
                .GetAll(

                    x =>

                        x.PatientId == patientId

                        &&

                        x.IsActive,

                    includeProperties:
                    "Insurance"

                )

                .FirstOrDefault();





            if (activeInsurance != null)
            {

                if (activeInsurance.InsuranceId == insuranceId)
                {

                    TempData["InsuranceMessage"] =

                        $"You already have {activeInsurance.Insurance.Name} as your active insurance.";



                    return RedirectToAction(nameof(Index));

                }

            }






            var pendingInsurance =

                _unitOfWork
                .GenericRepository<PatientInsurance>()

                .GetAll(

                    x =>

                        x.PatientId == patientId

                        &&

                        !x.IsApproved,

                    includeProperties:
                    "Insurance"

                )

                .FirstOrDefault();





            if (pendingInsurance != null)
            {

                if (pendingInsurance.InsuranceId == insuranceId)
                {

                    TempData["InsuranceMessage"] =

                        $"Insurance request already pending : {pendingInsurance.Insurance.Name}";



                    return RedirectToAction(nameof(Index));

                }



                TempData["InsuranceMessage"] =

                    $"You already have a pending insurance request : {pendingInsurance.Insurance.Name}";



                return RedirectToAction(nameof(Index));

            }






            var item =
                new PatientInsurance
                {

                    PatientId = patientId,


                    InsuranceId = insuranceId,


                    IsApproved = false,


                    IsActive = false,


                    CreatedDate = DateTime.Now,


                    StartDate = DateTime.Now

                };





            _unitOfWork
                .GenericRepository<PatientInsurance>()
                .Add(item);




            _unitOfWork.Save();





            TempData["InsuranceMessage"] =


                activeInsurance == null


                ?


                "Insurance request submitted successfully."


                :


                "Insurance change request submitted successfully.";




            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public IActionResult RequestAppointment()
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

            return View(vm);
        }

        [HttpPost]
        public IActionResult RequestAppointment(AppointmentViewModel model)
        {
            model.PatientId = _userManager.GetUserId(User);

            model.CreatedDate = DateTime.Now;

            model.Status =
                Appointment.AppointmentStatus.Pending;

            _appointmentService.CreateAppointment(model);
            var appointment =
    _appointmentService.GetLastAppointment(model.PatientId);
            var doctorId = model.DoctorId;


            _notificationService.Create(

        doctorId,

        $"{User.Identity.Name} requested an appointment",

        "Doctor",

        "Patients",

        "AppointmentRequest",

        $"/Doctor/Patients/AppointmentRequest/{appointment.Id}"

);

            var patient =

    _userManager.GetUserAsync(User)
    .Result;



            _notificationService.Create(

    model.DoctorId,

    $"{patient.Name} requested an appointment",

    "Doctor",

    "Patients",

    "AppointmentRequest",

    $"/Doctor/Patients/AppointmentRequest/{appointment.Id}"

);

            return RedirectToAction(nameof(Index));
        }
       

        public IActionResult ConfirmAppointment(int id)
        {


            var patientId =
                _userManager.GetUserId(User);




            var appointment =


                _unitOfWork

                .GenericRepository<Appointment>()

                .GetById(id);




            if (appointment == null)
                return RedirectToAction(nameof(Index));




            if (appointment.PatientId != patientId)
                return RedirectToAction(nameof(Index));





            appointment.Status =

                Appointment
                .AppointmentStatus
                .PatientConfirmed;




            _unitOfWork

                .GenericRepository<Appointment>()

                .Update(appointment);




            _unitOfWork.Save();




            _notificationService.Create(

                appointment.DoctorId,

                "Patient confirmed appointment",


                "Doctor",

                "DoctorDashboard",

                "Index",

                "/Doctor/DoctorDashboard"

            );




            return RedirectToAction(nameof(Index));

        }

        public IActionResult RejectAppointment(int id)
        {


            var patientId =

                _userManager.GetUserId(User);





            var appointment =


                _unitOfWork

                .GenericRepository<Appointment>()

                .GetById(id);




            if (appointment == null)
                return RedirectToAction(nameof(Index));



            if (appointment.PatientId != patientId)
                return RedirectToAction(nameof(Index));




            appointment.Status =


                Appointment
                .AppointmentStatus
                .Cancelled;




            _unitOfWork

                .GenericRepository<Appointment>()

                .Update(appointment);



            _unitOfWork.Save();





            _notificationService.Create(

                appointment.DoctorId,

                "Patient rejected appointment",


                "Doctor",

                "DoctorDashboard",

                "Index",

                "/Doctor/DoctorDashboard"

            );




            return RedirectToAction(nameof(Index));

        }
    }
}
