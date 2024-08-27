using booking.Models;

namespace booking.DAO
{
    public class MailSettingDAO
    {
        private static readonly object instaceLock = new object();
        private static MailSettingDAO instance = null;

        public static MailSettingDAO Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new MailSettingDAO();
                    return instance;
                }
            }
        }

        public Mailsetting getMailSetting()
        {
            bookingDBContext context = new bookingDBContext();
            return context.Mailsettings.FirstOrDefault() ?? (new Mailsetting());
        }
    }
}
