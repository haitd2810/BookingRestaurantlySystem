using booking.Models;

namespace booking.DAO
{
    public class SettingDAO
    {
        private static readonly object instaceLock = new object();
        private static SettingDAO instance = null;

        public static SettingDAO Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new SettingDAO();
                    return instance;
                }
            }
        }

        public Setting getSetting()
        {
            bookingDBContext context = new bookingDBContext();
            return context.Settings.FirstOrDefault() ?? new Setting();
        }
    }
}
