namespace ServiceChannel.Test.Application;

public interface ICovid19HttpClient
{
    Task<HttpContent> GetCovid19DataAsync();
}
