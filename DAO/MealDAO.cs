using booking.Models;
using Microsoft.EntityFrameworkCore;

namespace booking.DAO
{
    public class MealDAO
    {
        private static readonly object instaceLock = new object();
        private static MealDAO instance = null;
        private readonly bookingDBContext context = new bookingDBContext();
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
            
            return context.Meals.Where(meal => meal.Status[0] == 1).Include(cate => cate.Cate).ToList();
        }

        public Meal FindByID(int id) {
            return context.Meals
                          .Where(meal => meal.Id == id && meal.Status[0] == 1)
                          .Include(meal => meal.Cate)
                          .FirstOrDefault() ?? new Meal();
        }

        public Boolean Update(Meal meal)
        {
            if(meal == null) return false;
            if(FindByID(meal.Id).Id != 0)
            {
                context.Meals.Update(meal);
                int result = context.SaveChanges();
                if (result == 0)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean Add(Meal meal) {
            if (meal == null) return false;
            context.Meals.Add(meal);
            int result = context.SaveChanges();
            if (result == 0)
            {
                return false;
            }
            return true;
        }
    }
}
