using Investment.Infra.Context;

namespace Investment.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>();
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => 
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });


        }

        public static void UseApiConfiguration(this WebApplication app)
        {
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

        }
    }
}
