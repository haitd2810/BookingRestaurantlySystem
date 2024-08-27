using booking.Models;

namespace booking.IServices
{
    public interface IBookingService
    {
        Boolean UpdateBooking(Bookingtable booking);
        List<Bookingtable>GetAll(int pageNumber, int pageSize);
        List<Bookingtable> GetAll();
        Bookingtable FindByID(int id);
        List<Bookingtable> FindByName(string name);
        Boolean IsBooked(string email, string phone);
        public Bookingtable ChangeStatus(Bookingtable bookingtable);
    }
}
