using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class PostService :IPostService
    {
        public List<Post> getPost() => PostDAO.Instance.GetPost();
    }
}
