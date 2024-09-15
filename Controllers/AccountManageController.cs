using booking.IServices;
using booking.Models;
using booking.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace booking.Controllers
{
    public class AccountManageController : Controller
    {
        private readonly IStaffService staff_service = new StaffService();
        private readonly IRoleService role_service = new RoleService();
        private readonly ISendMailSerivce user = new SendMailSerivce();
        private readonly IMailSettingService mailSetting_service = new MailSettingService();
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<Staff> staff_list = staff_service.GetStaffList();
                    TempData["staff_list"] = staff_list;
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

        public ActionResult Details(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Staff staff = staff_service.FindById(id);
                    TempData["staff"] = staff;
                    List<Role> role_list = role_service.getRoleList();
                    TempData["role_list"] = role_list;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int account_id, int account_role)
        {
            if (account_id == 0 || account_role == 0)
            {
                return View("~/Views/Home/503.cshtml");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Staff staff = staff_service.FindById(account_id);
                    if (staff != null && staff.Id == account_id) {
                        staff.RoleId = account_role;
                        staff.UpdateDate = DateTime.Now;
                        if (!staff_service.Update(staff)) {
                            return View("~/Views/Home/503.cshtml");
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");

                }
                return RedirectToAction("Details", "AccountManage", new
                {
                    id = account_id
                });
            }
            return View("~/Views/Home/503.cshtml");
        }

        public ActionResult New()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<Role> role_list = role_service.getRoleList();
                    TempData["role_list"] = role_list;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string username, string password, int account_role)
        {
            try
            {
                if (!staff_service.FindByUser(username))
                {
                    return View("~/Views/Home/503.cshtml");
                }
                else
                {
                    Staff staff = new Staff() {
                        Username = username,
                        Password = password,
                        RoleId = account_role,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Status = new byte[1]
                    };
                    staff.Status[0] = 1;

                    if (!staff_service.Add(staff))
                    {
                        return View("~/Views/Home/503.cshtml");
                    }
                    //create variable email to authentication users
                    string email_from = mailSetting_service.getMailSetting().Mail ?? String.Empty;

                    // create variable password to authentication users
                    string email_password = mailSetting_service.getMailSetting().Password ?? String.Empty;

                    //create variable subject
                    const string subject = "Confirm your account";

                    //create body to send mail
                    string body = user.messageCfAccount(username, password);

                    //send mail
                    user.sendMailConfirm(email_from, email_password, username, body, subject);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("~/Views/Home/503.cshtml");

            }
            return RedirectToAction("Index", "AccountManage");
        }

        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (id == 0) return View("~/Views/Home/503.cshtml");
                    Staff staff = staff_service.FindById(id);
                    if (staff != null)
                    {
                        staff.Status[0] = 0;
                    }
                    staff_service.Update(staff);
                    return RedirectToAction("Index", "AccountManage");
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
