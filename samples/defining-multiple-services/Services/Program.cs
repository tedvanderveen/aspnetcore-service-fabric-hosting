﻿using CoherentSolutions.Extensions.Hosting.ServiceFabric;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new HostBuilder()
                .DefineStatefulService(
                    serviceBuilder =>
                    {
                        serviceBuilder
                            .UseServiceType("StatefulServiceType")
                            .DefineAspNetCoreListener(
                                listenerBuilder =>
                                {
                                    listenerBuilder
                                        .UseEndpointName("StatefulServiceEndpoint")
                                        .UseUniqueServiceUrlIntegration()
                                        .ConfigureWebHost(
                                            webHostBuilder => 
                                            {
                                                webHostBuilder.UseStartup<StatefulStartup>();
                                            });
                                });
                    })
                .DefineStatelessService(
                    serviceBuilder =>
                    {
                        serviceBuilder
                            .UseServiceType("StatelessServiceType")
                            .DefineAspNetCoreListener(
                                listenerBuilder =>
                                {
                                    listenerBuilder
                                        .UseEndpointName("StatelessServiceEndpoint")
                                        .UseUniqueServiceUrlIntegration()
                                        .ConfigureWebHost(
                                            webHostBuilder =>
                                            {
                                                webHostBuilder.UseStartup<StatelessStartup>();
                                            });
                                });
                    })
                .Build()
                .Run();
        }
    }
}