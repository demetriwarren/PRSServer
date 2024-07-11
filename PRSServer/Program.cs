using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PRSServer.Data;
namespace PRSServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<PRSDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PRSContext")
                ?? throw new InvalidOperationException("Connection string 'PRSDbContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
