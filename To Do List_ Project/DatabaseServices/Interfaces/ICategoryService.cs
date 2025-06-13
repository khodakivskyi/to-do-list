using To_Do_List__Project.Models;

namespace To_Do_List__Project.DatabaseServices.Interfaces
{
    public interface ICategoryService
    {
        void AddDefaultCategories();
        List<Category> GetCategories();
    }
}
