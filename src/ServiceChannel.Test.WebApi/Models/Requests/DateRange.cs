using System.Text.Json.Serialization;

namespace ServiceChannel.Test.WebApi.Models.Requests;

public class DateRange
{
    [JsonPropertyName("startDate")] public DateTime? StartDate { get; set; }

    [JsonPropertyName("endDate")] public DateTime? EndDate { get; set; }
}
