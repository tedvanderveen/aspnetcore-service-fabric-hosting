﻿using CoherentSolutions.Extensions.Hosting.ServiceFabric.Tools;

namespace CoherentSolutions.Extensions.Hosting.ServiceFabric.Fabric
{
    public interface IServiceHostListenerReplicaTemplate<out TConfigurator> : IConfigurableObject<TConfigurator>
        where TConfigurator : IServiceHostListenerReplicaTemplateConfigurator
    {
    }
}