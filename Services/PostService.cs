using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class PostService :IPostService
    {
        public List<Post> getPost() => Post.Instance.getPost();
    }
}
