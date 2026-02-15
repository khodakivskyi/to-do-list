using System.Xml.Serialization;
using todo.Repositories.Interfaces;
using todo.Models;

namespace todo.Repositories.XMLRepositories
{
    public class XmlTaskRepository : ITaskRepository
    {
        private readonly string _filePath;

        public XmlTaskRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                SaveTasks(new List<TaskModel>());
            }
        }

        public async Task<TaskModel?> AddTaskAsync(TaskModel task)
        {
            var tasks = await LoadTasksAsync();

            task.Id = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
            tasks.Add(task);
            SaveTasks(tasks);

            return await GetTaskByIdAsync(task.Id);
        }

        public async Task<TaskModel?> GetTaskByIdAsync(int id)
        {
            var tasks = await LoadTasksAsync();
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public async Task<bool> MarkTaskAsCompleteAsync(TaskModel task)
        {
            var tasks = await LoadTasksAsync();

            var index = tasks.FindIndex(t => t.Id == task.Id);
            if (index != -1)
            {
                tasks[index] = task;
                SaveTasks(tasks);
            }

            return true;
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
        {
            return await LoadTasksAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetTasksByCompletionStatusAsync(bool isCompleted)
        {
            var tasks = await LoadTasksAsync();
            return tasks.Where(t => t.Is_Completed == isCompleted).ToList();
        }

        public async Task ClearTasksAsync()
        {
            using var stream = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
            var serializer = new XmlSerializer(typeof(List<TaskModel>));
            await Task.Run(() => serializer.Serialize(stream, new List<TaskModel>()));
        }

        private Task<List<TaskModel>> LoadTasksAsync()
        {
            if (!File.Exists(_filePath))
                return Task.FromResult(new List<TaskModel>());

            var serializer = new XmlSerializer(typeof(List<TaskModel>));
            using var stream = new FileStream(_filePath, FileMode.Open);
            var result = (List<TaskModel>)serializer.Deserialize(stream)! ?? new List<TaskModel>();
            return Task.FromResult(result);
        }

        private void SaveTasks(List<TaskModel> tasks)
        {
            var serializer = new XmlSerializer(typeof(List<TaskModel>));
            using var stream = new FileStream(_filePath, FileMode.Create);
            serializer.Serialize(stream, tasks);
        }
    }
}
