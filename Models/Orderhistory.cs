﻿using Microsoft.EntityFrameworkCore;
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
        private readonly bookingDBContext context = new bookingDBContext();
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
    }
}
