using booking.Models;

namespace booking.IServices
{
    public interface IBookingService
    {
        Boolean isBooked(string email, string phone);
        public Bookingtable changeStatus(Bookingtable bookingtable);
    }
}
