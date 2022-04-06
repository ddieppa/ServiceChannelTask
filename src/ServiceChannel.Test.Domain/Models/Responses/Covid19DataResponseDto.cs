namespace ServiceChannel.Test.Domain.Models.Responses;

public class Covid19DataResponseDto
{
    private double averageDailyCases;
    private double? latitude;
    private double? longitude;
    public string? County { get; set; }
    public string? State { get; set; }

    public double? Latitude
    {
        get => string.IsNullOrWhiteSpace(this.State) ? this.latitude : null;
        set => this.latitude = value;
    }

    public double? Longitude
    {
        get => string.IsNullOrWhiteSpace(this.State) ? this.longitude : null;
        set => this.longitude = value;
    }

    public double AverageDailyCases
    {
        get => Math.Round(this.averageDailyCases);
        set => this.averageDailyCases = value;
    }

    public CasesPerDate? MinimumNumberOfCases { get; set; }
    public CasesPerDate? MaximumNumberOfCases { get; set; }
}
