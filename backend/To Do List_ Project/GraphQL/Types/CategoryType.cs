using GraphQL.Types;
using To_Do_List__Project.Models;

namespace To_Do_List__Project.GraphQL.Types
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
