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
        public IActionResult StatisticStaff()
        {
            List<Orderhistory> list_ord_history = object_odhistory.getAll();
            ViewBag.order_history = list_ord_history;

            List<Table> table_list = table_object.getTableList();
            ViewBag.table_list = table_list;

            List<Total_Statistics> list_total = orderHistory_service.getTotalStatistic(list_ord_history);
            ViewBag.total = list_total;
            return View();
        }
    }
}
