﻿using System;
using System.Collections.Generic;

namespace booking.Models
{
    public partial class Meal
    {
        public Meal()
        {
            Ordertables = new HashSet<Ordertable>();
            SpecialmealMeals = new HashSet<Specialmeal>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Img { get; set; }
        public float? Price { get; set; }
        public string? Intro { get; set; }
        public int? CateId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte[]? Status { get; set; }

        public virtual Categorymeal? Cate { get; set; }
        public virtual Specialmeal? SpecialmealIdNavigation { get; set; }
        public virtual ICollection<Ordertable> Ordertables { get; set; }
        public virtual ICollection<Specialmeal> SpecialmealMeals { get; set; }
    }
}
