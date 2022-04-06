using ServiceChannel.Test.Domain;
using ServiceChannel.Test.Domain.Models;
using ServiceChannel.Test.Domain.Models.Responses;
using ServiceChannel.Test.Domain.Requests;

namespace ServiceChannel.Test.Application;

public class Covid19DataService : ICovid19DataService
{
    private readonly ICovid19HttpClient covid19HttpClient;
    private readonly ICsvHelper csvHelper;

    public Covid19DataService(ICovid19HttpClient covid19HttpClient, ICsvHelper csvHelper)
    {
        this.covid19HttpClient = covid19HttpClient;
        this.csvHelper = csvHelper;
    }

    public async Task<IEnumerable<Covid19DataResponseDto>> GetCovid19DataAsync(Covid19DataFilterDto filter)
    {
        var httpContent = await this.covid19HttpClient.GetCovid19DataAsync()
                                    .ConfigureAwait(false);

        var fullData = await this.csvHelper.GetRecordsAsync(httpContent)
                                 .ConfigureAwait(false);

        var filteredData = new List<Covid19DataResponseDto>();
        foreach (var covid19Data in fullData)
        {
            var casesPerDate = ApplyDateRange(filter.DateRange,
                                         covid19Data);
            if (!casesPerDate.Any())
            {
                return filteredData;
            }
            covid19Data.CasesPerDate = casesPerDate;

            var IsCountyNull = string.IsNullOrWhiteSpace(filter.Location?.County);
            var IsStateNull = string.IsNullOrWhiteSpace(filter.Location?.State);
            var isThereACounty = string.Equals(covid19Data.Admin2,
                                               filter.Location?.County,
                                               StringComparison.OrdinalIgnoreCase);
            var IsThereAnState = string.Equals(covid19Data.ProvinceState,
                                               filter.Location?.State,
                                               StringComparison.OrdinalIgnoreCase);

            switch (IsCountyNull)
            {
                case false when !IsStateNull:
                {
                    if(isThereACounty && IsThereAnState)
                    {
                        filteredData.Add(CreateResponseDtoBasedOnData(covid19Data, filter.Location?.State));
                    }

                    break;
                }
                case false when IsStateNull:
                {
                    if(isThereACounty)
                    {
                        filteredData.Add(CreateResponseDtoBasedOnData(covid19Data, string.Empty));
                    }

                    break;
                }
                case true when IsThereAnState:
                    filteredData.Add(CreateResponseDtoBasedOnData(covid19Data, covid19Data.ProvinceState));
                    break;
            }
        }

        return filteredData;
    }

    private static IList<CasesPerDate?> ApplyDateRange(DateRange? dateRange, Covid19Data covid19Data)
    {
        var casesPerDate = covid19Data.CasesPerDate;
        if (dateRange is not null)
        {
            casesPerDate = covid19Data.CasesPerDate.Where(x => x?.Date >= dateRange.StartDate &&
                                                               x?.Date <= dateRange.EndDate)
                                      .ToList();
        }

        return casesPerDate;
    }

    private static Covid19DataResponseDto CreateResponseDtoBasedOnData(Covid19Data covid19Data, string? state)
    {
        var covid19DataResponseDto = new Covid19DataResponseDto();

        covid19DataResponseDto.State = state;
        covid19DataResponseDto.County = covid19Data.Admin2;
        covid19DataResponseDto.Longitude = covid19Data.Longitude;
        covid19DataResponseDto.Latitude = covid19Data.Latitude;
        covid19DataResponseDto.AverageDailyCases = covid19Data.CasesPerDate.Select(c => c?.Cases ?? 0)
                                                                          .Average();
        covid19DataResponseDto.MinimumNumberOfCases = covid19Data.CasesPerDate.MinBy(x => x?.Cases ?? 0);
        covid19DataResponseDto.MaximumNumberOfCases = covid19Data.CasesPerDate.MaxBy(x => x?.Cases ?? 0);

        return covid19DataResponseDto;
    }
}
