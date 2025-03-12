namespace Warranty.API.Extensions
{
    public static class CorsExtensions
    {
        public const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public static void AddAllowAnyCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }
    }
}
