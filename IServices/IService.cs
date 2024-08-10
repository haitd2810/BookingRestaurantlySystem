namespace booking.IServices
{
    public interface IService
    {
        int getPageNumber(int pageNumber, int pageSize);
        int getMaxPage(int pageSize);
    }
}
