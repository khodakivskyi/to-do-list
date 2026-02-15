using System.Threading.Tasks;
using todo.Factories.Interfaces;
using todo.Models;
using todo.Repositories.Interfaces;
using todo.Services.Interfaces;

namespace todo.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepositoryFactory _factory;
        public TaskService(ITaskRepositoryFactory factory)
        {
            _factory = factory;
        }

        public async Task<TaskModel> GetTaskByIdAsync(int id, string storageType)
        {
            var repository = _factory.Get(storageType);
            var task = await repository.GetTaskByIdAsync(id);

            if (task == null)
            {
                throw new KeyNotFoundException($"Task not found.");
            }

            return task;
        }

        public async Task<TaskModel> AddTaskAsync(TaskModel task, string storageType)
        {
            var repository = _factory.Get(storageType);
            var createdTask = await repository.AddTaskAsync(task);

            if (createdTask == null)
            {
                throw new Exception("Failed to create task.");
            }
            return createdTask;
        }
        public async Task<bool> MarkTaskAsCompleteAsync(int id, string storageType)
        {
            var repository = _factory.Get(storageType);
            var task = await GetTaskByIdAsync(id, storageType);
            task.Is_Completed = true;
            task.Completed_At = DateTime.UtcNow;

            return await repository.MarkTaskAsCompleteAsync(task);
        }

        public async Task ClearTasksAsync(string storageType)
        {
            var repository = _factory.Get(storageType);
            await repository.ClearTasksAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasksAsync(string storageType)
        {
            var repository = _factory.Get(storageType);
            return await repository.GetAllTasksAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetTasksByCompletionStatusAsync(bool isCompleted, string storageType)
        {
            var repository = _factory.Get(storageType);
            return await repository.GetTasksByCompletionStatusAsync(isCompleted);
        }
    }
}
