using todo.Exceptions;
using todo.Factories.Interfaces;
using todo.Models;
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
            if (id <= 0)
                throw new ValidationException("Task ID must be greater than zero.");

            var repository = _factory.Get(storageType);
            var task = await repository.GetTaskByIdAsync(id);

            if (task == null)
                throw new NotFoundException("Task");

            return task;
        }

        public async Task<TaskModel> AddTaskAsync(TaskModel task, string storageType)
        {
            if (task == null)
                throw new ValidationException("Task cannot be null.");

            if (string.IsNullOrWhiteSpace(task.Text))
                throw new ValidationException("Task text is required.");

            if (task.Text.Length > 100)
                throw new ValidationException("Task text cannot exceed 100 characters.");

            if (task.Due_Date.HasValue && task.Due_Date.Value < DateTime.UtcNow)
                throw new ValidationException("Due date cannot be in the past.");

            var repository = _factory.Get(storageType);
            var createdTask = await repository.AddTaskAsync(task);

            if (createdTask == null)
                throw new OperationFailedException("CreateTask");

            return createdTask;
        }

        public async Task<bool> MarkTaskAsCompleteAsync(int id, string storageType)
        {
            if (id <= 0)
                throw new ValidationException("Task ID must be greater than zero.");

            var task = await GetTaskByIdAsync(id, storageType);

            if (task.Is_Completed)
                throw new ValidationException("Task is already completed.");

            task.Is_Completed = true;
            task.Completed_At = DateTime.UtcNow;

            var repository = _factory.Get(storageType);
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
