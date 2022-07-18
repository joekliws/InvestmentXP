using Microsoft.OpenApi.Models;

namespace Investment.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration (this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Investing XP API",
                    Description = "TESTE TÉCNICO XP - API de Compra e Venda de Ativos",
                    Contact = new OpenApiContact() { Name = "Wilk Morais", Email = "wilkj.ms@gmail.com", Url= new Uri("https://github.com/joekliws")}
                });
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
