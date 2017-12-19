using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Solid.Practices.Composition;
using Solid.Practices.IoC;

namespace Samples.Client.Data.Fake.Shared
{
    public static class Helper
    {
        //TODO: Refactor
        private const string ProvidersAssemblyEnding = "Contracts.Providers";
        private const string FakeProvidersAssemblyEnding = "Fake.Providers";
        private const string BuildersAssemblyEnding = "Fake.ProviderBuilders";
        private const string MethodName = "CreateBuilder";
        private const string BuilderEnding = "Builder";
        private const string ProviderEnding = "Provider";
        private const string FakePrefix = "Fake";

        public static void RegisterBuilders(IDependencyRegistrator dependencyRegistrator)
        {
            var assembliesProvider = new BuildersAssemblySourceProvider(PlatformProvider.Current.GetRootPath());
            var allAssemblies = assembliesProvider.Assemblies.ToArray();
            var buildersAssemblies = allAssemblies.Where(t => t.GetName().Name.EndsWith(BuildersAssemblyEnding));
            var matchingBuildersTypes = buildersAssemblies.SelectMany(k => k.DefinedTypes
                .Where(t => t.IsInterface == false && t.IsAbstract == false && t.Name.EndsWith(BuilderEnding))
                .Select(t => t.AsType())).ToArray();
            foreach (var type in matchingBuildersTypes)
            {
                var instance = CreateInstanceImpl(type);
                dependencyRegistrator.RegisterInstance(type, instance);
            }
        }

        public static void RegisterBuildersAndFakeProviders(IDependencyRegistrator dependencyRegistrator)
        {
            var assembliesProvider = new ProvidersAssemblySourceProvider(PlatformProvider.Current.GetRootPath());
            var allAssemblies = assembliesProvider.Assemblies.ToArray();
            var providersAssemblies = allAssemblies.Where(t => t.GetName().Name.EndsWith(ProvidersAssemblyEnding));
            var matchingProvidersContractsTypes = providersAssemblies.SelectMany(k => k.DefinedTypes
                .Where(t => t.IsInterface && t.Name.EndsWith(ProviderEnding))
                .Select(t => t.AsType())).ToArray();
            var fakeProvidersAssemblies = allAssemblies.Where(t => t.GetName().Name.EndsWith(FakeProvidersAssemblyEnding));
            var matchingFakeProvidersTypes = fakeProvidersAssemblies.SelectMany(k => k.DefinedTypes
                .Where(t => t.IsInterface == false && t.IsAbstract == false && t.Name.EndsWith(ProviderEnding))
                .Select(t => t.AsType())).ToArray();
            var fakeTypeMatches = new Dictionary<Type, Type>();
            foreach (var type in matchingFakeProvidersTypes)
            {
                var matchingProviderContractType =
                    matchingProvidersContractsTypes.FirstOrDefault(
                        t => t.Name == "I" + type.Name.Replace(FakePrefix, string.Empty));
                if (matchingProviderContractType != null)
                {
                    fakeTypeMatches.Add(matchingProviderContractType, type);
                }
            }

            var buildersAssemblies = allAssemblies.Where(t => t.GetName().Name.EndsWith(BuildersAssemblyEnding));
            var matchingBuildersTypes = buildersAssemblies.SelectMany(k => k.DefinedTypes
                .Where(t => t.IsInterface == false && t.IsAbstract == false && t.Name.EndsWith(BuilderEnding))
                .Select(t => t.AsType())).ToArray();
            var builderTypeMatches = new Dictionary<Type, Type>();
            foreach (var type in matchingBuildersTypes)
            {
                var matchingProviderType =
                    matchingProvidersContractsTypes.FirstOrDefault(
                        t => t.Name == "I" + type.Name.Replace(BuilderEnding, string.Empty));
                if (matchingProviderType != null)
                {
                    builderTypeMatches.Add(type, matchingProviderType);
                }
            }

            foreach (var typeMatch in fakeTypeMatches)
            {
                dependencyRegistrator.RegisterSingleton(typeMatch.Key, typeMatch.Value);
            }

            foreach (var typeMatch in builderTypeMatches)
            {
                var instance = CreateInstanceImpl(typeMatch.Key);
                dependencyRegistrator.RegisterInstance(typeMatch.Key, instance);
            }
        }

        public static Dictionary<Type, Type> FindProviderMatches()
        {
            var assembliesProvider = new ProvidersAssemblySourceProvider(PlatformProvider.Current.GetRootPath());
            var allAssemblies = assembliesProvider.Assemblies.ToArray();
            var providersAssemblies = allAssemblies.Where(t => t.GetName().Name.EndsWith(ProvidersAssemblyEnding));
            var matchingProvidersTypes = providersAssemblies.SelectMany(k => k.DefinedTypes
                .Where(t => t.IsInterface && t.Name.EndsWith(ProviderEnding))
                .Select(t => t.AsType())).ToArray();
            var buildersAssemblies = allAssemblies.Where(t => t.GetName().Name.EndsWith(BuildersAssemblyEnding));
            var matchingBuildersTypes = buildersAssemblies.SelectMany(k => k.DefinedTypes
                .Where(t => t.IsInterface == false && t.IsAbstract == false && t.Name.EndsWith(BuilderEnding))
                .Select(t => t.AsType())).ToArray();
            var typeMatches = new Dictionary<Type, Type>();
            foreach (var type in matchingBuildersTypes)
            {
                var matchingProviderType =
                    matchingProvidersTypes.FirstOrDefault(
                        t => t.Name == "I" + type.Name.Replace(BuilderEnding, string.Empty));
                if (matchingProviderType != null)
                {
                    typeMatches.Add(type, matchingProviderType);
                }
            }
            return typeMatches;
        }

        public static object CreateInstance(Type type)
        {
            return CreateInstanceImpl(type);
        }

        private static object CreateInstanceImpl(Type type)
        {
            return type.GetRuntimeMethod(MethodName, new Type[] { }).Invoke(null, null);
        }

        public class BuildersAssemblySourceProvider : AssemblySourceProviderBase
        {
            public BuildersAssemblySourceProvider(string rootPath) : base(rootPath)
            {
            }

            protected override string[] ResolveNamespaces()
            {
                return new[] { BuildersAssemblyEnding };
            }
        }

        public class ProvidersAssemblySourceProvider : AssemblySourceProviderBase
        {
            public ProvidersAssemblySourceProvider(string rootPath) : base(rootPath)
            {
            }

            protected override string[] ResolveNamespaces()
            {
                return new[] { ProvidersAssemblyEnding, FakeProvidersAssemblyEnding, BuildersAssemblyEnding };
            }
        }
    }
}
