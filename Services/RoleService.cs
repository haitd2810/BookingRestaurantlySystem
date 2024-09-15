using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class RoleService : IRoleService
    {
        public List<Role> getRoleList() => RoleDAO.Instance.getAll();
    }
}
