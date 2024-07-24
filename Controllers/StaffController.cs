using booking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace booking.Controllers
{
    public class StaffController : Controller
    {
        private readonly bookingDBContext context = new bookingDBContext();
        public IActionResult Index()
        {
            List<Table> table_list = context.Tables.Include(t => t.TypeTable).ToList();
            ViewBag.listTable = table_list;

            return View();
        }

        public ActionResult Details(int id)
        {
            List<Meal> meal_list = context.Meals.Where(meal => meal.Status[0] == 1)
                                   .Include(cate => cate.Cate).ToList();
            ViewBag.mealList = meal_list;

            List<Ordertable> order_list = context.Ordertables.Where(od => od.Status[0] == 1 && od.TableId == id)
                                          .Include(od => od.Meal).ToList();
            ViewBag.orderList = order_list;

            List<Categorymeal> cate_list = context.Categorymeals.Where(cate => cate.Status[0] == 1).ToList();
            ViewBag.category = cate_list;
            return View("Details", id);
        }

        [Route("Staff/Schedule")]
        public IActionResult Schedule()
        {
            return View();
        }

        [Route("Staff/Statistic")]
        public IActionResult Statistic()
        {
            return View();
        }

        [Route("Staff/StatisticStableDetail")]
        public IActionResult StatisticStableDetail()
        {
            return View();
        }

    }
}
