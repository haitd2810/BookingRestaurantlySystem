using System;
using System.Collections.Generic;

namespace booking.Models
{
    public partial class Ordertable
    {
        public int Id { get; set; }
        public int? MealId { get; set; }
        public int? TableId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte[]? Status { get; set; }
        public int? OdHistoryId { get; set; }
        public float? Price { get; set; }

        public virtual Meal? Meal { get; set; }
        public virtual Orderhistory? OdHistory { get; set; }
        public virtual Table? Table { get; set; }
    }
}
