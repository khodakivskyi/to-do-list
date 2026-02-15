using todo.Models;

namespace todo.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        void AddDefaultCategories();
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
