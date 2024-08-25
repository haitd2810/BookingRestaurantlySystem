using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace booking.Models
{
    public partial class Bookingtable
    {
        public int Id { get; set; }
        public int? TableId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateOnly? DateOrder { get; set; }
        public TimeOnly? TimeOrder { get; set; }
        public string? Message { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte[]? Prepay { get; set; }
        public byte[]? Status { get; set; }

        public virtual Table? Table { get; set; }
        private readonly bookingDBContext context = new bookingDBContext();

        private static readonly object instaceLock = new object();
        private static Bookingtable instance = null;

        public static Bookingtable Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new Bookingtable();
                    return instance;
                }
            }
        }

        public Boolean updateBooking(Bookingtable booking)
        {
            if(booking == null) return false;
            context.Bookingtables.Update(booking);
            int row = context.SaveChanges();
            if(row <= 0)
            {
                Console.WriteLine("False: " + row);
                return false;
            }
                
            return true;
        }

        public List<Bookingtable> getAll(int pageNumber, int pageSize)
        {
            return context.Bookingtables.Include(book => book.Table)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .OrderByDescending(book => book.Status[0])
                                        .ToList();
        }
        public List<Bookingtable> getAll()
        {
            return context.Bookingtables.Include(book => book.Table)
                                        .ToList();
        }

        public Bookingtable findByID(int id)
        {
            return context.Bookingtables
                          .Where(b => b.Id == id)
                          .FirstOrDefault();
        }

        public List<Bookingtable> findByName(string name)
        {
            if (name == null) return getAll();
            return context.Bookingtables
                          .Where(book => book.Name.ToLower().Contains(name.ToLower()))
                          .ToList();
        }

        public Bookingtable changeStatus(Bookingtable booking)
        {
            booking.Status = new byte[] { 0 };
            return booking;
        }

        public Boolean isBooked(string email, string phone)
        {
            bookingDBContext context = new bookingDBContext();
            var booked = context.Bookingtables.Where(book => book.Email == email && book.Phone == phone).FirstOrDefault();
            return booked == null;
        }
    }
}
