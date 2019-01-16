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
                    Id = 1,
                    Kind = "PC",
                    Price = 25.43,
                    Quantity = 8
                },
                new WarehouseItemDto
                {
                    Id = 2,
                    Kind = "PC 2",
                    Price = 25.3,
                    Quantity = 4
                },
                new WarehouseItemDto
                {
                    Id = 3,
                    Kind = "PC 3",
                    Price = 254.3,
                    Quantity = 6
                },
                new WarehouseItemDto
                {
                    Id = 4,
                    Kind = "PC 4",
                    Price = 15.67,
                    Quantity = 9
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
