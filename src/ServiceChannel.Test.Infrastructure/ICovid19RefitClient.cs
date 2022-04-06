using Refit;

using ServiceChannel.Test.Application;

namespace ServiceChannel.Test.Infrastructure;

public interface ICovid19RefitClient : ICovid19HttpClient
{
    [Get("/time_series_covid19_confirmed_US.csv")]
#pragma warning disable CS0108, CS0114
    Task<HttpContent> GetCovid19DataAsync();
#pragma warning restore CS0108, CS0114
}
