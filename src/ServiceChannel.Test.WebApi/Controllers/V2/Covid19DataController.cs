using Mapster;

using Microsoft.AspNetCore.Mvc;

using ServiceChannel.Test.WebApi.Constants;

namespace ServiceChannel.Test.WebApi.Controllers.V2;

[ApiVersion(ApiVersions.V2)]
[Route(ApiRoutes.BaseRoute)]
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
    [HttpPost(ApiRoutes.Covid19Data.County,
                 Name = nameof(GetCovid19DataAsync))]
    [ProducesResponseType(StatusCodes.Status200OK,
                             Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest,
                             Type = typeof(string))]
    public async Task<IEnumerable<Covid19DataResponse>> GetCovid19DataAsync(
        [FromBody] Covid19DataFilterRequest covid19DataFilterRequest)
    {
       this.logger.LogInformation("GetCovid19DataAsync Version 2");
        return new List<Covid19DataResponse>();
    }
}
