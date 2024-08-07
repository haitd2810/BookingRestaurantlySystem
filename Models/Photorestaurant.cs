﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace booking.Models
{
    public partial class Photorestaurant
    {
        public int Id { get; set; }
        public string? Img { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte[]? Status { get; set; }
        public List<Photorestaurant> getphoto()
        {
            bookingDBContext context = new bookingDBContext();
            return context.Photorestaurants.Where(img => img.Status[0] == 1).ToList();
        }
    }
}
