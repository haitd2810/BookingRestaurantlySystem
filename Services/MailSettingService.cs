using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class MailSettingService : IMailSettingService
    {
        public Mailsetting getMailSetting() => MailSettingDAO.Instance.getMailSetting();
    }
}
