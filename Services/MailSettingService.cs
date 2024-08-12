using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class MailSettingService : IMailSettingService
    {
        private readonly bookingDBContext context = new bookingDBContext();
    }
}
