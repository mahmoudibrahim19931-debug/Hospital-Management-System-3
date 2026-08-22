using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hospital.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TimingController : Controller
    {
        private readonly IDoctorService _doctorService;

        public TimingController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_doctorService.GetAll(pageNumber, pageSize));
        }

        public IActionResult Details(int id)
        {
            var timing = _doctorService.GetTimingById(id);

            if (timing == null)
            {
                return NotFound();
            }

            return View(timing);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(Hospital.ViewModels.TimingViewModel timing)
        {
            try
            {
                _doctorService.AddTiming(timing);

                return Content("Saved Successfully");
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var timing = _doctorService.GetTimingById(id);

            if (timing == null)
            {
                return NotFound();
            }

            return View(timing);
        }

        [HttpPost]
        public IActionResult Edit(Hospital.ViewModels.TimingViewModel timing)
        {
            if (ModelState.IsValid)
            {
                _doctorService.UpdateTiming(timing);
                return RedirectToAction(nameof(Index));
            }

            return View(timing);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var timing = _doctorService.GetTimingById(id);

            if (timing == null)
            {
                return NotFound();
            }

            return View(timing);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _doctorService.DeleteTiming(id);

            return RedirectToAction(nameof(Index));
        }
    }
}