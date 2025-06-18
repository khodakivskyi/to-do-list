using GraphQL.Types;

namespace To_Do_List__Project.GraphQL.Mutations
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
