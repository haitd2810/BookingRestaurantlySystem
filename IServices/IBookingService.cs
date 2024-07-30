using booking.Models;

namespace booking.IServices
{
    public interface IBookingService
    {
        public Bookingtable changeStatus(Bookingtable bookingtable);
    }
}
