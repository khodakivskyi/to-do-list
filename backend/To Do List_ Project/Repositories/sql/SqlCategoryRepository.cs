using Microsoft.Data.SqlClient;
using System.Data;
using todo.Models;
using todo.Repositories.Interfaces;

namespace todo.Repositories.SQLRepositories
{
    public class SqlCategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public SqlCategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddDefaultCategoriesAsync(List<string> defaultCategories)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var categories = await GetCategoriesAsync();

                if (categories == null || !categories.Any())
                {
                    foreach (var name in defaultCategories)
                    {
                        string insertQuery = "INSERT INTO Categories (Category_Name) VALUES (@Category_Name)";
                        using var cmd = new SqlCommand(insertQuery, connection);
                        cmd.Parameters.Add("@Category_Name", SqlDbType.NVarChar).Value = name;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = new List<Category>();

            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Categories";
                var command = new SqlCommand(query, connection);
                connection.Open();
                var reader = await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        Category_Id = (int)reader["Category_Id"],
                        Category_Name = reader["Category_Name"].ToString()
                    });
                }
            }

            return categories;
        }
    }
}
