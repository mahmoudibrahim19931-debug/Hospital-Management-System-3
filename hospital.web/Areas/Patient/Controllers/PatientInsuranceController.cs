using Hospital.Models;
using Hospital.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace hospital.web.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize(Roles = "Patient")]
    public class PatientInsuranceController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;


        public PatientInsuranceController(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }



        public IActionResult Index()
        {

            var patientId =
                _userManager.GetUserId(User);



            ViewBag.AvailableInsurances =

                _unitOfWork
                .GenericRepository<Insurance>()
                .GetAll()
                .ToList();




            var selected =

                _unitOfWork
                .GenericRepository<PatientInsurance>()
                .GetAll(

                    x => x.PatientId == patientId,

                    includeProperties:
                    "Insurance"

                    )
                .ToList();




            return View(selected);

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

                    x => x.PatientId == patientId
                    &&
                    x.IsActive

                    )

                .FirstOrDefault();



            if (activeInsurance != null)
            {

                activeInsurance.IsActive = false;

                activeInsurance.EndDate =
                    DateTime.Now;


                _unitOfWork
                    .GenericRepository<PatientInsurance>()
                    .Update(activeInsurance);

            }





            var item = new PatientInsurance
            {

                PatientId = patientId,

                InsuranceId = insuranceId,



                StartDate =
                    DateTime.Now,


                CreatedDate =
                    DateTime.Now,



                IsApproved = false,


                IsActive = false

            };




            _unitOfWork
                .GenericRepository<PatientInsurance>()
                .Add(item);



            _unitOfWork.Save();



            TempData["Success"] =
                "Insurance request sent for approval";



            return RedirectToAction(nameof(Index));
        }


    }
}