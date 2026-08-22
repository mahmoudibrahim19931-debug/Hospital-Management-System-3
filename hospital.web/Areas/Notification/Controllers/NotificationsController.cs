using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Hospital.Services;

[Area("Notification")]
[Authorize]
public class NotificationsController : Controller
{
    private readonly INotificationService _service;

    public NotificationsController(INotificationService service)
    {
        _service = service;
    }


    public IActionResult Index()
    {

        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier);



        _service.MarkAllAsRead(userId);



        var notifications =

            _service.GetUserNotifications(
                userId);



        return View(notifications);

    }

    public IActionResult Read(int id)
    {

        _service.MarkAsRead(id);

        return RedirectToAction(nameof(Index));

    }

    public IActionResult Open(int id)
    {

        var notification =
        _service.GetById(id);



        if (notification == null)
            return NotFound();



        _service.MarkAsRead(id);



        return Redirect(

        notification.Url);


    }


}