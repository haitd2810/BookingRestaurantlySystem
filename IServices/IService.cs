namespace booking.IServices
{
    public interface IService
    {
        int GetPageNumber(int pageNumber, int pageSize);
        int GetMaxPage(int pageSize);
    }
}
