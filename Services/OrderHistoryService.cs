using booking.IServices;
using booking.Models;
using System.Reflection.Metadata.Ecma335;

namespace booking.Services
{
    public class OrderHistoryService : IOrderHistoryService
    {
        private readonly IOrderService order_service = new OrderService();
        private readonly Orderhistory object_odhistory = new Orderhistory();
        private readonly Table table_object = new Table();

        public bool deleteByZeroMeal(List<Ordertable> order_list, Ordertable order, int odHistory, int tableID)
        {

            if (order_service.checkOrderMeal(order_list))
            {
                order.DeleteOrderTable();
                object_odhistory.deleteByID(odHistory);

                //update status table
                table_object.markTableAsOrdered(tableID, false);
                return true;
            }
            else { order.DeleteOrderTable(); return true; }
        }

        public Orderhistory setDefault()
        {
            return new Orderhistory()
            {
                Fullname = null,
                Email = null,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                TotalPrice = 0,
                Payed = new byte[] { 0 },
                Status = new byte[] { 1 }
            };
        }
    }
}
