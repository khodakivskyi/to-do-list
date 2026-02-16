using todo.Repositories.Interfaces;

namespace todo.Factories.Interfaces
{
    public interface ICategoryRepositoryFactory
    {
        ICategoryRepository Get(int storageTypeId);
    }
}
