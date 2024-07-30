using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class BookingService : IBookingService
    {
        public Bookingtable changeStatus(Bookingtable booking)
        {
            if (booking.Status[0] == 1) booking.Status = new byte[] { 0 };
            else if (booking.Status[0] == 0) booking.Status = new byte[] { 1 };
            return booking;
        }
    }
}
