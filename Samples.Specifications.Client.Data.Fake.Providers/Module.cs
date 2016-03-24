using System;
using System.Composition;
using LogoFX.Samples.Specifications.Client.Data.Contracts.Dto;
using LogoFX.Samples.Specifications.Client.Data.Contracts.Providers;
using LogoFX.Samples.Specifications.Client.Data.Fake.Containers;
using LogoFX.Samples.Specifications.Client.Data.Fake.ProviderBuilders;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace LogoFX.Samples.Specifications.Client.Data.Fake.Providers
{
    [Export(typeof(ICompositionModule))]
    class Module : ICompositionModule<IIocContainer>
    {
        public void RegisterModule(IIocContainer iocContainer)
        {
            RegisterDataContainers(iocContainer);
            RegisterBuilders(iocContainer);
            RegisterProviders(iocContainer);            
        }

        private static void RegisterDataContainers(IIocContainerRegistrator iocContainer)
        {
            var warehouseContainer = InitializeWarehouseContainer();
            var userContainer = InitializeUserContainer();
            iocContainer.RegisterInstance(warehouseContainer);
            iocContainer.RegisterInstance(userContainer);            
        }

        private static IWarehouseContainer InitializeWarehouseContainer()
        {
            var warehouseContainer = new WarehouseContainer();
            warehouseContainer.UpdateWarehouseItems(new []
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
            userContainer.UpdateUsers(new []
            {
                new Tuple<string, string>("User", "pass") 
            });
            return userContainer;
        }

        private static void RegisterBuilders(IIocContainerRegistrator iocContainer)
        {
            iocContainer.RegisterInstance(WarehouseProviderBuilder.CreateBuilder()); 
            iocContainer.RegisterInstance(LoginProviderBuilder.CreateBuilder());           
        }

        private static void RegisterProviders(IIocContainerRegistrator iocContainer)
        {
            iocContainer.RegisterSingleton<IWarehouseProvider, FakeWarehouseProvider>();
            iocContainer.RegisterSingleton<ILoginProvider, FakeLoginProvider>();
        }
    }
}
