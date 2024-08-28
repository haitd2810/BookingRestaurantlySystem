using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class SettingService : ISettingService
    {
        public Setting getSetting() => SettingDAO.Instance.getSetting();
    }
}
