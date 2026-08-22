using Hospital.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hospital.web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class BillsController : Controller
    {

        private readonly IBillService _billService;



        public BillsController(
            IBillService billService)
        {
            _billService = billService;
        }




        public IActionResult Index(
    bool? isPaid,
    string search)
        {

            var bills =
                _billService.GetAll();



            if (isPaid.HasValue)
            {

                bills = bills
                    .Where(x =>

                        x.IsPaid ==
                        isPaid.Value)

                    .ToList();

            }




            if (!string.IsNullOrEmpty(search))
            {

                bills = bills
                    .Where(x =>

                        x.PatientName != null

                        &&

                        x.PatientName.Contains(

                            search,

                            StringComparison.OrdinalIgnoreCase

                        )

                    )
                    .ToList();

            }




            return View(bills);

        }

        public IActionResult Details(int id)
        {

            var bill =
                _billService.GetById(id);


            if (bill == null)
                return NotFound();


            return View(bill);

        }

    }


}