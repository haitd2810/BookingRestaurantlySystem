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
            Setting info_setting = context.Settings.FirstOrDefault();
            HttpContext.Session.SetString("InfoSetting", JsonConvert.SerializeObject(info_setting));

            List<Meal> meal = context.Meals.Include(cate => cate.Cate).ToList();
            ViewBag.meal = meal;

            List<Categorymeal> cate = context.Categorymeals.ToList();
            ViewBag.category = cate;

            List<Specialmeal> special_meal = context.Specialmeals.Include(meal => meal.meal).ToList();
            ViewBag.special_meals = special_meal;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
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
