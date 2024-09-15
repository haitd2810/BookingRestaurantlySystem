using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class StaffService : IStaffService
    {
        public bool Add(Staff staff) => StaffDAO.Instance.Add(staff);
        public Staff FindById(int id) => StaffDAO.Instance.FindByID(id);

        public bool FindByUser(string username) => StaffDAO.Instance.FindByUser(username);
        public List<Staff> GetStaffList() => StaffDAO.Instance.GetAll();

        public string setMessageLogin(string username, string password) => StaffDAO.Instance.setMessageLogin(username, password);

        public bool Update(Staff staff) => StaffDAO.Instance.Update(staff);
    }
}
