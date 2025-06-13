using GraphQL.Types;
using To_Do_List__Project.DatabaseServices.Interfaces;

namespace To_Do_List__Project.GraphQL
{
    public class CategoryQuery : ObjectGraphType
    {
        public CategoryQuery(ICategoryService categoryService)
        {
            Field<ListGraphType<CategoryType>>("categories")
                .Description("Отримати всі категорії")
                .Resolve(context => categoryService.GetCategories());
        }
    }
}
