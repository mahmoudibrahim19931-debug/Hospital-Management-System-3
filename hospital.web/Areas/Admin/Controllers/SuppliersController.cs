using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hospital.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SuppliersController : Controller
    {

        private readonly ISupplierService _service;



        public SuppliersController(
            ISupplierService service)
        {
            _service = service;
        }




        public IActionResult Index()
        {

            var suppliers =
                _service.GetAll();



            return View(suppliers);

        }




        [HttpGet]
        public IActionResult Create()
        {

            return View(
                new SupplierViewModel());

        }




        [HttpPost]
        public IActionResult Create(
            SupplierViewModel model)
        {

            _service.Create(model);


            return RedirectToAction(
                nameof(Index));

        }


    }
}