﻿using System;
using System.Fabric;
using CoherentSolutions.AspNetCore.ServiceFabric.Hosting.Tools;
using Microsoft.AspNetCore.Hosting;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;

namespace CoherentSolutions.AspNetCore.ServiceFabric.Hosting.Fabric
{
    public interface IServiceHostAspNetCoreListenerReplicaTemplateConfigurator
        : IServiceHostListenerReplicaTemplateConfigurator,
          IConfigurableObjectWebHostConfigurator
    {
        void UseEndpointName(
            string endpointName);

        void UseIntegrationOptions(
            ServiceFabricIntegrationOptions integrationOptions);

        void UseAspNetCoreCommunicationListener(
            Func<ServiceContext, string, Func<string, AspNetCoreCommunicationListener, IWebHost>, AspNetCoreCommunicationListener> factoryFunc);
    }
}