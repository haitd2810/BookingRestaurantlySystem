using booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly bookingDBContext context = new bookingDBContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //footer data
            Setting info_setting = context.Settings.FirstOrDefault();
            HttpContext.Session.SetString("InfoSetting", JsonConvert.SerializeObject(info_setting));

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
