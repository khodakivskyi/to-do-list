using GraphQL.Types;

namespace todo.GraphQL.Mutations
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation(TaskMutation taskMutation)
        {
            Name = "Mutation";

            var mutations = new ObjectGraphType[] { taskMutation };

            foreach (var mutation in mutations)
            {
                foreach (var field in mutation.Fields)
                {
                    AddField(field);
                }
            }
        }
    }
}
