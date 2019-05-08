﻿using System;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.ServiceFabric.Services.Runtime;

namespace CoherentSolutions.Extensions.Hosting.ServiceFabric.Fabric
{
    public class StatefulServiceRuntimeRegistrant : IStatefulServiceRuntimeRegistrant
    {
        public Task RegisterAsync(
            string serviceTypeName,
            Func<StatefulServiceContext, StatefulServiceBase> serviceFactory,
            CancellationToken cancellationToken)
        {
            return Microsoft.ServiceFabric.Services.Runtime.ServiceRuntime.RegisterServiceAsync(
                serviceTypeName, 
                serviceFactory, 
                cancellationToken: cancellationToken);
        }

        public Task UnregisterAsync(
            string serviceTypeName,
            CancellationToken cancellationToken)
        {
            // currently we do nothing here.

            return Task.CompletedTask;
        }
    }
}