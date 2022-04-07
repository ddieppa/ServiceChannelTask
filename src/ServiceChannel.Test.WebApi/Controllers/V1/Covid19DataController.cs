using Mapster;

using Microsoft.AspNetCore.Mvc;

namespace ServiceChannel.Test.WebApi.Controllers.V1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class Covid19DataController : ControllerBase
{
    private readonly ICovid19DataService covid19DataService;
    private readonly ILogger<Covid19DataController> logger;

    public Covid19DataController(ICovid19DataService covid19DataService,
                                        ILogger<Covid19DataController> logger)
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
        this.logger.LogInformation("Calling {Service} with filter: State:{State} && County: {County}",
                                   nameof(Covid19DataService),
                                   filterDto.Location.State,
                                   filterDto.Location.County);
        var resultDto = await this.covid19DataService.GetCovid19DataAsync(filterDto);
        var result = resultDto.Adapt<IEnumerable<Covid19DataResponse>>();
        return result;
    }
}
