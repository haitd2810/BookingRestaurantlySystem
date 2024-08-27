using booking.Models;

namespace booking.DAO
{
    public class PostDAO
    {
        private static readonly object instaceLock = new object();
        private static PostDAO instance = null;

        public static PostDAO Instance
        {
            get
            {
                lock (instaceLock)
                {
                    instance ??= new PostDAO();
                    return instance;
                }
            }
        }
        public List<Post> GetPost()
        {
            bookingDBContext context = new bookingDBContext();
            return context.Posts.Where(post => post.Status[0] == 1).ToList();
        }
    }
}
