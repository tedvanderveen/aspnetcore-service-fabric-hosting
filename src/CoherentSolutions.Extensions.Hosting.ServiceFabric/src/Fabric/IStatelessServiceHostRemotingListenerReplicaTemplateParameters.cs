﻿namespace CoherentSolutions.Extensions.Hosting.ServiceFabric.Fabric
{
    public interface IStatelessServiceHostRemotingListenerReplicaTemplateParameters
        : IStatelessServiceHostListenerReplicaTemplateParameters,
          IServiceHostRemotingListenerReplicaTemplateParameters
    {
    }
}