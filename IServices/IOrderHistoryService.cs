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
        public List<Total_Statistics> getTotalStatistic(List<Orderhistory> order_his_list, DateTime? start, DateTime? end);

        public List<Orderhistory> getListByDate(List<Orderhistory> order_his_list, DateTime? start, DateTime? end);
        public List<Total_Statistics> getCompletebyDate(List<Total_Statistics> list, DateTime? start, DateTime? end);
    }
}
