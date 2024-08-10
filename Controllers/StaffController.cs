using booking.IServices;
using booking.Models;
using booking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Text.Json.Serialization;

namespace booking.Controllers
{
    

    public class SelectedItem
    {
        public int MealID { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public int tableID { get; set; }
        public int odhistoryID { get; set; }
        public float price { get; set; }
    }
    public class StaffController : Controller
    {
        private readonly Ordertable object_od = new Ordertable();
        private readonly Orderhistory object_odhistory = new Orderhistory();
        private readonly Ordertable order_object = new Ordertable();
        private readonly BookingService booking_service = new BookingService();
        private readonly Bookingtable object_booking = new Bookingtable();
        private readonly Meal meal_object = new Meal();
        private readonly Table table_object = new Table();
        private readonly Categorymeal categorymeal_object = new Categorymeal();
        private readonly IOrderHistoryService orderHistory_service = new OrderHistoryService();
        private readonly IOrderService order_service = new OrderService();
        private readonly IService service = new Service();
        public IActionResult Index()
        {
            List<Table> table_list = table_object.getTableList();
            ViewBag.listTable = table_list;

            return View();
        }

        public ActionResult Details(int id)
        {
            List<Meal> meal_list = meal_object.getMeal();
            ViewBag.mealList = meal_list;

            List<Ordertable> order_list = order_object.getOrderList(id);
            ViewBag.orderList = order_list;

            List<Categorymeal> cate_list = categorymeal_object.getCate();
            ViewBag.category = cate_list;

            ViewBag.tableID = id;

            return View("Details", id);
        }

        public ActionResult DeleteOrderMeal(int tableID, int mealID, int odHistory)
        {
            Ordertable order = object_od.FindOrder(tableID, mealID, odHistory);
            
            List<Ordertable> order_list = object_od.FindOrder(tableID,odHistory);
            
            //delete in db
            orderHistory_service.deleteByZeroMeal(order_list, order, odHistory, tableID);
            return RedirectToAction("Details", "Staff", new
            {
                id = tableID
            });
        }

        public ActionResult payMeal(int orderHistoryID, int tableID)
        {
            if (orderHistoryID == -1 || tableID == -1) return RedirectToAction("Details", "Staff", new
                                                                              {
                                                                                  id = tableID
                                                                              });

            //update order table for payment
            object_od.updatePaymeal(orderHistoryID, tableID);

            //update order history for payment
            Orderhistory order_history = object_odhistory.findbyID(orderHistoryID);
            float total = object_odhistory.getTotal(order_history);
            order_history = orderHistory_service.updateByPayMeal(order_history, total);
            order_history.UpdateOrderHistory();

            //update status table
            table_object.markTableAsOrdered(tableID, false);

            return RedirectToAction("Details", "Staff", new
            {
                id = tableID
            });
        }
        [HttpPost]
        public IActionResult OrderMeal([FromBody] List<SelectedItem> selectedItems)
        {
            if (selectedItems == null || !selectedItems.Any()) return BadRequest("Invalid data.");

            int tableID = selectedItems[0].tableID;
            int orderHistoryID = -1;
            changeByOrderMeal(selectedItems, orderHistoryID);
            //update status order of table
            table_object.markTableAsOrdered(tableID, true);

            return Ok();
        }

/*        [Route("Staff/Schedule")]*/
        public IActionResult Schedule(int pageNumber)
        {
            const int pageSize = 10;
            pageNumber = service.getPageNumber(pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.maxPage = (pageNumber <= service.getMaxPage(pageSize) - 2? pageNumber+1 : service.getMaxPage(pageSize));

            List<Bookingtable> bookings = object_booking.getAll(pageNumber,pageSize);
            ViewBag.booking_list = bookings;
            return View();
        }
        [HttpPost]
        public IActionResult confirmBooking(int id)
        {
            Bookingtable booking = object_booking.findByID(id);
            booking_service.changeStatus(booking);
            booking.updateBooking();
            return RedirectToAction("Schedule", "Staff");
        }

        public IActionResult search(string name)
        {
            List<Bookingtable> booking = object_booking.findByName(name);
            return Ok(booking);
        }
/*        [Route("Staff/Statistic")]*/
        public IActionResult Statistic()
        {
            List<Orderhistory> list_ord_history = object_odhistory.getAll();
            ViewBag.order_history = list_ord_history;

            List<Table> table_list = table_object.getTableList();
            ViewBag.table_list = table_list;

            List<Total_Statistics> list_total = orderHistory_service.getTotalStatistic(list_ord_history);
            ViewBag.total = list_total;
            return View();
        }

        [Route("Staff/StatisticStableDetail")]
        public IActionResult StatisticStableDetail()
        {
            return View();
        }

        private void changeByOrderMeal(List<SelectedItem> selectedItems, int orderHistoryID)
        {
            foreach (var item in selectedItems)
            {
                Ordertable od = object_od.FindOrder(item.tableID, item.MealID, item.odhistoryID);
                if (od != null)
                {
                    od = order_service.changeByOrderMeal(od, item.quantity, item.price);
                    od.UpdateOrderTable();
                }
                else
                {
                    orderHistoryID = orderHistory_service.getIDOrderHistory(item.odhistoryID, orderHistoryID);
                    Ordertable new_od = order_service.setOrder(item.MealID, item.quantity, item.price, item.tableID, DateTime.Now,
                                                               DateTime.Now, new byte[] { 1 }, orderHistoryID);
                    new_od.AddOrderTable();
                }
            }
        }
    }
}
