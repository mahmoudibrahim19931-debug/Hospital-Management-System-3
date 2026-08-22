using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace hospital.web.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize(Roles = "Patient")]
    public class BillsController : Controller
    {
        private readonly IBillService _billService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;


        public BillsController(

            IBillService billService,

            UserManager<ApplicationUser> userManager,

            IUnitOfWork unitOfWork
            )
        {
            _billService = billService;

            _userManager = userManager;

            _unitOfWork = unitOfWork;
        }



        public IActionResult Index()
        {

            var patientId =
                _userManager.GetUserId(User);



            var bills =
                _billService.GetPatientBills(patientId);



            return View(bills);

        }



        public IActionResult Pay(int id)
        {

            var patientId =
                _userManager.GetUserId(User);



            var bill =
                _billService.GetById(id);



            ViewBag.Insurances =

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
                .ToList();



            return View(bill);

        }




        [HttpPost]
        public IActionResult Pay(
            int billId,
            int insuranceId)
        {

            _billService.Pay(

                billId,

                insuranceId

                );



            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public IActionResult Preview(
    int billId,
    int insuranceId)
        {

            var vm = _billService
                .CalculatePayment(
                    billId,
                    insuranceId);



            var patientId =
                _userManager.GetUserId(User);



            ViewBag.Insurances =

                _unitOfWork
                .GenericRepository<PatientInsurance>()
                .GetAll(

                    x => x.PatientId == patientId
                    &&
                    x.IsActive,

                    includeProperties:
                    "Insurance"

                    )
                .ToList();



            return View(
                "Pay",
                vm);

        }


    }
}