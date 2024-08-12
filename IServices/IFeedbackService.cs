using booking.Models;

namespace booking.IServices
{
    public interface IFeedbackService
    {
        bool isTrueIMG(IFormFile img);
        Feedback setValue(string name, string jobs, string feedback, DateTime create, DateTime update, byte[] status, string img);
    }
}
