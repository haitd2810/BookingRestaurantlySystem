using System;
using System.Collections.Generic;

namespace booking.Models
{
    public partial class Staff
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        /// <summary>
        /// foreign key
        /// </summary>
        public int? RoleId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte[]? Status { get; set; }

        public virtual Role? Role { get; set; }
    }
}
