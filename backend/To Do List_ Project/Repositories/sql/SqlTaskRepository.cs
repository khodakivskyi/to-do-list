using Microsoft.Data.SqlClient;
using todo.Repositories.Interfaces;
using todo.Models;

namespace todo.Repositories.SQLRepositories
{
    public class SqlTaskRepository : ITaskRepository
    {
        private readonly string _connectionString;

        public SqlTaskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<TaskModel?> GetTaskByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tasks WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
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

        public async Task<TaskModel?> AddTaskAsync(TaskModel task)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
    INSERT INTO Tasks (Text, Due_Date, Category_Id, Is_Completed, Created_At, Completed_At)
    OUTPUT INSERTED.*
    VALUES (@Text, @Due_Date, @Category_Id, @Is_Completed, @Created_At, @Completed_At)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Text", task.Text);
                    command.Parameters.AddWithValue("@Due_Date", task.Due_Date ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Category_Id", task.Category_Id ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Is_Completed", task.Is_Completed);
                    command.Parameters.AddWithValue("@Created_At", task.Created_At);
                    command.Parameters.AddWithValue("@Completed_At", task.Completed_At ?? (object)DBNull.Value);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapReaderToTaskModel(reader);
                        }
                    }
                }
            }
            return null;
        }

        public async Task<bool> MarkTaskAsCompleteAsync(TaskModel task)
        {
            Console.WriteLine($"Marking task with ID {task.Id} as complete. Is_Completed: {task.Is_Completed}, Completed_At: {task.Completed_At}");
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE Tasks SET Is_Completed = @Is_Completed, Completed_At = @Completed_At WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Is_Completed", task.Is_Completed);
                    command.Parameters.AddWithValue("@Completed_At", task.Completed_At);
                    command.Parameters.AddWithValue("@Id", task.Id);

                    var affectedRows = await command.ExecuteNonQueryAsync();

                    return affectedRows > 0;
                }
            }
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
        {
            var tasks = new List<TaskModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tasks";
                var command = new SqlCommand(query, connection);
                connection.Open();
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
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

        public async Task<IEnumerable<TaskModel>> GetTasksByCompletionStatusAsync(bool isCompleted)
        {
            var tasks = new List<TaskModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tasks WHERE Is_Completed = @Is_Completed";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Is_Completed", isCompleted ? 1 : 0); 

                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var task = new TaskModel
                    {
                        Id = (int)reader["Id"],
                        Text = reader["Text"].ToString()!,
                        Due_Date = reader["Due_Date"] == DBNull.Value ? null : (DateTime)reader["Due_Date"],
                        Category_Id = reader["Category_Id"] == DBNull.Value ? null : Convert.ToInt32(reader["Category_Id"]),
                        Is_Completed = (bool)reader["Is_Completed"],
                        Created_At = (DateTime)reader["Created_At"],
                        Completed_At = reader["Completed_At"] == DBNull.Value ? null : (DateTime)reader["Completed_At"]
                    };

                    tasks.Add(task);
                }
            }

            return tasks;
        }

        public async Task ClearTasksAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Tasks";
                var command = new SqlCommand(query, connection);
                connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }


        private TaskModel MapReaderToTaskModel(SqlDataReader reader)
        {
            return new TaskModel
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Text = reader.GetString(reader.GetOrdinal("Text")),
                Due_Date = reader.IsDBNull(reader.GetOrdinal("Due_Date"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("Due_Date")),
                Category_Id = reader.IsDBNull(reader.GetOrdinal("Category_Id"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("Category_Id")),
                Is_Completed = reader.GetBoolean(reader.GetOrdinal("Is_Completed")),
                Created_At = reader.GetDateTime(reader.GetOrdinal("Created_At")),
                Completed_At = reader.IsDBNull(reader.GetOrdinal("Completed_At"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("Completed_At"))
            };
        }
    }
}