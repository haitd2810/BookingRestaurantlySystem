using booking.Models;

namespace booking.IServices
{
    public interface IStaffService
    {
        string setMessageLogin(string username, string password);
        public List<Staff> GetStaffList();
        public Staff FindById(int id);
        public Boolean FindByUser(string username);
        public Boolean Update(Staff staff);
        public Boolean Add(Staff staff);
    }
}
