using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hospital.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MedicinesController : Controller
    {
        private readonly IMedicineService _medicineService;
        private readonly ISupplierService _supplierService;

        public MedicinesController(
            IMedicineService medicineService,
            ISupplierService supplierService)
        {
            _medicineService = medicineService;
            _supplierService = supplierService;
        }


        public IActionResult Index(string search)
        {

            var medicines =
                _medicineService.GetAll();


            if (!string.IsNullOrEmpty(search))
            {

                medicines = medicines
                    .Where(x =>

                        x.Name.Contains(search)

                    )
                    .ToList();

            }


            return View(medicines);

        }



        [HttpGet]
        public IActionResult Create()
        {

            var vm = new MedicineViewModel();


            vm.Suppliers =
                _supplierService
                .GetAll()
                .Select(x => new SelectListItem
                {

                    Text = x.Company,

                    Value = x.Id.ToString()

                })
                .ToList();



            return View(vm);

        }




        [HttpPost]
        public IActionResult Create(MedicineViewModel model)
        {
            _medicineService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Restock(int id)
        {

            var medicine =
                _medicineService.GetById(id);


            return View(medicine);

        }



        [HttpPost]
        public IActionResult Restock(int id, int quantity)
        {

            _medicineService
                .Restock(id, quantity);


            return RedirectToAction(nameof(Index));

        }

        public IActionResult Details(int id)
        {

            var medicine =

                _medicineService
                .GetDetails(id);



            return View(medicine);


        }

    }
}