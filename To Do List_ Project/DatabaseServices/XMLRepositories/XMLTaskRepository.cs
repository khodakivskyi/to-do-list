using Microsoft.Data.SqlClient;
using System.Xml.Serialization;
using To_Do_List__Project.DatabaseServices.Interfaces;
using To_Do_List__Project.Models;

namespace To_Do_List__Project.Database.XMLRepositories
{
    public class XMLTaskRepository : ITaskService
    {
        private readonly string _filePath;

        public XMLTaskRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                SaveTasks(new List<TaskModel>());
            }
        }
        public void AddTask(TaskModel task)
        {
            var tasks = GetAllTasks();

            task.Id = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;

            tasks.Add(task);

            SaveTasks(tasks);
        }

        public List<TaskModel> GetAllTasks()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<TaskModel>();

                var serializer = new XmlSerializer(typeof(List<TaskModel>));
                using var stream = new FileStream(_filePath, FileMode.Open);
                return (List<TaskModel>)serializer.Deserialize(stream)! ?? new List<TaskModel>();
            }
            catch
            {
                return new List<TaskModel>();
            }
        }
        public List<TaskModel> GetActiveTasks()
        {
            var tasks = GetAllTasks();
            var activeTasks = tasks.Where(t => t.Is_Completed == false).ToList();
            return activeTasks;
        }
        public List<TaskModel> GetCompletedTasks()
        {
            var tasks = GetAllTasks();
            var activeTasks = tasks.Where(t => t.Is_Completed == true).ToList();
            return activeTasks;
        }
        public TaskModel? GetTaskById(int taskId)
        {
            var tasks = GetAllTasks();
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            return task;
        }
        public void UpdateTask(TaskModel task)
        {
            var tasks = GetAllTasks();

            var index = tasks.FindIndex(t => t.Id == task.Id);
            if (index != -1)
            {
                tasks[index] = task;
                SaveTasks(tasks);
            }
        }
        private void SaveTasks(List<TaskModel> tasks)
        {
            var serializer = new XmlSerializer(typeof(List<TaskModel>));
            using var stream = new FileStream(_filePath, FileMode.Create);
            serializer.Serialize(stream, tasks);
        }
        public void ClearTasks()
        {
            SaveTasks(new List<TaskModel>());
        }
    }
}
