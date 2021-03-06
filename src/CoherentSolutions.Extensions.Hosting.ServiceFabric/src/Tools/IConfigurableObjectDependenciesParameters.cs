﻿using System;

using Microsoft.Extensions.DependencyInjection;

namespace CoherentSolutions.Extensions.Hosting.ServiceFabric.Tools
{
    public interface IConfigurableObjectDependenciesParameters
    {
        Func<IServiceCollection> DependenciesFunc { get; }

        Action<IServiceCollection> DependenciesConfigAction { get; }
    }
}