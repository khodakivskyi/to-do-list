using Microsoft.Data.SqlClient;

namespace To_Do_List__Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectingString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ToDo;Integrated Security=True;Encrypt=True";

            using (SqlConnection connection = new SqlConnection(connectingString))
            {
                try
                {
                    connection.Open();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

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
