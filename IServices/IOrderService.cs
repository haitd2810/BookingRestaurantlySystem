using booking.Models;

namespace booking.IServices
{
    public interface IOrderService
    {
        List<Ordertable> getOrderList(int id);
        Boolean AddOrderTable(Ordertable order);
        Boolean UpdateOrderTable(Ordertable order);
        Boolean DeleteOrderTable(Ordertable order);
        Ordertable FindOrder(int tableID, int mealID, int orderHistoryID);
        List<Ordertable> FindOrder(int tableID, int orderHistoryID);
        Boolean updatePaymeal(int orderHistoryID, int tableID);
        public Boolean checkOrderMeal(List<Ordertable> order_list);
        public Ordertable changeByOrderMeal(Ordertable order, int quantity, float price);
        public Ordertable setOrder(int mealID, int quantity, float price, int tableID, DateTime createDate, DateTime updateDate, byte[] status, int orderHistoryID);
    }
}
