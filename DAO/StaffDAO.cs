using booking.Models;
using Microsoft.EntityFrameworkCore;

namespace booking.DAO
{
    public class StaffDAO
    {
        private static readonly object instaceLock = new object();
        private static StaffDAO instance = null;

        public static StaffDAO Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new StaffDAO();
                    return instance;
                }
            }
        }
        public Boolean IsStaff(string username, string password)
        {
            bookingDBContext context = new bookingDBContext();
            Staff staff = context.Staff.Where(staff => staff.Username == username && staff.Password == password).FirstOrDefault();
            return staff != null;
        }
        public string setMessageLogin(string username, string password)
        {
            if (IsStaff(username, password))
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
