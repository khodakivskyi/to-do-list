using GraphQL.Types;

namespace To_Do_List__Project.GraphQL.Queries
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(TaskQuery taskQuery, CategoryQuery categoryQuery)
        {
            Name = "Query";

            foreach (var field in taskQuery.Fields)
                AddField(field);

            foreach (var field in categoryQuery.Fields)
                AddField(field);
        }
    }
}
