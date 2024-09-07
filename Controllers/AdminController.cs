using Microsoft.AspNetCore.Mvc;

namespace booking.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
			if (ModelState.IsValid) {
				try
				{
					
				}
				catch (Exception ex)
				{
					TempData["error"] = ex.Message;
					return View("~/Views/Home/503.cshtml");

				}
				return View();
			}
			return View("~/Views/Home/503.cshtml");

		}
    }
}
