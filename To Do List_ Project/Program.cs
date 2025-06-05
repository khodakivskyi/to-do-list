using Microsoft.Data.SqlClient;
using To_Do_List__Project.Repositories;
using To_Do_List__Project.XMLRepositories;

namespace To_Do_List__Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ToDo;Integrated Security=True;Encrypt=True";

            builder.Services.AddScoped<SQLTaskRepository>(provider =>
                new SQLTaskRepository(connectionString));
            builder.Services.AddScoped<SQLCategoryRepository>(provider =>
       new SQLCategoryRepository(connectionString));

            builder.Services.AddScoped<XMLTaskRepository>(provider =>
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "XMLdb");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, "tasks.xml");

                return new XMLTaskRepository(filePath);
            });
            builder.Services.AddScoped<XMLCategoryRepository>(provider =>
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "XMLdb");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, "categories.xml");

                return new XMLCategoryRepository(filePath);
            });


            // Add services to the container.
            builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
            builder.Services.AddSession();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var categoryRepository = scope.ServiceProvider.GetRequiredService<SQLCategoryRepository>();
                categoryRepository.AddDefaultCategories();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
