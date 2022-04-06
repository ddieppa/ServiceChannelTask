using CsvHelper.Configuration;

using ServiceChannel.Test.Domain;
using ServiceChannel.Test.Domain.Models;

namespace ServiceChannel.Test.Infrastructure;

public class Covid19DataCsvMap : ClassMap<Covid19Data>
{
    public Covid19DataCsvMap()
    {
        this.Map(m => m.UID).Name("UID");
        this.Map(m => m.ISO2).Name("iso2");
        this.Map(m => m.ISO3).Name("iso3");
        this.Map(m => m.Code3).Name("code3");
        this.Map(m => m.FIPS).Name("FIPS");
        this.Map(m => m.Admin2).Name("Admin2");
        this.Map(m => m.ProvinceState).Name("Province_State");
        this.Map(m => m.CountryRegion).Name("Country_Region");
        this.Map(m => m.Latitude).Name("Lat");
        this.Map(m => m.Longitude).Name("Long_");
        this.Map(m => m.CombinedKey).Name("Combined_Key");
    }
}
