using booking.IServices;
using booking.Models;
using Humanizer;

namespace booking.Services
{
    public class OrderService : IOrderService
    {
        public Ordertable changeByOrderMeal(Ordertable order, int quantity, float price)
        {
            order.Quantity += quantity;
            order.Price += price;
            return order;
        }

        public Boolean checkOrderMeal(List<Ordertable> order_list)
        {
            if (order_list.Count == 1 && order_list[0].Quantity == 1)
            {
                return true;
            }
            else return false;
        }

        public Ordertable setOrder(int mealID, int quantity, float price, int tableID, 
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
