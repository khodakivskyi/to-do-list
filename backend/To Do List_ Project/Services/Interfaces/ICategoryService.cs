using todo.Models;

namespace todo.Services.Interfaces
{
    public interface ICategoryService
    {
        Task AddDefaultCategoriesAsync(List<string> defaultCategories);
        Task<IEnumerable<Category>> GetCategoriesAsync(string storageType);
    }
}
