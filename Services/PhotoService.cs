using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class PhotoService : IPhotoService
    {
        public List<Photorestaurant> getphoto() => Photorestaurant.Instance.getphoto();
    }
}
