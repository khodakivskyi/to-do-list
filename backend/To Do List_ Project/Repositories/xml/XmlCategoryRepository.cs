using System.Xml.Serialization;
using todo.Repositories.Interfaces;
using todo.Models;

namespace todo.Repositories.XMLRepositories
{
    public class XmlCategoryRepository : ICategoryRepository
    {
        private readonly string _filePath;

        public XmlCategoryRepository(string filepath)
        {
            _filePath = filepath;

            if (!File.Exists(_filePath))
            {
                SaveCategories(new List<Category>());
            }
        }
        public async Task AddDefaultCategoriesAsync(List<string> defaultCategories)
        {
            var categories = await GetCategoriesAsync();

            if (categories == null || !categories.Any())
            {
                var newCategories = defaultCategories
                    .Select((name, index) => new Category
                    {
                        Category_Id = index + 1,
                        Category_Name = name
                    })
                    .ToList();

                SaveCategories(newCategories);
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
