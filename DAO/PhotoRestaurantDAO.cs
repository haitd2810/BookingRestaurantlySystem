using booking.Models;

namespace booking.DAO
{
    public class PhotoRestaurantDAO
    {
        private static readonly object instaceLock = new object();
        private static PhotoRestaurantDAO instance = null;

        public static PhotoRestaurantDAO Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new PhotoRestaurantDAO();
                    return instance;
                }
            }
        }

        public List<Photorestaurant> Getphoto()
        {
            bookingDBContext context = new bookingDBContext();
            return context.Photorestaurants.Where(img => img.Status[0] == 1).ToList();
        }
    }
}
