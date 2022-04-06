using ServiceChannel.Test.Domain.Models.Responses;
using ServiceChannel.Test.Domain.Requests;

namespace ServiceChannel.Test.Application;

public interface ICovid19DataService
{
    Task<IEnumerable<Covid19DataResponseDto>> GetCovid19DataAsync(Covid19DataFilterDto filter);
}
