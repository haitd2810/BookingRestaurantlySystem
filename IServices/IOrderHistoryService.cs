using booking.Models;
using booking.Services;

namespace booking.IServices
{
    public interface IOrderHistoryService
    {
        public Orderhistory setDefault();
        public Boolean deleteByZeroMeal(List<Ordertable> order_list, Ordertable order, int odHistory, int tableID);
        public Orderhistory updateByPayMeal(Orderhistory order_history, float total);
        public int getIDOrderHistory(int ExistID, int newID);
        public List<Total_Statistics> getTotalStatistic(List<Orderhistory> order_his_list);
    }
}
