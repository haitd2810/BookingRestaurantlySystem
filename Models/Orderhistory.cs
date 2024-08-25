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
        private readonly bookingDBContext context = new bookingDBContext();

        private static readonly object instaceLock = new object();
        private static Orderhistory instance = null;

        public static Orderhistory Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new Orderhistory();
                    return instance;
                }
            }
        }

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

        public Boolean AddOderHistory()
        {
            context.Orderhistories.Add(this);
            int result = context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public Boolean UpdateOrderHistory()
        {
            context.Orderhistories.Update(this);
            int result = context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public Orderhistory findbyID(int id)
        {
            return context.Orderhistories.Where(oh => oh.Id == id)
                          .Include(oh => oh.Ordertables)
                          .FirstOrDefault();
        }

        public Boolean deleteByID(int id)
        {
            Orderhistory od_history = findbyID(id);
            if (od_history != null)
            {
                context.Remove(od_history);
                context.SaveChanges();
            }
            return true;
        }

        public float getTotal(Orderhistory order_history)
        {
            if (order_history != null)
            {
                float total_price = 0;
                foreach (var od in order_history.Ordertables)
                {
                    total_price += float.Parse(od.Price.ToString());
                }
                return total_price;
            }
            return 0;
        }

        public List<Orderhistory> getAll()
        {
            return context.Orderhistories
                          .Where(od => od.Status[0] == 1 && od.Payed[0] == 1)
                          .Include(od => od.Ordertables)
                          .ToList();
        }

    }
}
