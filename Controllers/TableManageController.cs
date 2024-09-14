using booking.IServices;
using booking.Models;
using booking.Services;
using Microsoft.AspNetCore.Mvc;

namespace booking.Controllers
{
	public class TableManageController : Controller
	{
        private readonly ITableService table_service = new TableService();
		private readonly ITableTypeService tableType_service = new TableTypeService();
        private readonly IOrderService order_service = new OrderService();
        public IActionResult Index()
		{
            if (ModelState.IsValid)
            {
                try
                {
                    List<Table> tableList = table_service.GetTableList();
                    TempData["Table_List"] = tableList;
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");

                }
                return View();
            }
            return View("~/Views/Home/503.cshtml");
        }

        public ActionResult Details(int id)
        {
            if(id == 0)
            {
                return View("~/Views/Home/503.cshtml");
            }
			if (ModelState.IsValid)
			{
				try
				{
					Table table = table_service.FindById(id);
					TempData["table_result"] = table;
					List<Tabletype> table_type = tableType_service.getList();
					TempData["type_list"] = table_type;
                    List<Ordertable> order_list = order_service.getOrderList(id);
                    TempData["orderList"] = order_list;
                }
				catch (Exception ex)
				{
					TempData["error"] = ex.Message;
					return View("~/Views/Home/503.cshtml");

				}
				return View();
			}
			return View("~/Views/Home/503.cshtml");
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int table_id, string table_name, int table_type)
        {
            if(table_id == 0 || table_name == null || table_type == 0)
            {
                return View("~/Views/Home/503.cshtml");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Table table = table_service.FindById(table_id);
                    if(table != null)
                    {
                        table.TableName = table_name;
                        table.TypeTableId = table_type;
                        table_service.UpdateTable(table);
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");

                }
                return RedirectToAction("Details", "TableManage", new
                {
                    id = table_id
                });
            }
            return View("~/Views/Home/503.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int table_id)
        {
            if (table_id == 0)
            {
                return View("~/Views/Home/503.cshtml");
            }
            if (ModelState.IsValid)
            {
                List<Ordertable> order_list = order_service.getOrderList(table_id);
                if (order_list.Count != 0) 
                {
                    return View("~/Views/Home/503.cshtml"); 
                    
                }
                try
                {
                    Table table = table_service.FindById(table_id);
                    if (table != null)
                    {
                        table.Status = new byte[0];
                        table_service.UpdateTable(table);
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");

                }
                return RedirectToAction("Index", "TableManage");
            }
            return View("~/Views/Home/503.cshtml");
        }
    }
}
