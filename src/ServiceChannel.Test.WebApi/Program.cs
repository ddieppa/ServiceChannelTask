using System.Reflection;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Mvc.ApiExplorer;

using Serilog;

using ServiceChannel.Test.WebApi.Registrars;

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

    builder.Services.AddControllers();

    // FluentValidation configuration
    builder.Services.AddFluentValidation(options =>
    {
        options.ImplicitlyValidateChildProperties = true;
        options.ImplicitlyValidateRootCollectionElements = true;
        // Automatic registration of validators in assembly
        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    });
    //ApiVersioning configuration
    builder.Services.AddApiVersioningExplorer();
    // Swagger configuration
    builder.Services.AddSwagger();

    // Application Project configuration
    builder.Services.AddApplicationServices();
    // Refit clients configuration
    builder.Services.AddRefitClientsServices();
    // Infrastructure Project configuration
    builder.Services.AddInfrastructureServices();

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
