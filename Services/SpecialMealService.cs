using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class SpecialMealService : ISpecialMealService
    {
        public List<Specialmeal> GetSepcialMeal() => SpecialMealDAO.Instance.GetSepcialMeal();
    }
}
