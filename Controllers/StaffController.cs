using booking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            ViewBag.tableID = id;

            return View("Details", id);
        }

        public ActionResult DeleteOrderMeal(int tableID, int mealID, int odHistory)
        {
            Ordertable order = object_od.FindOrder(tableID, mealID);
            
            List<Ordertable> order_list = context.Ordertables
                                       .Where(od => od.TableId == tableID && od.OdHistoryId == odHistory)
                                       .ToList();
            if (order != null) order.DeleteOrderTable();
            //check if there are more than 1 meal in order?
            if (order_list.Count() == 1 && order_list[0].Quantity == 1)
            {
                object_odhistory.deleteByID(int.Parse(order_list[0].OdHistoryId.ToString()));
            }

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
            object_odhistory.updatePayment(orderHistoryID);
            return RedirectToAction("Details", "Staff", new
            {
                id = tableID
            });
        }
        [HttpPost]
        public IActionResult OrderMeal([FromBody] List<SelectedItem> selectedItems)
        {
            if (selectedItems == null || !selectedItems.Any())
            {
                return BadRequest("Invalid data.");
            }
            int tableID = 0;
            foreach (var item in selectedItems)
            {
                Ordertable od = object_od.FindOrder(item.tableID,item.MealID);
                if (od != null)
                {
                    od.Quantity += item.quantity;
                    od.Price += item.price;
                    od.UpdateOrderTable();
                }
                else
                {
                    int orderHistoryID = item.odhistoryID;
                    if (item.odhistoryID == -1) { 
                        Orderhistory od_history = new Orderhistory()
                        {
                            Fullname = null,
                            Email = null,
                            CreateDate = DateTime.Now,
                            UpdateDate  = DateTime.Now,
                            TotalPrice = 0,
                            Payed = new byte[] { 0 },
                            Status = new byte[] { 1 }

                        };
                        od_history.AddOderHistory();
                        orderHistoryID = od_history.Id;
                    }
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
            Table table = context.Tables.Where(tb => tb.Id == tableID).FirstOrDefault();
            if (table != null)
            {
                Console.WriteLine("thay table");
                table.Status = new byte[] { 1 };
                table.UpdateTable();
            }

            return Ok();
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
