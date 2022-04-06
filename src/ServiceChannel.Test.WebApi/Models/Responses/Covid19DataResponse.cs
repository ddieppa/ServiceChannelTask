using ServiceChannel.Test.Domain;

namespace ServiceChannel.Test.WebApi.Models.Responses;

public class Covid19DataResponse
{
    public string? County { get; set; }
    public string? State { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double  AverageDailyCases { get; set; }
    public CasesPerDate? MinimumNumberOfCases { get; set; }
    public CasesPerDate? MaximumNumberOfCases { get; set; }
}
