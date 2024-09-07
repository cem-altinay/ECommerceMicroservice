using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Utilities.Loging.Serilog
{
	public static class ElasticsearchLoggingExtensions
	{
		public static LoggerConfiguration UseElasticsearchLogger(this LoggerConfiguration loggerConfiguration, string applicationName, string? elasticUri=null)
		{
			return loggerConfiguration
				.Enrich.FromLogContext()
				.Enrich.WithMachineName()
				.Enrich.WithThreadId()
				.WriteTo.Console()
				.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri ?? "http://localhost:9200"))
				{
					AutoRegisterTemplate = true,
					IndexFormat = $"{applicationName.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
					AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
					FailureCallback = e => Console.WriteLine("Log gönderim hatası: " + e?.Exception?.Message),
					EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog
				});
		}
	}
}
