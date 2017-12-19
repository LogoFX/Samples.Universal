using System;
using System.Linq;
using System.Reflection;
using Solid.Practices.IoC;

namespace Samples.Client.Tests.Contracts
{
    public static class ContainerRegistratorExtensions
    {
        public static void RegisterAutomagically(
            this IDependencyRegistrator @object,
            Assembly contractsAssembly,
            Assembly implementationsAssembly)
        {
            var contracts =
                contractsAssembly.DefinedTypes.Where(t => t.IsInterface).Select(t => t.AsType()).ToArray();
            var implementations =
                implementationsAssembly.DefinedTypes.Where(
                        t => t.IsInterface == false)
                    .ToArray();
            var contractsInfo = contracts.ToDictionary(t => t.Name, t => t);
            var implementationsInfo = implementations.Where(t => t.Name.StartsWith("<>") == false)
                .ToDictionary(t => t.Name, t => t);
            foreach (var implementationInfo in implementationsInfo)
            {
                contractsInfo.TryGetValue("I" + implementationInfo.Key, out Type match);
                if (match != null)
                {
                    @object.RegisterSingleton(match, implementationInfo.Value.AsType());
                }
            }
        }
    }
}
