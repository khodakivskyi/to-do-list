using GraphQL.Types;
using todo.Models;

namespace todo.GraphQL.Types
{
    public class CategoryType : ObjectGraphType<Category>
    {
        public CategoryType()
        {
            Name = "Category";

            Field<IntGraphType>("id")
                .Resolve(ctx => ctx.Source.Category_Id);

            Field<StringGraphType>("categoryName")
                .Resolve(ctx => ctx.Source.Category_Name);
        }
    }
}
