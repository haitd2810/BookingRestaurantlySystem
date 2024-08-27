
using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class FeedbackService : IFeedbackService
    {
        public bool isTrueIMG(IFormFile img) => FeedbackDAO.Instance.isTrueIMG(img);

        private bool IsImage(IFormFile file) => FeedbackDAO.Instance.IsImage(file);

        public async Task saveFile(IFormFile img, string path) => FeedbackDAO.Instance.saveFile(img, path);

        public Feedback setValue(string name, string jobs, string feedback, DateTime create, DateTime update, byte[] status, string img)
         => FeedbackDAO.Instance.setValue(name, jobs, feedback, create, update, status, img);

        public async Task<string> pathToSave(IFormFile img, string path_save_feedback, string default_img)
        {
            if (isTrueIMG(img))
            {
                string path = path_save_feedback;
                await saveFile(img, path);
                return "/assets/img/uploads/" + img.FileName;
            }
            return default_img;
        }

        public bool addFeedback(Feedback feedback) => FeedbackDAO.Instance.addFeedback(feedback);

        public List<Feedback> getFeedback() => FeedbackDAO.Instance.getFeedback();
    }
}
