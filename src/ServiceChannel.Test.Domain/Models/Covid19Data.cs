namespace ServiceChannel.Test.Domain.Models;

public class Covid19Data
{
    public Covid19Data() => this.CasesPerDate = new List<CasesPerDate?>();
    public int? UID { get; set; }
    public string? ISO2 { get; set; }
    public string? ISO3 { get; set; }
    public int? Code3 { get; set; }
    public decimal? FIPS { get; set; }
    public string? Admin2 { get; set; }
    public string? ProvinceState { get; set; }
    public string? CountryRegion { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? CombinedKey { get; set; }

    public IList<CasesPerDate?> CasesPerDate { get; set; }
}
