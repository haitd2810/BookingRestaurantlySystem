
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class FeedbackService : IFeedbackService
    {
        public bool isTrueIMG(IFormFile img)
        {
            if (img != null && img.Length > 0 && IsImage(img))
            {
                return true;
            }
            return false;
        }

        private bool IsImage(IFormFile file)
        {
            string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension)) return false;

            if (!file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))  return false;

            return true;
        }

        public async Task saveFile(IFormFile img, string path)
        {
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), path);
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            var filePath = Path.Combine(uploads, img.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await img.CopyToAsync(fileStream);
            }
        }

        public Feedback setValue(string name, string jobs, string feedback, DateTime create, DateTime update, byte[] status, string img)
        {
            return new Feedback()
            {
                Name = name,
                Jobs = jobs,
                Detail = feedback,
                CreateDate = create,
                UpdateDate = update,
                Status = status,
                Img = img
            };
        }

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
    }
}
