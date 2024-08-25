using booking.Models;

namespace booking.IServices
{
    public interface IFeedbackService
    {
        Boolean addFeedback(Feedback feedback);
        List<Feedback> getFeedback();
        bool isTrueIMG(IFormFile img);
        Task<string> pathToSave(IFormFile img, string path_save_feedback, string default_img);
        Feedback setValue(string name, string jobs, string feedback, DateTime create, DateTime update, byte[] status, string img);
    }
}
