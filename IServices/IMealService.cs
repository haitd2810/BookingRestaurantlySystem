using booking.Models;
using ZstdSharp.Unsafe;

namespace booking.IServices
{
    public interface IMealService
    {
        public List<Meal> GetMeal();
        public Meal FindByID(int id);
        Task<string> PathToSave(IFormFile img, string path_save_feedback, string default_img);
        public Boolean UpdateMeal(Meal meal);
        public Meal SetValue(Meal meal, string meal_name, string intro, float price, int meal_cate, string path);

        public Boolean AddMeal(Meal meal);
    }
}
