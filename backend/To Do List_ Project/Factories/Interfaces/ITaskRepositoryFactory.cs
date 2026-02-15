using todo.Repositories.Interfaces;

namespace todo.Factories.Interfaces
{
    public interface ITaskRepositoryFactory
    {
        ITaskRepository Get(string type);
    }
}
