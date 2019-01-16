using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Common;
using Solid.Practices.Composition;

namespace Samples.Client.Data.Fake.Shared
{
    public static class ConventionsHelper
    {
        public static Dictionary<Type, Type> FindContractToBuilderMatches()
        {
            var assembliesProvider = new CustomAssemblySourceProvider(PlatformProvider.Current.GetRootPath(),
                new[] { Consts.ContractsAssemblyEnding, Consts.BuildersAssemblyEnding });
            var assemblies = assembliesProvider.Assemblies.ToArray();
            var contractTypes = assemblies.FindContractTypes();
            var contractToBuilderMatches = FindContractToBuilderMatchesImpl(assemblies, contractTypes);
            return contractToBuilderMatches;
        }

        private static Dictionary<Type, Type> FindContractToBuilderMatchesImpl(
            this IEnumerable<Assembly> assemblies,
            Type[] contractTypes)
        {
            var buildersTypes = assemblies.FindBuildersTypes();
            var contractToBuilderMatches = new Dictionary<Type, Type>();
            foreach (var builderType in buildersTypes)
            {
                var contractType =
                    contractTypes.FirstOrDefault(
                        t => t.Name == "I" + builderType.Name.Replace(Consts.BuilderEnding, string.Empty));
                if (contractType != null)
                {
                    contractToBuilderMatches.Add(contractType, builderType);
                }
            }
            return contractToBuilderMatches;
        }

        internal static Type[] FindContractTypes(this IEnumerable<Assembly> assemblies) => assemblies.FindTypes(
            Consts.ContractsAssemblyEnding,
            t => t.InterfaceEndsWith(Consts.ProviderEnding));

        internal static Type[] FindFakeTypes(this IEnumerable<Assembly> assemblies) => assemblies.FindTypes(
            Consts.FakeAssemblyEnding,
            t => t.ClassEndsWith(Consts.ProviderEnding));

        internal static Type[] FindBuildersTypes(this IEnumerable<Assembly> assemblies)
        {
            var buildersAssemblies = assemblies.Where(t => t.GetName().Name.EndsWith(Consts.BuildersAssemblyEnding));
            var buildersTypes = buildersAssemblies.SelectMany(k => k.DefinedTypes
                .Where(t => t.ClassEndsWith(Consts.BuilderEnding))
                .Select(t => t.AsType())).ToArray();
            return buildersTypes;
        }

        private static Type[] FindTypes(this IEnumerable<Assembly> assemblies, string assemblyEnding,
            Func<TypeInfo, bool> criterion) => assemblies.Where(t => t.GetName().Name.EndsWith(assemblyEnding))
            .SelectMany(k => k.DefinedTypes
                .Where(criterion)
                .Select(t => t.AsType())).ToArray();
    }
}