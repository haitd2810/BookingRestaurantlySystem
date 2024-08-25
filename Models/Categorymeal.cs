using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace booking.Models
{
    public partial class Categorymeal
    {
        public Categorymeal()
        {
            Meals = new HashSet<Meal>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte[]? Status { get; set; }

        public virtual ICollection<Meal> Meals { get; set; }
        public virtual ICollection<Ordertable> Ordertables { get; set; }
        private static readonly object instaceLock = new object();
        private static Categorymeal instance = null;
        public static Categorymeal Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new Categorymeal();
                    return instance;
                }
            }
        }

        public List<Categorymeal> getCate()
        {
            bookingDBContext context = new bookingDBContext();
            return context.Categorymeals.Where(cate => cate.Status[0] == 1).ToList();
        }
    }
}
