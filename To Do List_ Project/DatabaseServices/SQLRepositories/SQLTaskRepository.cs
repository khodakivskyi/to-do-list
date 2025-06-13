using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;
using To_Do_List__Project.DatabaseServices.Interfaces;
using To_Do_List__Project.Models;

namespace To_Do_List__Project.Database.SQLRepositories
{
    public class SQLTaskRepository : ITaskService
    {
        private readonly string _connectionString;

        public SQLTaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddTask(TaskModel task)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = @"
            INSERT INTO Tasks (Text, Due_Date, Category_Id, Is_Completed, Created_At, Completed_At)
            VALUES (@Text, @Due_Date, @Category_Id, @Is_Completed, @Created_At, @Completed_At)";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Text", task.Text);
                        command.Parameters.AddWithValue("@Due_Date", task.Due_Date.HasValue ? task.Due_Date.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@Category_Id", task.Category_Id.HasValue ? task.Category_Id.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@Is_Completed", task.Is_Completed);
                        command.Parameters.AddWithValue("@Created_At", task.Created_At);
                        command.Parameters.AddWithValue("@Completed_At", (object?)task.Completed_At ?? DBNull.Value);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public List<TaskModel> GetAllTasks()
        {
            try
            {
                var tasks = new List<TaskModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Tasks";
                    var command = new SqlCommand(query, connection);
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        tasks.Add(new TaskModel
                        {
                            Id = (int)reader["Id"],
                            Text = reader["Text"].ToString()!,
                            Due_Date = reader["Due_Date"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["Due_Date"],
                            Category_Id = reader["Category_Id"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["Category_Id"]),
                            Is_Completed = (bool)(reader["Is_Completed"] ?? false),
                            Created_At = (DateTime)reader["Created_At"],
                            Completed_At = reader["Completed_At"] as DateTime?
                        });
                    }
                }

                return tasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<TaskModel>();
            }
        }

        public TaskModel? GetTaskById(int taskId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Tasks WHERE Id = @Id";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", taskId);
                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return new TaskModel
                        {
                            Id = (int)reader["Id"],
                            Text = reader["Text"].ToString()!,
                            Due_Date = reader["Due_Date"] == DBNull.Value ? null : (DateTime)reader["Due_Date"],
                            Category_Id = reader["Category_Id"] == DBNull.Value ? null : Convert.ToInt32(reader["Category_Id"]),
                            Is_Completed = (bool)(reader["Is_Completed"] ?? false),
                            Created_At = (DateTime)reader["Created_At"],
                            Completed_At = reader["Completed_At"] as DateTime?
                        };
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public List<TaskModel> GetActiveTasks()
        {
            try
            {
                return GetTasksByCompletionStatus(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<TaskModel>();
            }
        }

        public List<TaskModel> GetCompletedTasks()
        {
            try
            {
                return GetTasksByCompletionStatus(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<TaskModel>();
            }
        }
        private List<TaskModel> GetTasksByCompletionStatus(bool isCompleted)
        {
            var tasks = new List<TaskModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tasks WHERE Is_Completed = @Is_Completed";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Is_Completed", isCompleted);
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var task = new TaskModel
                    {
                        Id = (int)reader["Id"],
                        Text = reader["Text"].ToString()!,
                        Due_Date = reader["Due_Date"] == DBNull.Value ? null : (DateTime)reader["Due_Date"],
                        Category_Id = reader["Category_Id"] == DBNull.Value ? null : Convert.ToInt32(reader["Category_Id"]),
                        Is_Completed = isCompleted,
                        Created_At = (DateTime)reader["Created_At"]
                    };

                    if (isCompleted)
                    {
                        task.Completed_At = reader["Completed_At"] == DBNull.Value ? null : (DateTime)reader["Completed_At"];
                    }

                    tasks.Add(task);
                }
            }

            return tasks;
        }

        public void UpdateTask(TaskModel task)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Tasks SET Is_Completed = @Is_Completed, Completed_At = @Completed_At WHERE Id = @Id";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Is_Completed", task.Is_Completed);
                        command.Parameters.AddWithValue("@Completed_At", (object?)task.Completed_At ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Id", task.Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void ClearTasks()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "DELETE FROM Tasks";
                    var command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
