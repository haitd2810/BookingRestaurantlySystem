using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace booking.Models
{
    public partial class Table
    {
        public Table()
        {
            Bookingtables = new HashSet<Bookingtable>();
            Ordertables = new HashSet<Ordertable>();
        }

        public int Id { get; set; }
        public string? TableName { get; set; }
        public int? TypeTableId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte[]? Ordered { get; set; }
        public byte[]? Status { get; set; }

        public virtual Tabletype? TypeTable { get; set; }
        public virtual ICollection<Bookingtable> Bookingtables { get; set; }
        public virtual ICollection<Ordertable> Ordertables { get; set; }
        private readonly bookingDBContext context = new bookingDBContext();
        public Boolean UpdateTable()
        {
            try
            {
                var entry = context.Entry(this);
                if (entry.State == EntityState.Detached)
                {
                    context.Tables.Attach(this);
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

        public void markTableAsOrdered(int tableID, Boolean ismark)
        {
            Table table = context.Tables.Where(tb => tb.Id == tableID).FirstOrDefault();
            if (table != null)
            {
                if (ismark) table.Ordered = new byte[] { 1 };
                else table.Ordered = new byte[] { 0 };
                table.UpdateTable();
            }
        }

        public List<Table> getTableList()
        {
            return context.Tables
                          .Where(table => table.Status[0] == 1)
                          .Include(t => t.TypeTable)
                          .ToList();
        }

    }

}
