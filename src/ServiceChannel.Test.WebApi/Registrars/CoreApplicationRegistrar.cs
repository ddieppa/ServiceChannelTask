using ServiceChannel.Test.Infrastructure;

namespace ServiceChannel.Test.WebApi.Registrars;

public static class CoreApplicationRegistrar
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ICovid19DataService, Covid19DataService>();
        services.AddScoped<ICovid19HttpClient>(provider => provider.GetRequiredService<ICovid19RefitClient>());

        return services;
    }
}
