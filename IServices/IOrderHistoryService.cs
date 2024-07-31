using booking.Models;
using Mysqlx.Crud;

namespace booking.IServices
{
    public interface IOrderHistoryService
    {
        public Orderhistory setDefault();

        public Boolean deleteByZeroMeal(List<Ordertable> order_list, Ordertable order, int odHistory, int tableID);
        
    }
}
