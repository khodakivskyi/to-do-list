using todo.Enums;
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

        public ICategoryRepository Get(int storageTypeId)
        {
            if (!Enum.IsDefined(typeof(StorageType), storageTypeId))
                throw new ValidationException($"Unknown storage type id: '{storageTypeId}'. Supported values: 1 (sql), 2 (xml).");

            var storageType = (StorageType)storageTypeId;

            return storageType switch
            {
                StorageType.sql => _serviceProvider.GetRequiredService<SqlCategoryRepository>(),
                StorageType.xml => _serviceProvider.GetRequiredService<XmlCategoryRepository>(),
                _ => throw new OperationFailedException("Unsupported storage type.")
            };
        }
    }
}
