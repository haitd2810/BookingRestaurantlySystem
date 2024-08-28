using booking.Models;
using booking.Services;

namespace booking.IServices
{
    public interface IOrderHistoryService
    {
        public List<Orderhistory> GetAll();
        public Orderhistory FindbyID(int id);
        public Orderhistory SetDefault();
        public Orderhistory UpdateByPayMeal(Orderhistory order_history, float total);
        public int GetIDOrderHistory(int ExistID, int newID);
        public List<Total_Statistics> GetTotalStatistic(List<Orderhistory> order_his_list, DateTime? start, DateTime? end);
        public List<Orderhistory> GetListByDate(List<Orderhistory> order_his_list, DateTime? start, DateTime? end);
        public List<Total_Statistics> GetCompletebyDate(List<Total_Statistics> list, DateTime? start, DateTime? end);
        public bool DeleteByZeroMeal(List<Ordertable> order_list, Ordertable order, int odHistory, int tableID);
        public float GetTotal(Orderhistory order_history);
        public Boolean UpdateOrderHistory(Orderhistory orderH);
    }
}
