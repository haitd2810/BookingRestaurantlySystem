using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class OrderService : IOrderService
    {
        public Boolean checkOrderMeal(List<Ordertable> order_list)
        {
            if (order_list.Count == 1 && order_list[0].Quantity == 1)
            {
                return true;
            }
            else return false;
        }
    }
}
