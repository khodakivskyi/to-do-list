using GraphQL;
using GraphQL.Types;
using todo.Repositories.SQLRepositories;
using todo.Repositories.XMLRepositories;
using todo.Repositories.Interfaces;
using todo.GraphQL.Types;

namespace todo.GraphQL.Queries
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

                    ICategoryRepository categoryService = source switch
                    {
                        "xml" => services!.GetRequiredService<Repositories.XMLRepositories.XmlCategoryRepository>(),
                        _ => services!.GetRequiredService<Repositories.SQLRepositories.SqlCategoryRepository>()
                    };

                    return categoryService.GetCategories();
                });
        }
    }
}
