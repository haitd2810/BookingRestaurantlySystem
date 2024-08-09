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

        public Boolean updateBooking()
        {
            context.Bookingtables.Update(this);
            int row = context.SaveChanges();
            if(row <= 0)
            {
                return false;
            }
            return true;
        }

        public List<Bookingtable> getAll(int pageNumber, int pageSize)
        {
            return context.Bookingtables.Include(book => book.Table)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .OrderByDescending(book => book.DateOrder)
                                        .OrderByDescending(book => book.TimeOrder)
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
    }
}
