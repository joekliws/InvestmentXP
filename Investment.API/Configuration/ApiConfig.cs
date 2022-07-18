namespace Investment.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddConfiguration(this IServiceCollection services)
        {
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
    }
}
