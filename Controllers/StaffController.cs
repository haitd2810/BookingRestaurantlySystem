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
        private readonly IBookingService booking_service = new BookingService();
        private readonly IOrderHistoryService orderHistory_service = new OrderHistoryService();
        private readonly IOrderService order_service = new OrderService();
        private readonly IService service = new Service();
        private readonly ITableService table_service = new TableService();
        private readonly IMealService meal_service = new MealService();
        private readonly ICategoryMealService cate_service = new CategoryMealService();
        public IActionResult Index()
        {
            List<Table> table_list = table_service.GetTableList();
            ViewBag.listTable = table_list;

            return View();
        }

        public ActionResult Details(int id)
        {
            List<Meal> meal_list = meal_service.GetMeal();
            ViewBag.mealList = meal_list;

            List<Ordertable> order_list = order_service.getOrderList(id);
            ViewBag.orderList = order_list;

            List<Categorymeal> cate_list = cate_service.GetCate();
            ViewBag.category = cate_list;

            ViewBag.tableID = id;

            return View("Details", id);
        }

        public ActionResult DeleteOrderMeal(int tableID, int mealID, int odHistory)
        {
            Ordertable order = order_service.FindOrder(tableID, mealID, odHistory);
            
            List<Ordertable> order_list = order_service.FindOrder(tableID,odHistory);
            
            //delete in db
            orderHistory_service.DeleteByZeroMeal(order_list, order, odHistory, tableID);
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
            order_service.updatePaymeal(orderHistoryID, tableID);

            //update order history for payment
            Orderhistory order_history = orderHistory_service.FindbyID(orderHistoryID);
            float total = orderHistory_service.GetTotal(order_history);
            order_history = orderHistory_service.UpdateByPayMeal(order_history, total);
            orderHistory_service.UpdateOrderHistory(order_history);

            //update status table
            table_service.MarkTableAsOrdered(tableID, false);

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
            table_service.MarkTableAsOrdered(tableID, true);

            return Ok();
        }

/*        [Route("Staff/Schedule")]*/
        public IActionResult Schedule(int pageNumber)
        {
            const int pageSize = 10;
            pageNumber = service.GetPageNumber(pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.maxPage = (pageNumber <= service.GetMaxPage(pageSize) - 2? pageNumber+1 : service.GetMaxPage(pageSize));

            List<Bookingtable> bookings = booking_service.GetAll(pageNumber,pageSize);
            ViewBag.booking_list = bookings;
            return View();
        }
        [HttpPost]
        public IActionResult confirmBooking(int id)
        {
            Bookingtable booking = booking_service.FindByID(id);
            booking_service.ChangeStatus(booking);
            booking_service.UpdateBooking(booking);
            return RedirectToAction("Schedule", "Staff");
        }

        public IActionResult search(string name)
        {
            List<Bookingtable> booking = booking_service.FindByName(name);
            return Ok(booking);
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
                Ordertable od = order_service.FindOrder(item.tableID, item.MealID, item.odhistoryID);
                if (od != null)
                {
                    od = order_service.changeByOrderMeal(od, item.quantity, item.price);
                    order_service.UpdateOrderTable(od);
                }
                else
                {
                    orderHistoryID = orderHistory_service.GetIDOrderHistory(item.odhistoryID, orderHistoryID);
                    Ordertable new_od = order_service.setOrder(item.MealID, item.quantity, item.price, item.tableID, DateTime.Now,
                                                               DateTime.Now, new byte[] { 1 }, orderHistoryID);
                    order_service.AddOrderTable(new_od);
                }
            }
        }
    }
}
