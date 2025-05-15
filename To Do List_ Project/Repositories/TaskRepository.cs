using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;
using To_Do_List__Project.Models;

namespace To_Do_List__Project.Repositories
{
    public class TaskRepository
    {
        private readonly string _connectionString;

        public TaskRepository(string connectionString)
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
                        command.Parameters.AddWithValue("@Due_Date", task.Due_Date.HasValue ? (object)task.Due_Date.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@Category_Id", task.Category_Id.HasValue ? (object)task.Category_Id.Value : DBNull.Value);
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

        public List<TaskModel> GetActiveTasks()
        {
            try
            {
                var tasks = new List<TaskModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Tasks WHERE Is_Completed = 0";
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
                            Is_Completed = reader["Is_Completed"] == DBNull.Value ? (bool?)null : (bool)reader["Is_Completed"],
                            Created_At = (DateTime)reader["Created_At"]
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

        public List<TaskModel> GetCompletedTasks()
        {
            try
            {
                var tasks = new List<TaskModel>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Tasks WHERE Is_Completed = @Is_Completed";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Is_Completed", true);
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
                            Is_Completed = reader["Is_Completed"] != DBNull.Value && (bool)reader["Is_Completed"],
                            Created_At = (DateTime)reader["Created_At"],
                            Completed_At = reader["Completed_At"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["Completed_At"]
                        });
                    }

                    return tasks;
                }
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
                            Due_Date = reader["Due_Date"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["Due_Date"],
                            Category_Id = reader["Category_Id"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["Category_Id"]),
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

        public void CleanTasks()
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
