using booking.IServices;
using booking.Models;
using booking.Services;
using Microsoft.AspNetCore.Mvc;

namespace booking.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IOrderHistoryService orderHistory_service = new OrderHistoryService();
        public IActionResult StatisticStaff(DateTime? start = null, DateTime? end = null)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    start ??= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0, DateTimeKind.Local);
                    end ??= DateTime.Now;
                    TempData["startDate"] = start; TempData["endDate"] = end;

                    List<Orderhistory> list_ord_history = orderHistory_service.GetAll() ?? new List<Orderhistory>();
                    List<Orderhistory> filterOrd = orderHistory_service.GetListByDate(list_ord_history, start, end) ?? new List<Orderhistory>();
                    List<Total_Statistics> list_total = orderHistory_service.GetTotalStatistic(filterOrd, start, end) ?? new List<Total_Statistics>();
                    TempData["total"] = list_total;
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");

                }
            }
            return View();
        }
    }
}
