using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class Service : IService
    {
        private readonly Bookingtable object_booking = new Bookingtable();
        public int GetMaxPage(int pageSize)
        {
            int maxPage = (BookingTableDAO.Instance.GetAll().Count) / pageSize;
            if (BookingTableDAO.Instance.GetAll().Count % pageSize != 0) maxPage += 1;
            return maxPage;
        }

        public int GetPageNumber(int pageNumber, int pageSize)
        {
            if (pageNumber <= 1) return 1;
            else if (pageNumber >= GetMaxPage(pageSize)) return GetMaxPage(pageSize);
            else return pageNumber;
        }
    }
}
