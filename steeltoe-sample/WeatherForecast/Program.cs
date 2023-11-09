using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Steeltoe.Common.Options;
using Steeltoe.Common.Security;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EndpointSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .AddServiceDiscovery(options => options.UseEureka())
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, builder) => builder.AddDynamicConsole())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.AddInMemoryCollection(new Dictionary<string, string>()
                    {
                       { "Eureka:Client:ServiceUrl", "https://dixue-asae-prod.svc.azuremicroservices.io/eureka/default/eureka" },
                       { "Eureka:Client:AppName", "weatherforecast" },
                       { "Spring:Application:Name", "weatherforecast"}
                    });
                    configHost.AddCertificateFile("/etc/azure-spring-cloud/certs/service-runtime-client-cert.p12");
                });
        }
}
