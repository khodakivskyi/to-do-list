using Microsoft.Data.SqlClient;
using To_Do_List__Project.Repositories;

namespace To_Do_List__Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ToDo;Integrated Security=True;Encrypt=True";

            builder.Services.AddScoped<TaskRepository>(provider =>
                new TaskRepository(connectionString));

            builder.Services.AddScoped<CategoryRepository>(provider =>
       new CategoryRepository(connectionString));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var categoryRepository = scope.ServiceProvider.GetRequiredService<CategoryRepository>();
                categoryRepository.AddDefaultCategories();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

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
