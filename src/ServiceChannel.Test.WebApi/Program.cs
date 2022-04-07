using System.Net.Mime;
using System.Reflection;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;

using Refit;

using Serilog;

using ServiceChannel.Test.Infrastructure;
using ServiceChannel.Test.WebApi.Options;

Log.Logger = new LoggerConfiguration()
             .WriteTo.Console()
             .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
                                         .WriteTo.Console()
                                         .ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.

    builder.Services
           .AddControllers()
           .AddFluentValidation(options =>
           {
               options.ImplicitlyValidateChildProperties = true;
               options.ImplicitlyValidateRootCollectionElements = true;
               // Automatic registration of validators in assembly
               options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
           });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
        options.DefaultApiVersion = new ApiVersion(1,
                                                   0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    });

    builder.Services.AddVersionedApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

    builder.Services.AddTransient<ICovid19DataService, Covid19DataService>();
    builder.Services.AddScoped<ICovid19HttpClient>(provider => provider.GetRequiredService<ICovid19RefitClient>());
    builder.Services.AddRefitClient<ICovid19RefitClient>(provider => new RefitSettings())
           .ConfigureHttpClient(c =>
           {
               c.BaseAddress =
                   new
                       Uri("https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series");
               c.DefaultRequestHeaders.Add("Accept",
                                           MediaTypeNames.Application.Json);
           });
    builder.Services.AddTransient<ICsvHelper, ServiceChannel.Test.Infrastructure.CsvHelper>();

    var app = builder.Build();

    app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                                        description.ApiVersion.ToString());
            }
        });
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e,
              "Application start-up failed");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
