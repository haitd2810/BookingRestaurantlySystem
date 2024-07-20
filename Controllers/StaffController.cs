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
