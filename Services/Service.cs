using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class Service : IService
    {
        private readonly Bookingtable object_booking = new Bookingtable();
        public int getMaxPage(int pageSize)
        {
            int maxPage = (object_booking.getAll().Count) / pageSize;
            if (object_booking.getAll().Count % pageSize != 0) maxPage += 1;
            return maxPage;
        }

        public int getPageNumber(int pageNumber, int pageSize)
        {
            if (pageNumber <= 1) return 1;
            else if (pageNumber >= getMaxPage(pageSize)) return getMaxPage(pageSize);
            else return pageNumber;
        }
    }
}
