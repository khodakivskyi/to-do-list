using GraphQL;
using GraphQL.Types;
using todo.GraphQL.Types;
using todo.Repositories.Interfaces;
using todo.Repositories.SQLRepositories;
using todo.Repositories.XMLRepositories;
using todo.Services.Interfaces;

namespace todo.GraphQL.Queries
{
    public class CategoryQuery : ObjectGraphType
    {
        public CategoryQuery(ICategoryService categoryService)
        {
            Field<ListGraphType<CategoryType>>("categories")
                .Argument<IntGraphType>("storageTypeId")
                .ResolveAsync(async context =>
                {
                    var storageTypeId = context.GetArgument<int>("storageTypeId");

                    return await categoryService.GetCategoriesAsync(storageTypeId);
                });
        }
    }
}
