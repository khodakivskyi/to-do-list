using todo.Factories.Interfaces;
using todo.Models;
using todo.Repositories.Interfaces;
using todo.Services.Interfaces;

namespace todo.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepositoryFactory _factory;
        public CategoryService(ICategoryRepositoryFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(string storageType)
        {
            var repository = _factory.Get(storageType);
            return await repository.GetCategoriesAsync();
        }
    }
}
