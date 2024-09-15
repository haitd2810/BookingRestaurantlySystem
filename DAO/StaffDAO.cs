using booking.Controllers;
using booking.Models;
using Microsoft.EntityFrameworkCore;

namespace booking.DAO
{
    public class StaffDAO
    {
        private readonly bookingDBContext context = new bookingDBContext();
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
            
            Staff staff = context.Staff.Where(staff => staff.Username == username
                                                    && staff.Password == password && staff.Status[0]==1)
                                       .FirstOrDefault();
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

        public List<Staff> GetAll()
        {
            return context.Staff.Where(account => account.Status[0] == 1)
                                .Include(account => account.Role)
                                .ToList() ?? new List<Staff>();
        }

        public Staff FindByID(int id)
        {
            return context.Staff.Where(account => account.Id == id)
                                .Include(account => account.Role)
                                .FirstOrDefault() ?? new Staff();
        }

        public Boolean FindByUser(string username)
        {
            Staff staff = context.Staff.Where(st => st.Username.ToLower() == username.ToLower()).FirstOrDefault();
            if (staff == null || staff.Username.Length > 0) return true;
            return false;
        }

        public Boolean Update(Staff staff)
        {
            if (staff == null) return false;
            context.Staff.Update(staff);
            int row = context.SaveChanges();
            if (row <= 0) return false;
            return true;
        }

        public Boolean Add(Staff staff)
        {
            if (staff == null) return false;
            context.Staff.Add(staff);
            int row = context.SaveChanges();
            if(row <= 0) return false;
            return true;
        }
    }
}
