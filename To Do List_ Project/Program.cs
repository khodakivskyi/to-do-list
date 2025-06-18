using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using To_Do_List__Project.Database.SQLRepositories;
using To_Do_List__Project.Database.XMLRepositories;
using To_Do_List__Project.GraphQL;
using To_Do_List__Project.GraphQL.Mutations;
using To_Do_List__Project.GraphQL.Queries;
using To_Do_List__Project.GraphQL.Types;


namespace To_Do_List__Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // SQL
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ToDo;Integrated Security=True;Encrypt=True";
            builder.Services.AddScoped<SQLTaskRepository>(_ => new SQLTaskRepository(connectionString));
            builder.Services.AddScoped<SQLCategoryRepository>(_ => new SQLCategoryRepository(connectionString));

            // XML
            builder.Services.AddScoped<XMLTaskRepository>(_ =>
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "XMLdb");
                Directory.CreateDirectory(path);
                return new XMLTaskRepository(Path.Combine(path, "tasks.xml"));
            });

            builder.Services.AddScoped<XMLCategoryRepository>(_ =>
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "XMLdb");
                Directory.CreateDirectory(path);
                return new XMLCategoryRepository(Path.Combine(path, "categories.xml"));
            });

            // GraphQL Types
            builder.Services.AddSingleton<TaskType>();
            builder.Services.AddSingleton<CategoryType>();

            builder.Services.AddSingleton<TaskQuery>();
            builder.Services.AddSingleton<CategoryQuery>();
            builder.Services.AddSingleton<RootQuery>();

            builder.Services.AddSingleton<TaskMutation>();
            builder.Services.AddSingleton<RootMutation>();
            builder.Services.AddSingleton<ISchema, AppSchema>();

            builder.Services.AddGraphQL(builder =>
            {
                builder.AddSystemTextJson();
                builder.AddErrorInfoProvider(opt => opt.ExposeExceptionDetails = true);
                builder.AddGraphTypes(typeof(RootQuery).Assembly);
                builder.AddGraphTypes(typeof(RootMutation).Assembly);
            });

            builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
            builder.Services.AddSession();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<SQLCategoryRepository>().AddDefaultCategories();
                scope.ServiceProvider.GetRequiredService<XMLCategoryRepository>().AddDefaultCategories();
            }

            if (!app.Environment.IsDevelopment())
                app.UseExceptionHandler("/Home/Error");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseGraphQL<ISchema>("/graphql");
            app.UseGraphQLGraphiQL("/ui/graphql");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
