using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class BookingService : IBookingService
    {
        public Bookingtable ChangeStatus(Bookingtable booking) => BookingTableDAO.Instance.ChangeStatus(booking);

        public Bookingtable FindByID(int id) => BookingTableDAO.Instance.FindByID(id);

        public List<Bookingtable> FindByName(string name) => BookingTableDAO.Instance.FindByName(name);

        public List<Bookingtable> GetAll(int pageNumber, int pageSize) => BookingTableDAO.Instance.GetAll(pageNumber, pageSize);

        public List<Bookingtable> GetAll() => BookingTableDAO.Instance.GetAll();

        public Boolean IsBooked(string email, string phone) => BookingTableDAO.Instance.IsBooked(email,phone);

        public bool UpdateBooking(Bookingtable booking) => BookingTableDAO.Instance.UpdateBooking(booking);
    }
}
