namespace ServiceChannel.Test.WebApi.Registrars;

public static class InfrastructureRegistrar
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<ICsvHelper, ServiceChannel.Test.Infrastructure.CsvHelper>();
        return services;
    }
}
