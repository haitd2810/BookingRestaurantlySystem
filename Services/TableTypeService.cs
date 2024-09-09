using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
	public class TableTypeService : ITableTypeService
	{
		public List<Tabletype> getList() => TableTypeDAO.Instance.getList();
	}
}
