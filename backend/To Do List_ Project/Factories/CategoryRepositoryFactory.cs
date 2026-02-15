using todo.Exceptions;
using todo.Factories.Interfaces;
using todo.Repositories.Interfaces;
using todo.Repositories.SQLRepositories;
using todo.Repositories.XMLRepositories;

namespace todo.Factories
{
    public class CategoryRepositoryFactory : ICategoryRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CategoryRepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICategoryRepository Get(string type)
        {
            return type switch
            {
                "sql" => _serviceProvider.GetRequiredService<SqlCategoryRepository>(),
                "xml" => _serviceProvider.GetRequiredService<XmlCategoryRepository>(),
                _ => throw new ValidationException($"Unknown storage type: '{type}'. Supported types: 'sql', 'xml'.")
            };
        }
    }
}
