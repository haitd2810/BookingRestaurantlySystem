using booking.Models;

namespace booking.IServices
{
    public interface IOrderService
    {
        public Boolean checkOrderMeal(List<Ordertable> order_list);
    }
}
