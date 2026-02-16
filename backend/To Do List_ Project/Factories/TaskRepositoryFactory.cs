using todo.Enums;
using todo.Exceptions;
using todo.Factories.Interfaces;
using todo.Repositories.Interfaces;
using todo.Repositories.SQLRepositories;
using todo.Repositories.XMLRepositories;

namespace todo.Factories
{
    public class TaskRepositoryFactory : ITaskRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public TaskRepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ITaskRepository Get(int storageTypeId)
        {
            if (!Enum.IsDefined(typeof(StorageType), storageTypeId))
                throw new ValidationException($"Unknown storage type id: '{storageTypeId}'. Supported values: 1 (sql), 2 (xml).");

            var storageType = (StorageType)storageTypeId;

            return storageType switch
            {
                StorageType.sql => _serviceProvider.GetRequiredService<SqlTaskRepository>(),
                StorageType.xml => _serviceProvider.GetRequiredService<XmlTaskRepository>(),
                _ => throw new OperationFailedException("Unsupported storage type.")
            };
        }
    }
}
