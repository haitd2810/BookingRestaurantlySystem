using booking.IServices;
using booking.Models;
using booking.Services;
using Microsoft.AspNetCore.Mvc;

namespace booking.Controllers
{
    public class UserController : Controller
    {
        private readonly ISendMailSerivce user = new SendMailSerivce();
        private readonly IStaffService staff_service = new StaffService();
        private readonly IMailSettingService mailSetting_service = new MailSettingService();
        public IActionResult Create()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewData["CurrentPage"] = "Login";
                    return View();
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");
                }
            }
            return View("~/Views/Home/503.cshtml");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Staff staff)
        {
            if(staff == null)
            {
                return View("~/Views/Home/503.cshtml");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    string username = staff.Username ?? String.Empty;
                    string password = staff.Password ?? String.Empty;
                    string message = staff_service.setMessageLogin(username, password);
                    HttpContext.Items["msg_login"] = message;
                    if (message == "Login Success") return RedirectToAction("Index", "Staff");
                    else return View("~/Views/User/Create.cshtml");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");
                }
            }
            return View("~/Views/Home/503.cshtml");
        }

        public IActionResult booking()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");
                }
            }
            return View("~/Views/Home/503.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult booking(string name, string email, string phone, string date, string time, string tableNumber, string message)
        {
                try
                {
                    // create variable email to authentication users
                    string email_from = mailSetting_service.getMailSetting().Mail ?? String.Empty;

                    // create variable password to authentication users
                    string email_password = mailSetting_service.getMailSetting().Password ?? String.Empty;

                    //create variable subject
                    const string subject = "Confirm your booking";

                    //create body to send mail
                    string body = user.messageConfirm(name, email, phone, date, time, tableNumber, message);

                    //send mail
                    user.sendMailConfirm(email_from, email_password, email, body, subject);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");
                }
        }
    }
}
