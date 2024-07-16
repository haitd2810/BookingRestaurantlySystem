using booking.Models;
using booking.Services;
using Microsoft.AspNetCore.Mvc;

namespace booking.Controllers
{
    public class UserController : Controller
    {
        private readonly bookingDBContext context = new bookingDBContext();
        private readonly IUserManage user = new UserManage();
        public IActionResult Create()
        {
            ViewData["CurrentPage"] = "Login";
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Staff staff)
        {
            Staff staff_method = new Staff();
            if(staff_method.isStaff(staff.Username, staff.Password))
            {
                HttpContext.Items["msg_login"] = "Login Success";
                return RedirectToAction("Index", "Staff");
            }
            else
            {
                HttpContext.Items["msg_login"] = "Failed Login";
                return View("~/Views/User/Create.cshtml");
            }
        }

        public IActionResult booking()
        {
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult booking(string name, string email, string phone, string date, string time, string tableNumber, string message)
        {
            // confirm email
            string email_from = context.Mailsettings.FirstOrDefault().Mail;
            string email_password = context.Mailsettings.FirstOrDefault().Password;
            string subject = "Confirm your booking";
            string body = user.messageConfirm(name, email, phone, date, time, tableNumber, message);
            user.sendMailConfirm(email_from, email_password, email, body, subject);

            return RedirectToAction("Index", "Home");
        }
    }
}
