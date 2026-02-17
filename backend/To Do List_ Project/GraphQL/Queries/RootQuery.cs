using GraphQL.Types;

namespace todo.GraphQL.Queries
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(TaskQuery taskQuery, CategoryQuery categoryQuery)
        {
            Name = "Query";

            var queries = new ObjectGraphType[] { taskQuery, categoryQuery };

            foreach (var query in queries)
            {
                foreach (var field in query.Fields)
                {
                    AddField(field);
                }
            }
        }
    }
}
