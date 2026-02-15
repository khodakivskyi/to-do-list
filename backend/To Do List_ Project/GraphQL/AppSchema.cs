using GraphQL.Types;
using todo.GraphQL.Mutations;
using todo.GraphQL.Queries;

namespace todo.GraphQL
{
    public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<RootQuery>();
            Mutation = provider.GetRequiredService<RootMutation>();
        }
    }
}
