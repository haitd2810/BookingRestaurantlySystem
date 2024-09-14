using booking.IServices;
using booking.Models;
using booking.Services;
using Microsoft.AspNetCore.Mvc;

namespace booking.Controllers
{
    public class MealManageController : Controller
    {
        private readonly IMealService mealService = new MealService();
        private readonly ICategoryMealService cateService = new CategoryMealService();
        const string path_save_meal = "wwwroot/assets/img/menu";
        const string default_img = "/assets/img/menu/default-meal.png";
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<Meal> meal_list = mealService.GetMeal();
                    TempData["Meal_List"] = meal_list;
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
            if (ModelState.IsValid)
            {
                try
                {
                    Meal meal = mealService.FindByID(id);
                    TempData["Meal"] = meal;
                    List<Categorymeal> cateList = cateService.GetCate();
                    TempData["CategoryList"] = cateList;
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
        public async Task<ActionResult> Edit(int meal_id, string meal_name, float price, string intro, int meal_cate, IFormFile img)
        {
            try
            {
                Meal meal = mealService.FindByID(meal_id);
                string path = meal.Img;

                if (img != null && img.Length > 0)
                {
                    path = await mealService.PathToSave(img, path_save_meal, default_img);
                }
                meal = mealService.SetValue(meal, meal_name, intro, price, meal_cate, path);

                if (!mealService.UpdateMeal(meal))
                {
                    return View("~/Views/Home/503.cshtml");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("~/Views/Home/503.cshtml");
            }

            return RedirectToAction("Details", "MealManage", new { id = meal_id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Meal meal = mealService.FindByID(id);
                    meal.Status = new byte[0];
                    mealService.UpdateMeal(meal);
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");

                }
                return RedirectToAction("Index", "MealManage");
            }
            return View("~/Views/Home/503.cshtml");
        }

        public ActionResult New()
        {
            if (ModelState.IsValid)
            {
                List<Categorymeal> cateList = cateService.GetCate();
                TempData["CategoryList"] = cateList;
                return View();
            }
            return View("~/Views/Home/503.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string meal_name, float price, string intro, int meal_cate, IFormFile img)
        {
            try
            {
                string path = default_img;
                if (img != null && img.Length > 0)
                {
                    path = await mealService.PathToSave(img, path_save_meal, default_img);
                }
                Meal meal = new Meal()
                {
                    Name = meal_name,
                    Price = price,
                    Intro = intro,
                    CateId = meal_cate,
                    Img = path,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    Status = new byte[1]
                };
                meal.Status[0] = 1;
                if (!mealService.AddMeal(meal)) {
                    return View("~/Views/Home/503.cshtml");
                }
               
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("~/Views/Home/503.cshtml");
            }

            return RedirectToAction("Index", "MealManage");
        }


    }
}
