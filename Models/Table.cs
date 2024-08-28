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
    }
}
