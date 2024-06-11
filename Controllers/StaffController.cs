using Microsoft.AspNetCore.Mvc;

namespace booking.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
