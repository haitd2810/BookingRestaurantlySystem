using System;
using System.Collections.Generic;

namespace booking.Models
{
    public partial class Tabletype
    {
        public Tabletype()
        {
            Tables = new HashSet<Table>();
        }

        public int Id { get; set; }
        public int? Seat { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte[]? Status { get; set; }

        public virtual ICollection<Table> Tables { get; set; }
    }
}
