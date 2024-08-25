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

        private static readonly object instaceLock = new object();
        private static Feedback instance = null;

        public static Feedback Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new Feedback();
                    return instance;
                }
            }
        }

        public Boolean addFeedback(Feedback feedback)
        {
            if (feedback != null)
            {
                bookingDBContext context = new bookingDBContext();
                context.Feedbacks.Add(feedback);
                int result = context.SaveChanges();
                if (result != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Feedback> getFeedback()
        {
            bookingDBContext context = new bookingDBContext();
            return context.Feedbacks.Where(fb => fb.Status[0] == 1).ToList();
        }
    }
}
