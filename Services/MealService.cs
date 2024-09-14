using booking.DAO;
using booking.IServices;
using booking.Models;

namespace booking.Services
{
    public class MealService : IMealService
    {
        public Meal FindByID(int id) => MealDAO.Instance.FindByID(id);

        public List<Meal> GetMeal() => MealDAO.Instance.GetMeal();
       
        public bool IsTrueIMG(IFormFile img)
        {
            if (img != null && img.Length > 0 && IsImage(img))
            {
                return true;
            }
            return false;
        }

        public bool IsImage(IFormFile file)
        {
            string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension)) return false;

            if (!file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase)) return false;

            return true;
        }
        public async Task SaveFile(IFormFile img, string path)
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
        public async Task<string> PathToSave(IFormFile img, string path_save_feedback, string default_img)
        {
            if (IsTrueIMG(img))
            {
                string path = path_save_feedback;
                await SaveFile(img, path);
                return "/assets/img/menu/" + img.FileName;
            }
            return default_img;
        }

        public bool UpdateMeal(Meal meal) => MealDAO.Instance.Update(meal);

        public Meal SetValue(Meal meal, string meal_name, string intro, float price, int meal_cate, string path)
        {
            meal.Img = path;
            meal.Name = meal_name;
            meal.Price = price;
            meal.Intro = intro;
            meal.CateId = meal_cate;
            return meal;
        }

        public bool AddMeal(Meal meal) => MealDAO.Instance.Add(meal);
    }
}
