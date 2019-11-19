﻿using System;
using System.Fabric;
using System.Fabric.Description;
using System.IO;
using System.Reflection;
using System.Threading;
using CoherentSolutions.Extensions.Hosting.ServiceFabric.Fabric.Runtime.ServiceManifest.Objects;
using CoherentSolutions.Extensions.Hosting.ServiceFabric.Fabric.Runtime.ServiceManifest.Extensions;
using CoherentSolutions.Extensions.Hosting.ServiceFabric.Fabric.Runtime.ServiceManifest.Objects.Factories;
using CoherentSolutions.Extensions.Hosting.ServiceFabric.Fabric.Runtime.ServiceManifest;

namespace CoherentSolutions.Extensions.Hosting.ServiceFabric.Fabric.Runtime
{
    public static class GhostServiceRuntime
    {
        private const string CODE_PACKAGE_NAME = "Code";

        private const string CODE_PACKAGE_VERSION = "1.0.0";

        private const string SERVICE_NAME = "ServiceName";

        private readonly static byte[] initializationData;

        static GhostServiceRuntime()
        {
            initializationData = new byte[0];
        }

        public static StatelessServiceContext CreateStatelessServiceContext(
            string serviceTypeName)
        {
            var servicePackage = LoadServicePackage();

            var activeCodePackage = new CodePackageFactory()
                .Create(new CodePackageElement
                {
                    Manifest = servicePackage.Manifest,
                    Name = CODE_PACKAGE_NAME,
                    Version = CODE_PACKAGE_VERSION
                });

            var nodeContext = new GhostNodeContext();
            var activationContext = new GhostCodePackageActivationContext(
                servicePackage.Manifest.Name,
                servicePackage.Manifest.Version,
                activeCodePackage,
                new ApplicationPrincipalsDescription(),
                servicePackage.Manifest.ReadConfigurationPackages(),
                servicePackage.Manifest.ReadDataPackages(),
                servicePackage.Manifest.ReadServiceTypesDescriptions(),
                servicePackage.Manifest.ReadServiceEndpoints());

            var servicePartition = new GhostStatelessServiceSingletonPartition();

            return new StatelessServiceContext(
                nodeContext,
                activationContext,
                serviceTypeName,
                new Uri($"{activationContext.ApplicationName}/{SERVICE_NAME}"),
                initializationData,
                Guid.NewGuid(),
                1);
        }

        public static IServicePartition GetPartition(
            Guid parititionId)
        {
            return new GhostStatelessServiceSingletonPartition();
        }

        private static ServicePackage LoadServicePackage()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var current = location;

            var br = false;
            for (; !br;)
            {
                current = Path.GetDirectoryName(current);
                if (current is null)
                {
                    current = Path.GetPathRoot(location);
                    br = true;
                }

                var path = Path.Combine(current, "PackageRoot");
                if (Directory.Exists(path))
                {
                    return ServicePackage.Load(path);
                }
            }

            throw new InvalidOperationException(
                string.Format(
                    "Could not locate 'PackageRoot' directory inside {0} -> {0}{1}..{1} directory tree.", 
                    current, 
                    Path.DirectorySeparatorChar));
        }
    }
}