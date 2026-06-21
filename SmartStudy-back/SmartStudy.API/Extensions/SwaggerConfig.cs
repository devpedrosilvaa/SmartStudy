namespace SmartStudy.API.Extensions
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                new Microsoft.OpenApi.OpenApiInfo
                {
                    Title = "SmartStudy API",
                    Version = "v1",
                    Description = "API for SmartStudy application"
                });
            });
            return services;
        }
}
}
