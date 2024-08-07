﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AlarmApp.Util.Configuration
{
    public static class CustomConfigurationBuilder
    {
        public static IConfiguration Build(string path, string fileName, string environmentName = null, string configString = null)
        {
            if (string.IsNullOrWhiteSpace(environmentName))
            {
                environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            }

            return new ConfigurationBuilder()
                        .SetBasePath(path)
                        .AddJsonFile($"{fileName}.json")
                        .AddEnvironmentVariables()
                        .Build();
        }

        public static IConfiguration Build(Stream stream)
        {
            return new ConfigurationBuilder().AddJsonStream(stream).Build();
        }

        public static IConfiguration Build(string configuration)
        {
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(configuration);
                writer.Flush();
                stream.Position = 0;
                return Build(stream);
            }
        }
    }
}
