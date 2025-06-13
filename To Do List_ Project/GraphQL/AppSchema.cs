using GraphQL.Types;
using To_Do_List__Project.GraphQL.Queries;

namespace To_Do_List__Project.GraphQL
{
    public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<RootQuery>();
            //
        }
    }
}
