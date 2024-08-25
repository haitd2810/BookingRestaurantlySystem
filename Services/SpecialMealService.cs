using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class SpecialMealService : ISpecialMealService
    {
        public List<Specialmeal> getSepcialMeal() => Specialmeal.Instance.getSepcialMeal();
    }
}
