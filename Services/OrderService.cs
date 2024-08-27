using booking.DAO;
using booking.IServices;
using booking.Models;
using Humanizer;

namespace booking.Services
{
    public class OrderService : IOrderService
    {
        public bool AddOrderTable(Ordertable order) => OrderTableDAO.Instance.AddOrderTable(order);

        public Ordertable changeByOrderMeal(Ordertable order, int quantity, float price)
            => OrderTableDAO.Instance.ChangeByOrderMeal(order,quantity, price);

        public Boolean checkOrderMeal(List<Ordertable> order_list) => OrderTableDAO.Instance.CheckOrderMeal(order_list);

        public bool DeleteOrderTable(Ordertable order) => OrderTableDAO.Instance.DeleteOrderTable(order);

        public Ordertable FindOrder(int tableID, int mealID, int orderHistoryID) => OrderTableDAO.Instance.FindOrder(tableID, mealID, orderHistoryID);

        public List<Ordertable> FindOrder(int tableID, int orderHistoryID) => OrderTableDAO.Instance.FindOrder(tableID, orderHistoryID);

        public List<Ordertable> getOrderList(int id) => OrderTableDAO.Instance.GetOrderList(id);

        public Ordertable setOrder(int mealID, int quantity, float price, int tableID, 
            DateTime createDate, DateTime updateDate, byte[] status, int orderHistoryID)
            => OrderTableDAO.Instance.SetOrder(mealID,quantity, price, tableID, createDate, updateDate, status, orderHistoryID);

        public bool UpdateOrderTable(Ordertable order) => OrderTableDAO.Instance.UpdateOrderTable(order);

        public bool updatePaymeal(int orderHistoryID, int tableID) => OrderTableDAO.Instance.UpdatePaymeal(orderHistoryID, tableID);
    }
}
