using booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json;
using booking.Services;


namespace booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly bookingDBContext context = new bookingDBContext();
        private readonly IUserManage user = new UserManage();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //footer data
            Setting info_setting = context.Settings.FirstOrDefault();
            ViewBag.infosetting = info_setting;

            //menu data
            List<Meal> meal_list = context.Meals.Where(meal => meal.Status[0] == 1).Include(cate => cate.Cate).ToList();
            ViewBag.meal = meal_list;

            //category meal data
            List<Categorymeal> cate_list = context.Categorymeals.Where(cate => cate.Status[0] == 1).ToList();
            ViewBag.category = cate_list;

            //special meal data
            List<Specialmeal> special_meal_list = context.Specialmeals.Where(spec => spec.Status[0] == 1).Include(meal => meal.meal).ToList();
            ViewBag.special_meals = special_meal_list;

            //post data
            List<Post> post_list = context.Posts.Where(post => post.Status[0] == 1).ToList();
            ViewBag.post = post_list;

            //feedback data
            List<Feedback> feedback_list = context.Feedbacks.Where(fb => fb.Status[0] == 1).ToList();
            ViewBag.feedback = feedback_list;

            //photo  data
            List<Photorestaurant> photo_list = context.Photorestaurants.Where(img => img.Status[0] == 1).ToList();
            ViewBag.photo = photo_list;

            //booking list data
            List<Bookingtable> booking_list = context.Bookingtables.ToList();
            ViewBag.booking = booking_list;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createFeedback(string name, string jobs, string email, string phone, string img, string feedback)
        {
            Bookingtable booked = new Bookingtable();
            if (!booked.isBooked(email, phone) )
            {
                Feedback fb = new Feedback()
                {
                    Name = name,
                    Jobs = jobs,
                    Detail = feedback,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Status = new byte[] {0},
                    Img = img == null? "/assets/img/testimonials/d8b5d0a738295345ebd8934b859fa1fca1c8c6ad.jpeg" : img.ToString()
                };
                fb.giveFeedback();
                string email_from = context.Mailsettings.FirstOrDefault().Mail;
                string email_password = context.Mailsettings.FirstOrDefault().Password;
                string subject = "Thanks for your response";
                string body = user.messageFeedback(name);
                user.sendMailConfirm(email_from,email_password,email,body,subject);
            }
            return RedirectToAction("Index", "Home");

        }
            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
