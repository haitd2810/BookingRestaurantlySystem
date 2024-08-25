using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class MealService : IMealService
    {
        public List<Meal> getMeal() => Meal.Instance.getMeal();
    }
}
