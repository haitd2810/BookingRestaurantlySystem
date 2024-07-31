using booking.IServices;
using booking.Models;
using booking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly bookingDBContext context = new bookingDBContext();
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
            if (orderHistoryID == -1 || tableID == -1) 
                return RedirectToAction("Details", "Staff", new
                {
                    id = tableID
                });
            //update order table for payment
            object_od.updatePaymeal(orderHistoryID, tableID);

            //update order history for payment
            Orderhistory order_history = object_odhistory.findbyID(orderHistoryID);
            order_history.TotalPrice = object_odhistory.getTotal(order_history);
            order_history.Payed = new byte[] { 1 };
            order_history.UpdateDate = DateTime.Now;
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

            int tableID = 0;
            int orderHistoryID = -1;
            foreach (var item in selectedItems)
            {
                Ordertable od = object_od.FindOrder(item.tableID,item.MealID,item.odhistoryID);
                if (od != null)
                {
                    od.Quantity += item.quantity;
                    od.Price += item.price;
                    od.UpdateOrderTable();
                }
                else
                {
                    //check orderHistoryID is exist or not
                    if (orderHistoryID == -1 && item.odhistoryID == -1) { 
                        Orderhistory od_history = orderHistory_service.setDefault();
                        od_history.AddOderHistory();
                        orderHistoryID = od_history.Id;
                    }
                    //check orderHistoryID is exist or not
                    if (orderHistoryID == -1 && item.odhistoryID != -1) orderHistoryID = item.odhistoryID;
                    Ordertable new_od = new Ordertable()
                    {
                        MealId = item.MealID,
                        Quantity = item.quantity,
                        Price = item.price,
                        TableId = item.tableID,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Status = new byte[] { 1 },
                        OdHistoryId = orderHistoryID
                    };
                    new_od.AddOrderTable();
                    tableID = item.tableID;
                }
            }
            //update status order of table
            table_object.markTableAsOrdered(tableID, true);

            return Ok();
        }

        [Route("Staff/Schedule")]
        public IActionResult Schedule()
        {
            List<Bookingtable> bookings = object_booking.getAll();
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
