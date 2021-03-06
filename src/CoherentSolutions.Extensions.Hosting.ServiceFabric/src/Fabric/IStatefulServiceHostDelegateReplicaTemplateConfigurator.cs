﻿using System;

namespace CoherentSolutions.Extensions.Hosting.ServiceFabric.Fabric
{
    public interface IStatefulServiceHostDelegateReplicaTemplateConfigurator
        : IServiceHostDelegateReplicaTemplateConfigurator
    {
        void UseEvent(
            StatefulServiceLifecycleEvent @event);

        void UseDelegateInvoker(
            Func<Delegate, IServiceProvider, IStatefulServiceHostDelegateInvoker> factoryFunc);
    }
}