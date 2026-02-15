using todo.Models;

namespace todo.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TaskModel> GetTaskByIdAsync(int id, string storageType);
        Task<TaskModel> AddTaskAsync(TaskModel task, string storageType);
        Task<bool> MarkTaskAsCompleteAsync(int id, string storageType);
        Task<IEnumerable<TaskModel>> GetAllTasksAsync(string storageType);
        Task<IEnumerable<TaskModel>> GetTasksByCompletionStatusAsync(bool isCompleted, string storageType);
        Task ClearTasksAsync(string storageType);
    }
}
