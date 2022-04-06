using System.Text.Json.Serialization;

namespace ServiceChannel.Test.WebApi.Models.Requests;

public class Covid19DataRequest
{
    [JsonPropertyName("location")] public Location? Location { get; set; }

    [JsonPropertyName("dateRange")] public DateRange? DateRange { get; set; }
}
