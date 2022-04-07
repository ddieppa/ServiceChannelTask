using Mapster;

using Microsoft.AspNetCore.Mvc;

using ServiceChannel.Test.Application;
using ServiceChannel.Test.Domain.Requests;
using ServiceChannel.Test.WebApi.Models.Requests;
using ServiceChannel.Test.WebApi.Models.Responses;

namespace ServiceChannel.Test.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Covid19InformationController : ControllerBase
{
    private readonly ICovid19DataService covid19DataService;
    private readonly ILogger<Covid19InformationController> logger;

    public Covid19InformationController(ICovid19DataService covid19DataService,
                                        ILogger<Covid19InformationController> logger)
    {
        this.covid19DataService = covid19DataService;
        this.logger = logger;
    }

    /// <summary>
    ///     Get Covid19 Information
    /// </summary>
    /// <param name="covid19DataFilterRequest"></param>
    /// <returns>Covid19 Data</returns>
    /// <response code="200">Covid19 Data</response>
    [HttpPost("Data",
                 Name = "GetCovid19DataAsync")]
    [ProducesResponseType(StatusCodes.Status200OK,
                             Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest,
                             Type = typeof(string))]
    public async Task<IEnumerable<Covid19DataResponse>> GetCovid19DataAsync(
        [FromBody] Covid19DataFilterRequest covid19DataFilterRequest)
    {
        var filterDto = covid19DataFilterRequest.Adapt<Covid19DataFilterDto>();
        this.logger.LogInformation("Calling Covid19 Data Service with filter: State:{State} && County: {County}",
                                   filterDto.Location.State,
                                   filterDto.Location.County);
        var resultDto = await this.covid19DataService.GetCovid19DataAsync(filterDto);
        var result = resultDto.Adapt<IEnumerable<Covid19DataResponse>>();
        return result;
    }
}
