namespace todo.Models
{
    public class XmlDbSettings
    {
        public string Folder { get; set; } = "XmlStorage";
        public string TasksFile { get; set; } = "tasks.xml";
        public string CategoriesFile { get; set; } = "categories.xml";
    }
}
