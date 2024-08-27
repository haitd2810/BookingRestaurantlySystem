using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace booking.Models
{
    public partial class Specialmeal
    {
        public int Id { get; set; }
        public int? MealId { get; set; }
        public string? Detail { get; set; }
        public string? Img { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte[]? Status { get; set; }

        public virtual Meal IdNavigation { get; set; } = null!;
        public virtual Meal? meal { get; set; }

    }
}
