using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Common.Security;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace Azure.SpringApps.Sample
{
    public class Program
    {
        private static ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        private static ILogger logger = factory.CreateLogger<Program>();
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args);
            if (Environment.GetEnvironmentVariable("EUREKA_CLIENT_SERVICEURL_DEFAULTZONE") != null)
            {
                registerEureka(builder);
                builder.AddServiceDiscovery(options => options.UseEureka());
            }
            builder.Build()
            .Run();
        }

        private static IHostBuilder registerEureka(IHostBuilder builder)
        {
            builder.ConfigureHostConfiguration(configHost =>
                 {
                     var envs = new Dictionary<string, string>();
                     if (Environment.GetEnvironmentVariable("SPRING_APPLICATION_NAME") != null)
                     {
                         envs.Add("Eureka:Client:AppName", Environment.GetEnvironmentVariable("SPRING_APPLICATION_NAME"));
                     }
                     envs.Add("Eureka:Client:ServiceUrl", Environment.GetEnvironmentVariable("EUREKA_CLIENT_SERVICEURL_DEFAULTZONE"));
                     configHost.AddInMemoryCollection(envs);

                     if (Environment.GetEnvironmentVariable("EUREKA_CLIENT_TLS_ENABLED") != null && Boolean.Parse(Environment.GetEnvironmentVariable("EUREKA_CLIENT_TLS_ENABLED")))
                     {
                         var eurekaClientTLSKeyStore = Environment.GetEnvironmentVariable("EUREKA_CLIENT_TLS_KEYSTORE");
                         if (eurekaClientTLSKeyStore == null)
                         {
                             logger.LogWarning("Eureka Client TLS is enabled but cannot find the path of keystore");
                         }
                         else
                         {
                             if (eurekaClientTLSKeyStore.StartsWith("file://"))
                             {
                                 eurekaClientTLSKeyStore = eurekaClientTLSKeyStore.Substring("file://".Length);
                             }
                             eurekaClientTLSKeyStore = Path.GetFullPath(eurekaClientTLSKeyStore);
                             if (File.Exists(eurekaClientTLSKeyStore))
                             {
                                 configHost.AddCertificateFile(eurekaClientTLSKeyStore);
                             }
                             else
                             {
                                 logger.LogWarning("Eureka Client TLS is enabled but cannot get keystore file.");
                             }
                         }
                     }
                 });
            return builder;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, builder) => builder.AddDynamicConsole())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
