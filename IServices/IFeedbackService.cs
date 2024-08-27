using booking.Models;

namespace booking.IServices
{
    public interface IFeedbackService
    {
        Boolean AddFeedback(Feedback feedback);
        List<Feedback> GetFeedback();
        bool IsTrueIMG(IFormFile img);
        Task<string> PathToSave(IFormFile img, string path_save_feedback, string default_img);
        Feedback SetValue(string name, string jobs, string feedback, DateTime create, DateTime update, byte[] status, string img);
    }
}
