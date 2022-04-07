using System.Net.Mime;

using Refit;

using ServiceChannel.Test.Infrastructure;

namespace ServiceChannel.Test.WebApi.Registrars;

public static class RefitClientsRegistrar
{
    public static IServiceCollection AddRefitClientsServices(this IServiceCollection services)
    {
        services.AddRefitClient<ICovid19RefitClient>(provider => new RefitSettings())
               .ConfigureHttpClient(c =>
               {
                   c.BaseAddress =
                       new
                           Uri("https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series");
                   c.DefaultRequestHeaders.Add("Accept",
                                               MediaTypeNames.Application.Json);
               });

        return services;
    }
}
