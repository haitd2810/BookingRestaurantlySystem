using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class SpecialMealService : ISpecialMealService
    {
        public List<Specialmeal> getSepcialMeal() => SpecialMealDAO.Instance.getSepcialMeal();
    }
}
