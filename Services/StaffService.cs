using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class StaffService : IStaffService
    {
        public string setMessageLogin(string username, string password)
        {
            Staff staff_method = new Staff();
            if (staff_method.isStaff(username, password))
            {
                return "Login Success";
            }
            else
            {
                return "Failed Login";
            }
        }
    }
}
