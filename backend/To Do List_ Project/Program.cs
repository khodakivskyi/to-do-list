using dotenv.net;
using GraphQL;
using GraphQL.Types;
using todo.Models;
using todo.GraphQL;
using todo.GraphQL.Mutations;
using todo.GraphQL.Queries;
using todo.GraphQL.Types;
using todo.Repositories.SQLRepositories;
using todo.Repositories.XMLRepositories;

namespace todo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotEnv.Load();

            var builder = WebApplication.CreateBuilder(args);

            // Read environment variables
            var envVars = DotEnv.Read();
            string connectionString = envVars["DATABASE_URL"] ?? throw new InvalidOperationException("DATABASE_URL is not set");
            string appUrl = envVars["APP_URL"] ?? "http://localhost:5000";


            // Configure sql repositories and xml storages + repositories
            builder.Services.AddScoped<SqlTaskRepository>(sp => new SqlTaskRepository(connectionString));
            builder.Services.AddScoped<SqlCategoryRepository>(sp => new SqlCategoryRepository(connectionString));

            var xmlSettings = builder.Configuration.GetSection("XmlStorageConfiguration").Get<XmlDbSettings>() ?? new XmlDbSettings();
            var xmlFolderPath = Path.Combine(Directory.GetCurrentDirectory(), xmlSettings.Folder);
            Directory.CreateDirectory(xmlFolderPath);

            var xmlTasksPath = Path.Combine(xmlFolderPath, xmlSettings.TasksFile);
            var xmlCategoriesPath = Path.Combine(xmlFolderPath, xmlSettings.CategoriesFile);

            builder.Services.AddScoped(sp => new XmlTaskRepository(Path.Combine(xmlFolderPath, "tasks.xml")));
            builder.Services.AddScoped(sp => new XmlCategoryRepository(Path.Combine(xmlFolderPath, "categories.xml")));


            // GraphQL
            builder.Services.AddSingleton<TaskType>();
            builder.Services.AddSingleton<CategoryType>();
            builder.Services.AddSingleton<RootQuery>();
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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy
                        .WithOrigins(appUrl)
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<Repositories.SQLRepositories.SqlCategoryRepository>().AddDefaultCategories();
                scope.ServiceProvider.GetRequiredService<Repositories.XMLRepositories.XmlCategoryRepository>().AddDefaultCategories();
            }

            if (!app.Environment.IsDevelopment())
                app.UseExceptionHandler("/Home/Error");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseSession();

            app.UseGraphQL<ISchema>("/graphql");
            app.UseGraphQLGraphiQL("/ui/graphql");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
