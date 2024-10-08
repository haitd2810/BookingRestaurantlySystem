﻿using booking.DAO;
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
        private readonly IOrderHistoryService orderHistory_service = new OrderHistoryService();
        private readonly IOrderService order_service = new OrderService();
        private readonly ITableService table_service = new TableService();
        private readonly IMealService meal_service = new MealService();
        private readonly ICategoryMealService cate_service = new CategoryMealService();
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<Table> table_list = table_service.GetTableList();
                    TempData["listTable"] = table_list;

                    return View();
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");
                }
            }
            return View("~/Views/Home/503.cshtml");
        }

        public ActionResult Details(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<Meal> meal_list = meal_service.GetMeal();
                    TempData["mealList"] = meal_list;


                    List <Ordertable> order_list = order_service.getOrderList(id);
                    TempData["orderList"] = order_list;


                    List <Categorymeal> cate_list = cate_service.GetCate();
                    TempData["category"] = cate_list;


                    TempData["tableID"] = id;
                    return View("Details", id);
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");
                }
            }
            return View("~/Views/Home/503.cshtml");
        }

        public ActionResult Delete(int tableID, int mealID, int odHistory)
        {
            if (tableID == 0 || mealID == 0 || odHistory == 0)
            {
                return View("~/Views/Home/503.cshtml");
            }
            if (ModelState.IsValid)
            {
                try
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
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");
                }
            }
            return View("~/Views/Home/503.cshtml");
        }

        public ActionResult payMeal(int orderHistoryID, int tableID)
        {
            if(orderHistoryID == 0 || tableID == 0)
            {
                return View("~/Views/Home/503.cshtml");
            }
            if (ModelState.IsValid)
            {
                try
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
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");
                }
            }
            return View("~/Views/Home/503.cshtml");
        }
        [HttpPost]
        public IActionResult Update([FromBody] List<SelectedItem> selectedItems)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (selectedItems == null || !selectedItems.Any()) return BadRequest("Invalid data.");

                    int tableID = selectedItems[0].tableID;
                    int orderHistoryID = -1;
                    changeByOrderMeal(selectedItems, orderHistoryID);
                    //update status order of table
                    table_service.MarkTableAsOrdered(tableID, true);

                    return Ok();
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");
                }
            }
            return View("~/Views/Home/503.cshtml");
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
