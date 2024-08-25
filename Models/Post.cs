using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace booking.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Brief { get; set; }
        public string? Detail { get; set; }
        public string? Img { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public byte[]? Status { get; set; }

        private static readonly object instaceLock = new object();
        private static Post instance = null;

        public static Post Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new Post();
                    return instance;
                }
            }
        }
        public List<Post> getPost()
        {
            bookingDBContext context = new bookingDBContext();
            return context.Posts.Where(post => post.Status[0] == 1).ToList();
        }
    }
}
