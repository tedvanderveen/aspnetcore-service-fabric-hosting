﻿namespace CoherentSolutions.Extensions.Hosting.ServiceFabric.Fabric
{
    public interface IStatelessServiceHostRemotingListenerReplicaTemplateConfigurator
        : IStatelessServiceHostListenerReplicaTemplateConfigurator,
          IServiceHostRemotingListenerReplicaTemplateConfigurator
    {
    }
}