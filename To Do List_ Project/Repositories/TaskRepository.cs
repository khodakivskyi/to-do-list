using Microsoft.Data.SqlClient;
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
                            Due_Date = (DateTime)reader["Due_Date"],
                            Category_Id = reader["Category_Id"] as int?,
                            Is_Complited = (bool)(reader["Is_Complited"] ?? false),
                            Created_At = (DateTime)reader["Created_At"],
                            Complited_At = reader["Complited_At"] as DateTime?
                        });
                    }
                }

                return tasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public void AddTask(TaskModel task)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = @"
            INSERT INTO Tasks (Text, Due_Date, Category_Id, Is_Complited, Created_At, Complited_At)
            VALUES (@Text, @Due_Date, @Category_Id, @Is_Completed, @Created_At, @Completed_At)";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Text", task.Text);
                        command.Parameters.AddWithValue("@Due_Date", task.Due_Date);
                        command.Parameters.AddWithValue("@Category_Id", (object?)task.Category_Id ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Is_Completed", task.Is_Complited);
                        command.Parameters.AddWithValue("@Created_At", task.Created_At);
                        command.Parameters.AddWithValue("@Completed_At", (object?)task.Complited_At ?? DBNull.Value);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public List<TaskModel> GetActiveTasks()
        {
            var tasks = new List<TaskModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tasks WHERE Is_Complited = 0";
                var command = new SqlCommand(query, connection);
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new TaskModel
                    {
                        Id = (int)reader["Id"],
                        Text = reader["Text"].ToString(),
                        Due_Date = (DateTime)reader["Due_Date"],
                        Category_Id = (int)reader["Category_Id"],
                        Is_Complited = (bool)reader["Is_Complited"],
                        Created_At = (DateTime)reader["Created_At"]
                    });
                }
            }

            return tasks;
        }

        public TaskModel GetTaskById(int taskId)
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
                        Text = reader["Text"].ToString(),
                        Due_Date = (DateTime)reader["Due_Date"],
                        Category_Id = reader["Category_Id"] as int?,
                        Is_Complited = (bool)(reader["Is_Complited"] ?? false),
                        Created_At = (DateTime)reader["Created_At"],
                        Complited_At = reader["Complited_At"] as DateTime?
                    };
                }
                return null;
            }
        }
        public void UpdateTask(TaskModel task)
        {
            using (var connection = new SqlConnection(_connectionString))
            {

            }
        }
    }
}
