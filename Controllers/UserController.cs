using booking.Models;
using Microsoft.AspNetCore.Mvc;

namespace booking.Controllers
{
    public class UserController : Controller
    {
        private readonly bookingDBContext context = new bookingDBContext();
        public IActionResult Index()
        {
            ViewData["CurrentPage"] = "Login";
            return View();
        }
        private Boolean isValidStaff(string username, string password)
        {
            Staff staff = context.Staff.Where(staff => staff.Username == username && staff.Password == password).FirstOrDefault();
            return staff != null;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Staff staff)
        {
            if(isValidStaff(staff.Username, staff.Password))
            {
                return RedirectToAction("Index", "Staff");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }
    }
}
