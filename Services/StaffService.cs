using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class StaffService : IStaffService
    {
        public string setMessageLogin(string username, string password) => StaffDAO.Instance.setMessageLogin(username, password);
    }
}
