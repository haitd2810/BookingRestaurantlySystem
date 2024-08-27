using booking.Models;
using Microsoft.EntityFrameworkCore;

namespace booking.DAO
{
    public class BookingTableDAO
    {
        private static readonly object instaceLock = new object();
        private static BookingTableDAO instance = null;
        private readonly bookingDBContext context = new bookingDBContext();
        public static BookingTableDAO Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new BookingTableDAO();
                    return instance;
                }
            }
        }

        public Boolean UpdateBooking(Bookingtable booking)
        {
            if (booking == null) return false;
            context.Bookingtables.Update(booking);
            int row = context.SaveChanges();
            if (row <= 0)
            {
                Console.WriteLine("False: " + row);
                return false;
            }

            return true;
        }

        public List<Bookingtable> GetAll(int pageNumber, int pageSize)
        {
            return context.Bookingtables.Include(book => book.Table)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .OrderByDescending(book => book.Status[0])
                                        .ToList();
        }
        public List<Bookingtable> GetAll()
        {
            return context.Bookingtables.Include(book => book.Table)
                                        .ToList();
        }

        public Bookingtable FindByID(int id)
        {
            return context.Bookingtables
                          .Where(b => b.Id == id)
                          .FirstOrDefault();
        }

        public List<Bookingtable> FindByName(string name)
        {
            if (name == null) return GetAll();
            return context.Bookingtables
                          .Where(book => book.Name.ToLower().Contains(name.ToLower()))
                          .ToList();
        }

        public Bookingtable ChangeStatus(Bookingtable booking)
        {
            booking.Status = new byte[] { 0 };
            return booking;
        }

        public Boolean IsBooked(string email, string phone)
        {
            bookingDBContext context = new bookingDBContext();
            var booked = context.Bookingtables.Where(book => book.Email == email && book.Phone == phone).FirstOrDefault();
            return booked == null;
        }
    }
}
