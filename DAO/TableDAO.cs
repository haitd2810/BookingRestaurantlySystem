using booking.Models;
using Microsoft.EntityFrameworkCore;

namespace booking.DAO
{
    public class TableDAO
    {
        private readonly bookingDBContext context = new bookingDBContext();
        private static readonly object instaceLock = new object();
        private static TableDAO instance = null;

        public static TableDAO Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new TableDAO();
                    return instance;
                }
            }
        }
        
        public Boolean UpdateTable(Table table)
        {
            try
            {
                var entry = context.Entry(table);
                if (entry.State == EntityState.Detached)
                {
                    context.Tables.Attach(table);
                    entry.State = EntityState.Modified;
                }
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = entry.CurrentValues;
                var databaseEntry = entry.GetDatabaseValues();

                if (databaseEntry == null)
                {
                    throw new Exception("The entity being updated has been deleted by another user.", ex);
                }
                else
                {
                    var databaseValues = databaseEntry.ToObject();
                    entry.OriginalValues.SetValues(databaseValues);

                    try
                    {
                        context.SaveChanges();
                        return true;
                    }
                    catch (DbUpdateConcurrencyException retryEx)
                    {
                        throw new Exception("Concurrency conflict could not be resolved.", retryEx);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the table.", ex);
            }
        }

        public void MarkTableAsOrdered(int tableID, Boolean ismark)
        {
            Table table = context.Tables.Where(tb => tb.Id == tableID).FirstOrDefault();
            if (table != null)
            {
                if (ismark) table.Ordered = new byte[] { 1 };
                else table.Ordered = new byte[] { 0 };
                UpdateTable(table);
            }
        }

        public List<Table> GetTableList()
        {
            return context.Tables
                          .Where(table => table.Status[0] == 1)
                          .Include(t => t.TypeTable)
                          .ToList();
        }

        public Table FindByID(int id)
        {
            return context.Tables
                          .Where(t => t.Id == id)
                          .Where(table => table.Status[0] == 1)
                          .Include(t => t.TypeTable)
                          .FirstOrDefault() ?? new Table();
        }
    }
}
