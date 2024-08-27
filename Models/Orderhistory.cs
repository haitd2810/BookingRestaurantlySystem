using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public string getDate()
        {
            return this.CreateDate.ToString().Split(' ')[0] ?? string.Empty;
        }
        public string getTime()
        {
            string full_time_start = this.CreateDate.ToString().Split(' ')[1];
            string[] sub_time_start = full_time_start.Split(":");

            string full_time_end = this.UpdateDate.ToString().Split(' ')[1];
            string[] sub_time_end = full_time_end.Split(":");
            return sub_time_start[0] + ":" + sub_time_start[1] + $" {this.CreateDate.ToString().Split(' ')[2]}"
                + " - "
                + sub_time_end[0] + ":" + sub_time_end[1] + $" {this.UpdateDate.ToString().Split(' ')[2]}";
        }

    }
}
