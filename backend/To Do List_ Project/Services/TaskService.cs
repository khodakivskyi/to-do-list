using todo.Enums;
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

        public async Task<TaskModel> GetTaskByIdAsync(int id, int storageTypeId)
        {
            if (id <= 0)
                throw new ValidationException("Task ID must be greater than zero.");

            var repository = _factory.Get(storageTypeId);
            var task = await repository.GetTaskByIdAsync(id);

            if (task == null)
                throw new NotFoundException("Task");

            return task;
        }

        public async Task<TaskModel> AddTaskAsync(TaskModel task, int storageTypeId)
        {
            if (task == null)
                throw new ValidationException("Task cannot be null.");

            if (string.IsNullOrWhiteSpace(task.Text))
                throw new ValidationException("Task text is required.");

            if (task.Text.Length > 100)
                throw new ValidationException("Task text cannot exceed 100 characters.");

            if (task.Due_Date.HasValue && task.Due_Date.Value < DateTime.UtcNow)
                throw new ValidationException("Due date cannot be in the past.");

            var repository = _factory.Get(storageTypeId);
            var createdTask = await repository.AddTaskAsync(task);

            if (createdTask == null)
                throw new OperationFailedException("CreateTask");

            return createdTask;
        }

        public async Task<bool> MarkTaskAsCompleteAsync(int id, int storageTypeId)
        {
            if (id <= 0)
                throw new ValidationException("Task ID must be greater than zero.");

            var task = await GetTaskByIdAsync(id, storageTypeId);

            if (task.Is_Completed)
                throw new ValidationException("Task is already completed.");

            task.Is_Completed = true;
            task.Completed_At = DateTime.UtcNow;

            var repository = _factory.Get(storageTypeId);
            return await repository.MarkTaskAsCompleteAsync(task);
        }

        public async Task ClearTasksAsync(int storageTypeId)
        {
            var repository = _factory.Get(storageTypeId);
            await repository.ClearTasksAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasksAsync(int storageTypeId)
        {
            var repository = _factory.Get(storageTypeId);
            return await repository.GetAllTasksAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetTasksByCompletionStatusAsync(int statusTypeId, int storageTypeId)
        {
            var repository = _factory.Get(statusTypeId);

            if (!Enum.IsDefined(typeof(StatusType), statusTypeId))
                throw new ValidationException($"Unknown status type id: '{statusTypeId}'. Supported values: 1 (sql), 2 (xml).");

            var statusType = (StatusType)statusTypeId;

            IEnumerable<TaskModel> tasks = statusType switch
            {
                StatusType.active => await repository.GetTasksByCompletionStatusAsync(false),
                StatusType.completed => await repository.GetTasksByCompletionStatusAsync(true),
                _ => throw new OperationFailedException("Unsupported status type.")
            };

            return tasks;
        }
    }
}
