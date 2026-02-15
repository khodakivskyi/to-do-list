using GraphQL.Types;

namespace todo.GraphQL.Mutations
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation()
        {
            var taskMutation = new TaskMutation();

            foreach (var field in taskMutation.Fields)
            {
                AddField(field);
            }
        }
    }
}
