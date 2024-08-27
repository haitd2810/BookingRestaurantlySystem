using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class CategoryMealService : ICategoryMealService
    {
        public List<Categorymeal> GetCate() => CategoryMealDAO.Instance.GetCate();
    }
}
