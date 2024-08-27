
using booking.Models;

namespace booking.IServices
{
    public interface ITableService
    {
        public Boolean UpdateTable(Table table);
        public void MarkTableAsOrdered(int tableID, Boolean ismark);
        public List<Table> GetTableList();
    }
}
