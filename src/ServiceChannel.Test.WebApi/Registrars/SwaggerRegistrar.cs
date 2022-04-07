using ServiceChannel.Test.WebApi.Options;

namespace ServiceChannel.Test.WebApi.Registrars;

public static class SwaggerRegistrar
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddSwaggerGen();
        services.ConfigureOptions<ConfigureSwaggerOptions>();

        return services;
    }
}
