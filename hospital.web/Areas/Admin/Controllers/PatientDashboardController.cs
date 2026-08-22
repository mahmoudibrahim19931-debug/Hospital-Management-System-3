using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hospital.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PatientDashboardController : Controller
    {
        private readonly IPatientDashboardService _service;
        private readonly IBillService _billService;

        public PatientDashboardController(
            IPatientDashboardService service,
            IBillService billService)
        {
            _service = service;
            _billService = billService;
        }


        public IActionResult Index(string id)
        {
            var model = _service.GetDashboard(id);

            return View(model);
        }



      


    }
}