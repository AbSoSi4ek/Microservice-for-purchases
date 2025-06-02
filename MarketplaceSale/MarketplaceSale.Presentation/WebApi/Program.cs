using MarketplaceSale.Infrastructure.EntityFramework;
namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var dbConnectionString = builder.Configuration.GetConnectionString(nameof(ApplicationDbContext));

            if (string.IsNullOrEmpty(dbConnectionString))
            {
                throw new InvalidOperationException("Connection string for ApplicationDbContext is not configured.");
            }

            builder.Services.AddNpgsql<ApplicationDbContext>(dbConnectionString, options =>
            {
                options.MigrationsAssembly("MarketplaceSale.Infrastructure.EntityFramework");

            });

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}


