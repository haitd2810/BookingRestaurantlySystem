using booking.Models;
using Microsoft.EntityFrameworkCore;

namespace booking.DAO
{
    public class SpecialMealDAO
    {
        private static readonly object instaceLock = new object();
        private static SpecialMealDAO instance = null;

        public static SpecialMealDAO Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new SpecialMealDAO();
                    return instance;
                }
            }
        }
        public List<Specialmeal> GetSepcialMeal()
        {
            bookingDBContext context = new bookingDBContext();
            return context.Specialmeals.Where(spec => spec.Status[0] == 1).Include(spec => spec.Meal).ToList();
        }
    }
}
