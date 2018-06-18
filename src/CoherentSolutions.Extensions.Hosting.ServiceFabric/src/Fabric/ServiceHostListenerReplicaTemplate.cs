﻿using System;
using System.Fabric;

using CoherentSolutions.Extensions.Hosting.ServiceFabric.Tools;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.Runtime;

namespace CoherentSolutions.Extensions.Hosting.ServiceFabric.Fabric
{
    public abstract class ServiceHostListenerReplicaTemplate<TService, TParameters, TConfigurator, TListener>
        : ConfigurableObject<TConfigurator>, IServiceHostListenerReplicaTemplate<TConfigurator>
        where TService : IService
        where TParameters : IServiceHostListenerReplicaTemplateParameters
        where TConfigurator : IServiceHostListenerReplicaTemplateConfigurator
    {
        protected abstract class ListenerParameters
            : IServiceHostListenerReplicaTemplateParameters,
              IServiceHostListenerReplicaTemplateConfigurator
        {
            public string EndpointName { get; private set; }

            public Func<IServiceHostListenerLoggerOptions> LoggerOptionsFunc { get; private set; }

            public Action<IServiceCollection> DependenciesConfigAction { get; private set; }

            protected ListenerParameters()
            {
                this.EndpointName = string.Empty;
                this.LoggerOptionsFunc = DefaultLoggerOptionsFunc;
                this.DependenciesConfigAction = null;
            }

            public void UseEndpointName(
                string endpointName)
            {
                this.EndpointName = endpointName
                 ?? throw new ArgumentNullException(nameof(endpointName));
            }

            public void UseLoggerOptions(
                Func<IServiceHostListenerLoggerOptions> factoryFunc)
            {
                this.LoggerOptionsFunc = factoryFunc
                 ?? throw new ArgumentNullException(nameof(factoryFunc));
            }

            private static IServiceHostListenerLoggerOptions DefaultLoggerOptionsFunc()
            {
                return ServiceHostListenerLoggerOptions.Disabled;
            }

            public void ConfigureDependencies(
                Action<IServiceCollection> configAction)
            {
                if (configAction == null)
                {
                    throw new ArgumentNullException(nameof(configAction));
                }

                this.DependenciesConfigAction = this.DependenciesConfigAction.Chain(configAction);
            }
        }

        public abstract TListener Activate(
            TService service);

        protected abstract Func<ServiceContext, ICommunicationListener> CreateCommunicationListenerFunc(
            TService service,
            TParameters parameters);
    }
}