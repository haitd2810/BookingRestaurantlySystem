using booking.Models;

namespace booking.DAO
{
    public class CategoryMealDAO
    {
        private static readonly object instaceLock = new object();
        private static CategoryMealDAO instance = null;
        public static CategoryMealDAO Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new CategoryMealDAO();
                    return instance;
                }
            }
        }

        public List<Categorymeal> GetCate()
        {
            bookingDBContext context = new bookingDBContext();
            return context.Categorymeals.Where(cate => cate.Status[0] == 1).ToList();
        }
    }
}
