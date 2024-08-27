using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class MealService : IMealService
    {
        public List<Meal> GetMeal() => MealDAO.Instance.GetMeal();
    }
}
