using System;
using System.Collections.Generic;

namespace booking.Models
{
    public partial class Orderhistory
    {
        public Orderhistory()
        {
            Ordertables = new HashSet<Ordertable>();
        }

        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public float? TotalPrice { get; set; }
        public byte[]? Payed { get; set; }
        public byte[]? Status { get; set; }

        public virtual ICollection<Ordertable> Ordertables { get; set; }
    }
}
