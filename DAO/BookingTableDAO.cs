using booking.Models;
using Microsoft.EntityFrameworkCore;

namespace booking.DAO
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DateOrder { get; set; }
        public string TimeOrder { get; set; }
        public string TableName { get; set; }
        public string Message { get; set; }
        public bool Prepay { get; set; }
        public bool Status { get; set; }
    }
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
                          .FirstOrDefault() ?? new Bookingtable();
        }

        public List<Bookingtable> FindByName(string name)
        {
            if (name == null || name.Length == 0) return GetAll();
            return context.Bookingtables
                          .Where(book => book.Name.ToLower().Contains(name.ToLower()))
                          .Include(booked => booked.Table)
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
