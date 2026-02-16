using todo.Models;

namespace todo.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TaskModel> GetTaskByIdAsync(int id, int storageTypeId);
        Task<TaskModel> AddTaskAsync(TaskModel task, int storageTypeId);
        Task<bool> MarkTaskAsCompleteAsync(int id, int storageTypeId);
        Task<IEnumerable<TaskModel>> GetAllTasksAsync(int storageTypeId);
        Task<IEnumerable<TaskModel>> GetTasksByCompletionStatusAsync(int statusTypeId, int storageTypeId);
        Task ClearTasksAsync(int storageTypeId);
    }
}
