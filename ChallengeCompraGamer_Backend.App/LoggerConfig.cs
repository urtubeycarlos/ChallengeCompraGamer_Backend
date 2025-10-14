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
        }
    }
}
