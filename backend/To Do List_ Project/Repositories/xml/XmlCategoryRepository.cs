using System.Xml.Serialization;
using todo.Repositories.Interfaces;
using todo.Models;

namespace todo.Repositories.XMLRepositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _filePath;

        public CategoryRepository(string filepath)
        {
            _filePath = filepath;

            if (!File.Exists(_filePath))
            {
                SaveCategories(new List<Category>());
            }
        }
        public void AddDefaultCategories()
        {
            var categories = GetCategories();

            if (categories == null || categories.Count == 0)
            {
                categories = new List<Category>
                    {
                        new() { Category_Name = "Робота" },
                        new() { Category_Name = "Особисте" },
                        new() { Category_Name = "Навчання" },
                        new() { Category_Name = "Покупки" }
                    };

                SaveCategories(categories);
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            if (!File.Exists(_filePath))
                return new List<Category>();

            var serializer = new XmlSerializer(typeof(List<Category>));
            using var stream = new FileStream(_filePath, FileMode.Open);
            return (List<Category>)await Task.Run(() => serializer.Deserialize(stream)!) ?? new List<Category>();
        }

        private void SaveCategories(List<Category> categories)
        {
            var serializer = new XmlSerializer(typeof(List<Category>));
            using var stream = new FileStream(_filePath, FileMode.Create);
            serializer.Serialize(stream, categories);
        }
    }
}
