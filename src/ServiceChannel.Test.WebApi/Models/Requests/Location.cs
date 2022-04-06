using System.Text.Json.Serialization;

namespace ServiceChannel.Test.WebApi.Models.Requests;

public class Location
{
    public Location(string? County, string? State)
    {
        this.County = County;
        this.State = State;
    }

    [JsonPropertyName("county")] public string? County { get; init; }

    [JsonPropertyName("state")] public string? State { get; init; }

    public void Deconstruct(out string? County, out string? State)
    {
        County = this.County;
        State = this.State;
    }
}
