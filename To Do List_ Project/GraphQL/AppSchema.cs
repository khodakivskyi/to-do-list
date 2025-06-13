using GraphQL.Types;

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
