using booking.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using booking.Services;
using booking.IServices;


namespace booking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISendMailSerivce user_service = new SendMailSerivce();
        private readonly IFeedbackService fb_service = new FeedbackService();
        private readonly IBookingService book_service = new BookingService();
        private readonly ISettingService settingService = new SettingService();
        private readonly IMailSettingService mailSetting_service = new MailSettingService();
        private readonly IMealService meal_service = new MealService();
        private readonly ICategoryMealService categorymeal_service = new CategoryMealService();
        private readonly ISpecialMealService spmeal_Service = new SpecialMealService();
        private readonly IPostService post_service = new PostService();
        private readonly IPhotoService photo_service = new PhotoService();
        const string default_img = "/assets/img/testimonials/d8b5d0a738295345ebd8934b859fa1fca1c8c6ad.jpeg";
        const string path_save_feedback = "wwwroot/assets/img/uploads";

        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //footer data
                    Setting info_setting = settingService.getSetting();
                    TempData["infosetting"] = info_setting;

                    //menu data
                    List<Meal> meal_list = meal_service.GetMeal();
                    TempData["meal"] = meal_list;

                    //category meal data
                    List<Categorymeal> cate_list = categorymeal_service.GetCate();
                    TempData["category"] = cate_list;

                    //special meal data
                    List <Specialmeal> special_meal_list = spmeal_Service.GetSepcialMeal();
                    TempData["special_meals"] = special_meal_list;

                    //post data
                    List <Post> post_list = post_service.getPost();
                    TempData["post"] = post_list;

                    //feedback data
                    List <Feedback> feedback_list = fb_service.GetFeedback();
                    TempData["feedback"] = feedback_list;

                    //photo  data
                    List <Photorestaurant> photo_list = photo_service.getphoto();
                    TempData["photo"] = photo_list;

                    //booking list data
                    List <Bookingtable> booking_list = book_service.GetAll();
                    TempData["booking"] = booking_list;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
#pragma warning disable S6967
        public async Task<ActionResult> createFeedback(string name, string jobs, string email, string phone, IFormFile img, string feedback)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //declare variable
                    string subject = "Thanks for your response";
                    //check user who give feedback, have yet booked table
                    if (!book_service.IsBooked(email, phone))
                    {
                        //get path to save file img of user
                        string imgPath = await fb_service.PathToSave(img,path_save_feedback,default_img);

                        // create object of feedback
                        Feedback fb = fb_service.SetValue(name, jobs, feedback, DateTime.Now, DateTime.Now, (new byte[] { 0 }), imgPath);

                        //save feedback to database
                        fb_service.AddFeedback(fb);

                        //create email variable to authenticate user
                        string email_from = mailSetting_service.getMailSetting().Mail ?? string.Empty;

                        //create password variable to authenticate user
                        string email_password = mailSetting_service.getMailSetting().Password ?? string.Empty;

                        //create body to send mail
                        string body = user_service.messageFeedback(name);

                        //send mail
                        user_service.sendMailConfirm(email_from, email_password, email, body, subject);
                    }
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");
                }
            }
            return View("~/Views/Home/503.cshtml");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
