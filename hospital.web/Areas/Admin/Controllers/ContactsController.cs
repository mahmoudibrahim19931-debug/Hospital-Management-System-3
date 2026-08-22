using Hospital.Models;
using Hospital.Services;
using Hospital.Utilities;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace hospital.web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class ContactsController : Controller
    {

        private IContactService _contact;
        private IHospitalInfo _HospitalInfo;

        public ContactsController(IContactService contact, IHospitalInfo HospitalInfo)
        {
            _contact = contact;
            _HospitalInfo = HospitalInfo;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_contact.GetAll(pageNumber, pageSize));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.hospital = new SelectList(
    _HospitalInfo.GetAll(1, 1000).Data,
    "Id",
    "Name"
);
            var viewModel = _contact.GetContactById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ContactViewModel vm)
        {
            _contact.UpdateContact(vm);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(ContactViewModel vm)
        {
            _contact.InsertContact(vm);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _contact.DeleteContact(id);

            return RedirectToAction("Index");
        }

    }
}
