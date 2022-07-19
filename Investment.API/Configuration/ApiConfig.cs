﻿using Investment.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Investment.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                string connectionStr = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionStr, assembly=>assembly.MigrationsAssembly("Investment.API"));
            });
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
