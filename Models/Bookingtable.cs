using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        public Boolean isBooked(string email, string phone)
        {
            bookingDBContext context = new bookingDBContext();
            var booked = context.Bookingtables.Where(book => book.Email == email && book.Phone == phone).FirstOrDefault();
            return booked == null;
        }
    }
}
