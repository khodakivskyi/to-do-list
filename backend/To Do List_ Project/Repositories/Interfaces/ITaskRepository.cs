using todo.Models;

namespace todo.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskModel?> GetTaskByIdAsync(int id);
        Task<TaskModel?> AddTaskAsync(TaskModel task);
        Task<bool> MarkTaskAsCompleteAsync(TaskModel task);
        Task<IEnumerable<TaskModel>> GetAllTasksAsync();
        Task<IEnumerable<TaskModel>> GetTasksByCompletionStatusAsync(bool isCompleted);
        Task ClearTasksAsync();
    }
}
