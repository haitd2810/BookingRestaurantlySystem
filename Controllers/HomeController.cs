using booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json;
using booking.Services;
using booking.IServices;


namespace booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly bookingDBContext context = new bookingDBContext();
        private readonly ISendMailSerivce user_service = new SendMailSerivce();
        private readonly FeedbackService fb_service = new FeedbackService();
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
        public async Task<ActionResult> createFeedback(string name, string jobs, string email, string phone, IFormFile img, string feedback)
        {
            Bookingtable booked = new Bookingtable();

            //check user who give feedback, have yet booked table
            if (!booked.isBooked(email, phone))
            {
                string imgPath = "/assets/img/testimonials/d8b5d0a738295345ebd8934b859fa1fca1c8c6ad.jpeg";

                //check img that user upload is true format or not
                if(fb_service.isTrueIMG(img))
                {
                    string path = "wwwroot/assets/img/uploads";
                    await fb_service.saveFile(img, path);
                    imgPath = "/assets/img/uploads/" + img.FileName;
                }
                Feedback fb = new Feedback()
                {
                    Name = name,
                    Jobs = jobs,
                    Detail = feedback,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Status = new byte[] { 0 },
                    Img = imgPath
                };
                fb.giveFeedback();
                string email_from = context.Mailsettings.FirstOrDefault().Mail;
                string email_password = context.Mailsettings.FirstOrDefault().Password;
                string subject = "Thanks for your response";
                string body = user_service.messageFeedback(name);
                user_service.sendMailConfirm(email_from, email_password, email, body, subject);
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
