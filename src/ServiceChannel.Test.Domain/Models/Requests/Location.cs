namespace ServiceChannel.Test.Domain.Requests;

public class Location
{
    public Location(string? County, string? State)
    {
        this.County = County;
        this.State = State;
    }

    public string? County { get; init; }

    public string? State { get; init; }

    public void Deconstruct(out string? County, out string? State)
    {
        County = this.County;
        State = this.State;
    }
}
