using booking.IServices;
using booking.Models;
using booking.Services;
using Microsoft.AspNetCore.Mvc;

namespace booking.Controllers
{
    public class UserController : Controller
    {
        private readonly bookingDBContext context = new bookingDBContext();
        private readonly ISendMailSerivce user = new SendMailSerivce();
        private readonly IStaffService staff_service = new StaffService();
        private readonly IMailSettingService mailSetting_service = new MailSettingService();
        private readonly Mailsetting mail_setting_object = new Mailsetting();
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
            string username = staff.Username ?? String.Empty;
            string password = staff.Password ?? String.Empty;
            string message = staff_service.setMessageLogin(username, password);
            HttpContext.Items["msg_login"] = message;
            if (message == "Login Success") return RedirectToAction("Index", "Staff");
            else return View("~/Views/User/Create.cshtml");
        }

        public IActionResult booking()
        {
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult booking(string name, string email, string phone, string date, string time, string tableNumber, string message)
        {
            // create variable email to authentication users
            string email_from = mail_setting_object.getMailSetting().Mail ?? String.Empty;

            // create variable password to authentication users
            string email_password = mail_setting_object.getMailSetting().Password ?? String.Empty;

            //create variable subject
            const string subject = "Confirm your booking";

            //create body to send mail
            string body = user.messageConfirm(name, email, phone, date, time, tableNumber, message);

            //send mail
            user.sendMailConfirm(email_from, email_password, email, body, subject);

            return RedirectToAction("Index", "Home");
        }
    }
}
