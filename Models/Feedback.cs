using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace booking.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Jobs { get; set; }
        public string? Detail { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte[]? Status { get; set; }
        public string? Img { get; set; }

    }
}
