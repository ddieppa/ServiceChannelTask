using ServiceChannel.Test.Domain.Models;

namespace ServiceChannel.Test.Application;

public interface ICsvHelper
{
    Task<IEnumerable<Covid19Data>> GetRecordsAsync(HttpContent content);
}
