using Hospital.Models;
using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace hospital.web.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    [Authorize(Roles = "Doctor")]
    public class DoctorDashboardController : Controller
    {
        private readonly IDoctorDashboardService _dashboardService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDoctorStatisticsService _statisticsService;

        public DoctorDashboardController(
    IDoctorDashboardService dashboardService,
    UserManager<ApplicationUser> userManager,
    IDoctorStatisticsService statisticsService)
        {
            _dashboardService = dashboardService;
            _userManager = userManager;
            _statisticsService = statisticsService;
        }
        public IActionResult Index()
        {
            var doctorId =
_userManager.GetUserId(User);


            var model =

            _dashboardService
            .GetDashboard(doctorId);



            var statistics =

            _statisticsService
            .GetStatistics(doctorId);



            ViewBag.Statistics =
            statistics;


            return View(model);
        }
    }
}