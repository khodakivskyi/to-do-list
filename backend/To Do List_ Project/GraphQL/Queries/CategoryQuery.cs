using GraphQL;
using GraphQL.Types;
using To_Do_List__Project.Database.SQLRepositories;
using To_Do_List__Project.Database.XMLRepositories;
using To_Do_List__Project.DatabaseServices.Interfaces;
using To_Do_List__Project.GraphQL.Types;

namespace To_Do_List__Project.GraphQL.Queries
{
    public class CategoryQuery : ObjectGraphType
    {
        public CategoryQuery()
        {
            Field<ListGraphType<CategoryType>>("categories")
                .Description("Отримати всі категорії")
                .Argument<StringGraphType>("source", "sql або xml")
                .Resolve(context =>
                {
                    var source = context.GetArgument<string>("source")?.ToLower();
                    var services = context.RequestServices;

                    ICategoryService categoryService = source switch
                    {
                        "xml" => services!.GetRequiredService<XMLCategoryRepository>(),
                        _ => services!.GetRequiredService<SQLCategoryRepository>()
                    };

                    return categoryService.GetCategories();
                });
        }
    }
}
