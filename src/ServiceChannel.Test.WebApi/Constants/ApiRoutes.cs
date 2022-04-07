namespace ServiceChannel.Test.WebApi.Constants;

public static class ApiRoutes
{
    public const string BaseRoute = "api/v{version:apiVersion}/[controller]";

    public static class Covid19Data
    {
        public const string County = "county";
    }
}
