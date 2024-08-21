using booking.IServices;
using booking.Models;
using booking.Services;
using Microsoft.AspNetCore.Mvc;

namespace booking.Controllers
{
    public class DashboardController : Controller
    {
        private readonly Orderhistory object_odhistory = new Orderhistory();
        private readonly Table table_object = new Table();
        private readonly IOrderHistoryService orderHistory_service = new OrderHistoryService();
        public IActionResult StatisticStaff(DateTime? start = null, DateTime? end = null)
        {
            start ??= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            end ??= DateTime.Now;
            ViewBag.startDate = start; ViewBag.endDate = end;
            List<Orderhistory> list_ord_history = object_odhistory.getAll();
            ViewBag.order_history = list_ord_history;

            List<Table> table_list = table_object.getTableList();
            ViewBag.table_list = table_list;

            List<Orderhistory> filterOrd = orderHistory_service.getListByDate(list_ord_history, start, end);
            List<Total_Statistics> list_total = orderHistory_service.getTotalStatistic(filterOrd, start, end);
            ViewBag.total = list_total;
            return View();
        }
    }
}
