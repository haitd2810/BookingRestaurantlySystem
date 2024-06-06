using Microsoft.AspNetCore.Mvc;

namespace booking.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            ViewData["CurrentPage"] = "Login";
            return View();
        }
    }
}
