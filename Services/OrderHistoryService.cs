using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class OrderHistoryService : IOrderHistoryService
    {
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
