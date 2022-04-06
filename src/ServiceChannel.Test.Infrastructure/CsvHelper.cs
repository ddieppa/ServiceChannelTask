using System.Globalization;

using CsvHelper;

using ServiceChannel.Test.Application;
using ServiceChannel.Test.Domain;
using ServiceChannel.Test.Domain.Models;

namespace ServiceChannel.Test.Infrastructure;

public class CsvHelper : ICsvHelper
{
    public async Task<IEnumerable<Covid19Data>> GetRecordsAsync(HttpContent content)
    {
        var result = new List<Covid19Data>();
        await using var stream = await content.ReadAsStreamAsync()
                                              .ConfigureAwait(false);

        using var csvReader = new CsvReader(new StreamReader(stream),
                                            CultureInfo.InvariantCulture);

        await csvReader.ReadAsync();
        csvReader.ReadHeader();

        while (await csvReader.ReadAsync())
        {
            var covidData = new Covid19Data();
            covidData.UID = csvReader.GetField<int>("UID");
            covidData.ISO2 = csvReader.GetField<string>("iso2");
            covidData.ISO3 = csvReader.GetField<string>("iso3");
            covidData.Code3 = csvReader.GetField<int>("code3");
            covidData.FIPS = csvReader.TryGetField<decimal?>(nameof(covidData.FIPS),
                                                             out var fips)
                                 ? fips
                                 : null;
            covidData.Admin2 = csvReader.GetField<string>("Admin2");;
            covidData.ProvinceState = csvReader.GetField<string>("Province_State");
            covidData.CountryRegion = csvReader.GetField<string>("Country_Region");;
            covidData.Latitude = csvReader.GetField<double>("Lat");
            covidData.Longitude = csvReader.GetField<double>("Long_");
            covidData.CombinedKey = csvReader.GetField<string>("Combined_Key");
            for (var i = 11; i < csvReader.HeaderRecord.Length; i++)
            {
                var casesPerDate = new CasesPerDate();
                casesPerDate.Date = DateTime.Parse(csvReader.HeaderRecord[i],
                                                   CultureInfo.InvariantCulture);
                casesPerDate.Cases = csvReader.GetField<int>(csvReader.HeaderRecord[i]);
                covidData.CasesPerDate.Add(casesPerDate);
            }

            result.Add(covidData);
        }

        return result;
    }
}
