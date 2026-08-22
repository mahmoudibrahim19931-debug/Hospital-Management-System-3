using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc;

namespace hospital.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InsuranceController : Controller
    {
        private readonly IInsuranceService _insuranceService;


        public InsuranceController(
            IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }




        public IActionResult Index()
        {
            var insurances =
                _insuranceService.GetAll();

            return View(insurances);
        }





        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public IActionResult Create(Insurance insurance)
        {

            if (!ModelState.IsValid)
                return View(insurance);


            _insuranceService.Create(insurance);


            return RedirectToAction(nameof(Index));
        }





        public IActionResult Edit(int id)
        {

            var insurance =
                _insuranceService.GetById(id);


            if (insurance == null)
                return NotFound();



            return View(insurance);

        }





        [HttpPost]
        public IActionResult Edit(Insurance insurance)
        {

            if (!ModelState.IsValid)
                return View(insurance);



            _insuranceService.Update(insurance);



            return RedirectToAction(nameof(Index));

        }







        public IActionResult Delete(int id)
        {

            _insuranceService.Delete(id);



            return RedirectToAction(nameof(Index));

        }


    }
}