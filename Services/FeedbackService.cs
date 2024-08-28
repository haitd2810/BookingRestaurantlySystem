
using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class FeedbackService : IFeedbackService
    {
        public bool IsTrueIMG(IFormFile img) => FeedbackDAO.Instance.isTrueIMG(img);

        public bool IsImage(IFormFile file) => FeedbackDAO.Instance.IsImage(file);

        public async Task SaveFile(IFormFile img, string path) => FeedbackDAO.Instance.saveFile(img, path);

        public Feedback SetValue(string name, string jobs, string feedback, DateTime create, DateTime update, byte[] status, string img)
         => FeedbackDAO.Instance.setValue(name, jobs, feedback, create, update, status, img);

        public async Task<string> PathToSave(IFormFile img, string path_save_feedback, string default_img)
        {
            if (IsTrueIMG(img))
            {
                string path = path_save_feedback;
                await SaveFile(img, path);
                return "/assets/img/uploads/" + img.FileName;
            }
            return default_img;
        }

        public bool AddFeedback(Feedback feedback) => FeedbackDAO.Instance.addFeedback(feedback);

        public List<Feedback> GetFeedback() => FeedbackDAO.Instance.getFeedback();
    }
}
