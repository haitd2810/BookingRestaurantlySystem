using booking.DAO;
using booking.IServices;
using booking.Models;
using Microsoft.EntityFrameworkCore;

namespace booking.Services
{
    public class TableService : ITableService
    {
        public List<Table> GetTableList() => TableDAO.Instance.GetTableList();

        public void MarkTableAsOrdered(int tableID, bool ismark) => TableDAO.Instance.MarkTableAsOrdered(tableID, ismark);

        public bool UpdateTable(Table table) => TableDAO.Instance.UpdateTable(table);
    }
}
