using booking.Models;
using Microsoft.EntityFrameworkCore;

namespace booking.DAO
{
    public class MealDAO
    {
        private static readonly object instaceLock = new object();
        private static MealDAO instance = null;
        public static MealDAO Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new MealDAO();
                    return instance;
                }
            }
        }

        public List<Meal> GetMeal()
        {
            bookingDBContext context = new bookingDBContext();
            return context.Meals.Where(meal => meal.Status[0] == 1).Include(cate => cate.Cate).ToList();
        }
    }
}
