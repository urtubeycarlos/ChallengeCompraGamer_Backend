using Microsoft.AspNetCore.Mvc;
using Serilog.Sinks.Elasticsearch;
using Serilog;

namespace ChallengeCompraGamer_Backend.App
{
    public static class LoggerConfig
    {
        public static void Add(WebApplicationBuilder builder)
        {
            string elasticUri = builder.Configuration["ElasticConfiguration:Uri"] ?? throw new InvalidOperationException("Connection string 'ElasticConfiguration:Uri' not found.");

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = "apilog-{0:yyyy.MM.dd}"
                })
                .CreateLogger();

            builder.Host.UseSerilog();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    ValidationProblemDetails problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                        Title = "One or more validation errors occurred."
                    };

                    ILogger<Program> logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogError("One or more validation errors occurred. Errors: {@Errors}", problemDetails.Errors);

                    return new BadRequestObjectResult(problemDetails);
                };
            });
        }
    }
}
