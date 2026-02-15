using todo.Factories.Interfaces;
using todo.Models;
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

        public async Task AddDefaultCategoriesAsync(List<string> defaultCategories)
        {
            var sqlRepository = _factory.Get("sql");
            var xmlRepository = _factory.Get("xml");

            var sqlTask = sqlRepository.AddDefaultCategoriesAsync(defaultCategories);
            var xmlTask = xmlRepository.AddDefaultCategoriesAsync(defaultCategories);

            await Task.WhenAll(sqlTask, xmlTask);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(string storageType)
        {
            var repository = _factory.Get(storageType);
            return await repository.GetCategoriesAsync();
        }
    }
} 
