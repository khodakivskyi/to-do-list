using todo.Models;

namespace todo.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(string storageType);
    }
}
