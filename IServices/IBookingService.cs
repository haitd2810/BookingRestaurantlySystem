using booking.Models;

namespace booking.IServices
{
    public interface IBookingService
    {
        Boolean updateBooking(Bookingtable booking);
        List<Bookingtable> getAll(int pageNumber, int pageSize);
        List<Bookingtable> getAll();
        Bookingtable findByID(int id);
        List<Bookingtable> findByName(string name);
        Boolean isBooked(string email, string phone);
        public Bookingtable changeStatus(Bookingtable bookingtable);
    }
}
