using Microsoft.EntityFrameworkCore;
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
        private readonly bookingDBContext context = new bookingDBContext();
        public Boolean AddOrderTable()
        {
            context.Ordertables.Add(this);
            int result = context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public Boolean UpdateOrderTable()
        {
            context.Ordertables.Update(this);
            int result = context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public Boolean DeleteOrderTable()
        {
            if (this != null)
            {
                this.Quantity -= 1;
                this.Price -= this.Meal.Price;
                if (this.Quantity <= 0)
                {
                    context.Remove(this);
                    context.SaveChanges();
                }
                else this.UpdateOrderTable();
            }
            return true;
        }

        public Ordertable FindOrder(int tableID, int mealID)
        {
            return context.Ordertables.
                   Where(od => od.TableId == tableID && od.MealId == mealID)
                   .Include(meal => meal.Meal)
                   .Include(table => table.Table)
                   .Include(odHis => odHis.OdHistory)
                   .FirstOrDefault();
        }

        public Boolean updatePaymeal(int orderHistoryID, int tableID)
        {
            List<Ordertable> order = context.Ordertables.
                   Where(od => od.TableId == tableID && od.OdHistoryId == orderHistoryID)
                   .ToList();
            foreach (Ordertable item in order)
            {
                item.Status = new byte[] { 0 };
                item.UpdateDate = DateTime.Now;
                item.UpdateOrderTable();
            }
            return true;
        }
    }
}
