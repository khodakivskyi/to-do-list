using todo.Exceptions;
using todo.Factories.Interfaces;
using todo.Models;
using todo.Services.Interfaces;

namespace todo.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepositoryFactory _factory;

        public CategoryService(ICategoryRepositoryFactory factory)
        {
            _factory = factory;
        }

        public async Task AddDefaultCategoriesAsync(List<string> defaultCategories)
        {
            if (defaultCategories == null || defaultCategories.Count == 0)
                throw new ValidationException("Default categories list cannot be empty.");

            if (defaultCategories.Any(string.IsNullOrWhiteSpace))
                throw new ValidationException("Category name cannot be empty.");

            var sqlRepository = _factory.Get(1);
            var xmlRepository = _factory.Get(2);

            var sqlTask = sqlRepository.AddDefaultCategoriesAsync(defaultCategories);
            var xmlTask = xmlRepository.AddDefaultCategoriesAsync(defaultCategories);

            await Task.WhenAll(sqlTask, xmlTask);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(int storageTypeId)
        {
            var repository = _factory.Get(storageTypeId);
            return await repository.GetCategoriesAsync();
        }
    }
} 
