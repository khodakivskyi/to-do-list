using GraphQL.Types;
using To_Do_List__Project.Models;

namespace To_Do_List__Project.GraphQL.Types
{
    public class CategoryType : ObjectGraphType<Category>
    {
        public CategoryType()
        {
            Field(x => x.Category_Id);
            Field(x => x.Category_Name);
        }
    }

}
