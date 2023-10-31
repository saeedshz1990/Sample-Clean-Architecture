using Microsoft.Extensions.Logging;
using Serilog;

namespace Sample_Clean_Architecture.Web
{
    public class Program
    {

        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            var configSettings = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json")
       .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configSettings)
                .CreateLogger();

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.ConfigureAppConfiguration(config =>
                    {
                        config.AddConfiguration(configSettings);
                    });
                    webBuilder.ConfigureLogging(logger =>
                    {
                        logger.ClearProviders();
                        logger.AddEventLog();
                        logger.AddFile("logs/logtext.txt");
                        logger.AddSerilog();
                    });
                    webBuilder.UseStartup("Sample_Clean_Architecture.Web");
                    webBuilder.UseIISIntegration();
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());

                });
        }
    }
}
