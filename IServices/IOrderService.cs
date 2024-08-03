using booking.Models;

namespace booking.IServices
{
    public interface IOrderService
    {
        public Boolean checkOrderMeal(List<Ordertable> order_list);
        public Ordertable changeByOrderMeal(Ordertable order, int quantity, float price);
        public Ordertable setOrder(int mealID, int quantity, float price, int tableID, DateTime createDate, DateTime updateDate, byte[] status, int orderHistoryID);
    }
}
