using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class BookingService : IBookingService
    {
        public Bookingtable changeStatus(Bookingtable booking) => Bookingtable.Instance.changeStatus(booking);

        public Bookingtable findByID(int id) => Bookingtable.Instance.findByID(id);

        public List<Bookingtable> findByName(string name) => Bookingtable.Instance.findByName(name);

        public List<Bookingtable> getAll(int pageNumber, int pageSize) => Bookingtable.Instance.getAll(pageNumber, pageSize);

        public List<Bookingtable> getAll() => Bookingtable.Instance.getAll();

        public Boolean isBooked(string email, string phone) => Bookingtable.Instance.isBooked(email,phone);

        public bool updateBooking(Bookingtable booking) => Bookingtable.Instance.updateBooking(booking);
    }
}
