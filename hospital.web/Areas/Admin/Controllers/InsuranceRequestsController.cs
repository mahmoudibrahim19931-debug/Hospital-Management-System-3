using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace hospital.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InsuranceRequestsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;

        public InsuranceRequestsController(
            IUnitOfWork unitOfWork,
            INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }




        public IActionResult Index()
        {

            var requests =

                _unitOfWork
                .GenericRepository<PatientInsurance>()
                .GetAll(

                    x => !x.IsApproved,

                    includeProperties:
                    "Patient,Insurance"

                    )

                .OrderByDescending(x => x.CreatedDate)

                .ToList();



            return View(requests);

        }







        public IActionResult Approve(int id)
        {

            var request =

                _unitOfWork
                .GenericRepository<PatientInsurance>()
                .GetAll(

                    x => x.Id == id,

                    includeProperties:
                    "Patient,Insurance"

                    )

                .FirstOrDefault();



            if (request == null)
                return RedirectToAction(nameof(Index));

            if (request.IsApproved)
            {
                return RedirectToAction(nameof(Index));
            }



            var oldInsurance =

                _unitOfWork
                .GenericRepository<PatientInsurance>()
                .GetAll(

                    x => x.PatientId ==
                    request.PatientId

                    &&

                    x.IsActive

                    )

                .FirstOrDefault();





            if (oldInsurance != null)
            {

                oldInsurance.IsActive = false;

                oldInsurance.EndDate = DateTime.Now;



                _unitOfWork
                    .GenericRepository<PatientInsurance>()
                    .Update(oldInsurance);

            }





            request.IsApproved = true;

            request.IsActive = true;

            request.StartDate = DateTime.Now;




            _unitOfWork
                .GenericRepository<PatientInsurance>()
                .Update(request);




            _unitOfWork.Save();





            _notificationService.Create(

     request.PatientId,

     $"Insurance approved : {request.Insurance.Name}",

     "Patient",

     "Home",

     "Index",

     "/Patient/Home"

 );





            return RedirectToAction(nameof(Index));

        }









        public IActionResult Reject(int id)
        {

            var request =

                _unitOfWork
                .GenericRepository<PatientInsurance>()
                .GetAll(

                    x => x.Id == id,

                    includeProperties:
                    "Insurance"

                    )

                .FirstOrDefault();




            if (request == null)
                return RedirectToAction(nameof(Index));





            _notificationService.Create(

    request.PatientId,

    $"Insurance rejected : {request.Insurance.Name}",

    "Patient",

    "Home",

    "Index",

    "/Patient/Home"

);





            _unitOfWork
                .GenericRepository<PatientInsurance>()
                .Delete(request);



            _unitOfWork.Save();




            return RedirectToAction(nameof(Index));

        }

    }
}