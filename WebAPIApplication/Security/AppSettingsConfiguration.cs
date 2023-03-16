namespace WebAPIApplication.Security
{
    public static class AppSettingsConfiguration
    {
        public static IServiceCollection AddAppSettingsModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Use this to load settings from appSettings file
            services.Configure<SecuritySettings>(options => configuration.GetSection("Jwt").Bind(options));

            return services;
        }
    }
}
