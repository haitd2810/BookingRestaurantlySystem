using booking.DAO;
using booking.IServices;
using booking.Models;
using booking.Services;
using Microsoft.AspNetCore.Mvc;

namespace booking.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IService service = new Service();
        private readonly IBookingService booking_service = new BookingService();
        public IActionResult Index(int pageNumber = 1)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    const int pageSize = 10;
                    pageNumber = service.GetPageNumber(pageNumber, pageSize);
                    TempData["CurrentPage"] = pageNumber;
                    TempData["maxPage"] = (pageNumber <= service.GetMaxPage(pageSize) - 2 ? pageNumber + 1 : service.GetMaxPage(pageSize));

                    List<Bookingtable> bookings = booking_service.GetAll(pageNumber, pageSize);
                    TempData["booking_list"] = bookings;
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
        public IActionResult Update(int id)
        {
            if(id == 0)
            {
                return View("~/Views/Home/503.cshtml");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Bookingtable booking = booking_service.FindByID(id);
                    booking_service.ChangeStatus(booking);
                    booking_service.UpdateBooking(booking);
                    return RedirectToAction("Index", "Schedule");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");
                }
            }
            return View("~/Views/Home/503.cshtml");
        }

        public IActionResult Search(string name)
        {
            if(name == null)
            {
                return View("~/Views/Home/503.cshtml");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var bookings = booking_service.FindByName(name);
                    var bookingViewModels = bookings.Select(b => new BookingViewModel
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Email = b.Email,
                        DateOrder = b.DateOrder.ToString(),
                        TimeOrder = b.TimeOrder.ToString(),
                        TableName = b.Table.TableName,
                        Message = b.Message,
                        Prepay = b.Prepay[0] == 1,
                        Status = b.Status[0] == 1
                    }).ToList();
                    return Ok(bookingViewModels);
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return View("~/Views/Home/503.cshtml");
                }
            }
            return View("~/Views/Home/503.cshtml");
        }
    }
}
