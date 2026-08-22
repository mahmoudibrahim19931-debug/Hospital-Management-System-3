
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hospital.web.Areas.Lab.Controllers
{
    [Area("Lab")]
    [Authorize]
    public class LabController : Controller
    {
        private readonly ILabService _labService;

        public LabController(ILabService labService)
        {
            _labService = labService;
        }


        public IActionResult PendingTests(string search)
        {

            var labs =
                _labService.GetPendingLabs(search);



            return View(labs);

        }


        [HttpGet]
        public IActionResult EnterResult(int id)
        {
            var lab = _labService.GetById(id);

            if (lab == null)
                return NotFound();

            return View(lab);
        }



        [HttpPost]
        public IActionResult EnterResult(LabViewModel model)
        {
            _labService.Update(model);

            return RedirectToAction(nameof(PendingTests));
        }


    }
}

