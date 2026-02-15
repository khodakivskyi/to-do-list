using todo.Models;

namespace todo.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddDefaultCategoriesAsync(List<string> defaultCategories);
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
