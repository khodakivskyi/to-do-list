using todo.Exceptions;
using todo.Repositories.SQLRepositories;
using todo.Repositories.Interfaces;
using todo.Factories.Interfaces;
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

        public ITaskRepository Get(string type)
        {
            return type switch
            {
                "sql" => _serviceProvider.GetRequiredService<SqlTaskRepository>(),
                "xml" => _serviceProvider.GetRequiredService<XmlTaskRepository>(),
                _ => throw new ValidationException($"Unknown storage type: '{type}'. Supported types: 'sql', 'xml'.")
            };
        }
    }
}
