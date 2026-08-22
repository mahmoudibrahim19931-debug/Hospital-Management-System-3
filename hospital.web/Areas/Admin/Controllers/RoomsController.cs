using Hospital.Models;
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hospital.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoomsController : Controller
    {
        private readonly IRoomService _room;

        public RoomsController(IRoomService room)
        {
            _room = room;
        }

        public IActionResult Index(
    string? search,
    RoomStatus? status,
    RoomType? type,
    int pageNumber = 1,
    int pageSize = 10)
        {
            var rooms = _room.GetAll(pageNumber, pageSize);

            if (!string.IsNullOrWhiteSpace(search))
            {
                rooms.Data = rooms.Data
                    .Where(x =>
                        x.RoomNumber.Contains(search,
                        StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (status.HasValue)
            {
                rooms.Data = rooms.Data
                    .Where(x => x.Status == status.Value)
                    .ToList();
            }

            if (type.HasValue)
            {
                rooms.Data = rooms.Data
                    .Where(x => x.Type == type.Value)
                    .ToList();
            }

            ViewBag.Search = search;
            ViewBag.Status = status;
            ViewBag.Type = type;

            return View(rooms);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var room = _room.GetRoomById(id);

            if (room == null)
                return NotFound();

            return View(room);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new RoomViewModel();

            vm.Hospitals = _room.GetHospitals()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

            vm.Patients = _room.GetPatients()
                .Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name
                }).ToList();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoomViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            _room.InsertRoom(vm);

            TempData["Success"] = "Room created successfully.";

            return Content("Saved Successfully");
        }
        

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vm = _room.GetRoomById(id);

            vm.Hospitals = _room.GetHospitals()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

            vm.Patients = _room.GetPatients()
                .Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name
                }).ToList();

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoomViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            _room.UpdateRoom(vm);

            TempData["Success"] = "Room updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var room = _room.GetRoomById(id);

            if (room == null)
                return NotFound();

            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _room.DeleteRoom(id);

            TempData["Success"] = "Room deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}