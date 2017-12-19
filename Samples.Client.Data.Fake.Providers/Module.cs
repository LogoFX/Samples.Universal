using System;
using JetBrains.Annotations;
using Samples.Client.Data.Contracts.Dto;
using Samples.Client.Data.Contracts.Providers;
using Samples.Client.Data.Fake.Containers;
using Samples.Client.Data.Fake.ProviderBuilders;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Samples.Client.Data.Fake.Providers
{
    [UsedImplicitly]
    class Module : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator)
        {
            RegisterDataContainers(dependencyRegistrator);
            RegisterBuilders(dependencyRegistrator);
            RegisterProviders(dependencyRegistrator);            
        }

        private static void RegisterDataContainers(IDependencyRegistrator dependencyRegistrator)
        {
            var warehouseContainer = InitializeWarehouseContainer();
            var userContainer = InitializeUserContainer();
            dependencyRegistrator.RegisterInstance(warehouseContainer);
            dependencyRegistrator.RegisterInstance(userContainer);
        }

        private static IWarehouseContainer InitializeWarehouseContainer()
        {
            var warehouseContainer = new WarehouseContainer();
            warehouseContainer.UpdateWarehouseItems(new[]
            {
                new WarehouseItemDto
                {
                    Kind = "PC",
                    Price = 25.43,
                    Quantity = 8
                }
            });
            return warehouseContainer;
        }

        private static IUserContainer InitializeUserContainer()
        {
            var userContainer = new UserContainer();
            userContainer.UpdateUsers(new[]
            {
                new Tuple<string, string>("User", "pass")
            });
            return userContainer;
        }

        private static void RegisterBuilders(IDependencyRegistrator dependencyRegistrator)
        {
            dependencyRegistrator.RegisterInstance(WarehouseProviderBuilder.CreateBuilder());
            dependencyRegistrator.RegisterInstance(LoginProviderBuilder.CreateBuilder());
        }

        private static void RegisterProviders(IDependencyRegistrator dependencyRegistrator)
        {
            dependencyRegistrator.RegisterSingleton<IWarehouseProvider, FakeWarehouseProvider>();
            dependencyRegistrator.RegisterSingleton<ILoginProvider, FakeLoginProvider>();
        }
    }
}
