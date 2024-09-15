using booking.Models;

namespace booking.DAO
{
    public class RoleDAO
    {
        private readonly bookingDBContext context = new bookingDBContext();
        private static readonly object instaceLock = new object();
        private static RoleDAO instance = null;

        public static RoleDAO Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new RoleDAO();
                    return instance;
                }
            }
        }

        public List<Role> getAll()
        {
            return context.Roles.Where(role => role.Status[0] == 1)
                                .ToList();
        }
    }
}
