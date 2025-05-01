using Microsoft.Extensions.Configuration;
using System.IO;

namespace OnAirLight.CommandLine.Config
{
    public static class ConfigService
    {
        public static AppConfig LoadAppSettings(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>()
                .AddCommandLine(args)
                .Build();


            var config = new AppConfig();

            builder.Bind(config);

            return config;
        }
    }
}
