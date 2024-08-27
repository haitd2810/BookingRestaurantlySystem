using booking.Models;
using Microsoft.EntityFrameworkCore;

namespace booking.DAO
{
    public class OrderTableDAO
    {
        private readonly bookingDBContext context = new bookingDBContext();
        private static readonly object instaceLock = new object();
        private static OrderTableDAO instance = null;
        public static OrderTableDAO Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new OrderTableDAO();
                    return instance;
                }
            }
        }
        public List<Ordertable> GetOrderList(int id)
        {
            if (id == 0) return new List<Ordertable>();
            return context.Ordertables
                          .Where(od => od.Status[0] == 1 && od.TableId == id)
                          .Include(od => od.Meal.Cate)
                          .Include(od => od.Meal)
                          .ToList();
        }
        public Boolean AddOrderTable(Ordertable order)
        {
            context.Ordertables.Add(order);
            int result = context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public Boolean UpdateOrderTable(Ordertable order)
        {
            context.Ordertables.Update(order);
            int result = context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public Boolean DeleteOrderTable(Ordertable order)
        {
            if (order != null)
            {
                order.Quantity -= 1;
                order.Price -= order.Meal.Price;
                if (order.Quantity <= 0)
                {
                    context.Remove(order);
                    context.SaveChanges();
                }
                else { UpdateOrderTable(order); }
            }
            return true;
        }

        public Ordertable FindOrder(int tableID, int mealID, int orderHistoryID)
        {
            return context.Ordertables.
                    Where(od => od.TableId == tableID && od.MealId == mealID && od.OdHistoryId == orderHistoryID)
                    .Include(meal => meal.Meal)
                    .Include(table => table.Table)
                    .Include(odHis => odHis.OdHistory)
                    .FirstOrDefault();

        }
        public List<Ordertable> FindOrder(int tableID, int orderHistoryID)
        {
            return context.Ordertables.
                    Where(od => od.TableId == tableID && od.OdHistoryId == orderHistoryID)
                    .Include(meal => meal.Meal)
                    .Include(table => table.Table)
                    .Include(odHis => odHis.OdHistory)
                    .ToList();

        }
        public Boolean UpdatePaymeal(int orderHistoryID, int tableID)
        {
            List<Ordertable> order = context.Ordertables.
                   Where(od => od.TableId == tableID && od.OdHistoryId == orderHistoryID)
                   .ToList();
            foreach (Ordertable item in order)
            {
                item.Status = new byte[] { 0 };
                item.UpdateDate = DateTime.Now;
                UpdateOrderTable(item);
            }
            return true;
        }

        public Ordertable ChangeByOrderMeal(Ordertable order, int quantity, float price)
        {
            order.Quantity += quantity;
            order.Price += price;
            return order;
        }

        public Boolean CheckOrderMeal(List<Ordertable> order_list)
        {
            if (order_list.Count == 1 && order_list[0].Quantity == 1)
            {
                return true;
            }
            else return false;
        }

        public Ordertable SetOrder(int mealID, int quantity, float price, int tableID,
           DateTime createDate, DateTime updateDate, byte[] status, int orderHistoryID)
        {
            return new Ordertable()
            {
                MealId = mealID,
                Quantity = quantity,
                Price = price,
                TableId = tableID,
                CreateDate = createDate,
                UpdateDate = updateDate,
                Status = status,
                OdHistoryId = orderHistoryID
            };
        }
    }
}
