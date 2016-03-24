using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using LogoFX.Client.Bootstrapping;
using LogoFX.Client.Bootstrapping.Adapters.Contracts;
using LogoFX.Client.Bootstrapping.Adapters.WinRTContainer;
using Solid.Practices.Composition;
using Solid.Practices.Composition.Client;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Samples.Universal.Client.Presentation.Shell
{
    public partial class BootstrapperContainerBase<TRootViewModel, TIocContainerAdapter> : CaliburnApplication
        where TRootViewModel : class
        where TIocContainerAdapter : class, IIocContainer, IBootstrapperAdapter, IUniversalAdapter,  new()
    {
        private readonly TIocContainerAdapter _iocContainerAdapter;
        private IBootstrapperAdapter _bootstrapperAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapperContainerBase{TRootViewModel, TIocContainerAdapter}"/> class.
        /// </summary>
        /// <param name="iocContainerAdapter">The ioc container adapter.</param>        
        public BootstrapperContainerBase(
            TIocContainerAdapter iocContainerAdapter)
            : this(iocContainerAdapter, new BootstrapperCreationOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapperContainerBase{TRootViewModel, TIocContainerAdapter}"/> class.
        /// </summary>
        /// <param name="iocContainerAdapter">The ioc container adapter.</param>
        /// <param name="creationOptions">The creation options.</param>
        public BootstrapperContainerBase(
            TIocContainerAdapter iocContainerAdapter,
            BootstrapperCreationOptions creationOptions)
        {
            _iocContainerAdapter = iocContainerAdapter;
            Suspending += OnSuspending;
            if (creationOptions.DiscoverCompositionModules)
            {
                InitializeCompositionModules();
            }
            if (creationOptions.InspectAssemblies)
            {
                InitializeInspectedAssemblies();
            }
            Initialize();
        }

        /// <summary>
        /// Gets the path of composition modules that will be discovered during bootstrapper configuration.
        /// </summary>
        public virtual string ModulesPath
        {
            get { return "."; }
        }

        /// <summary>
        /// Gets the prefixes of the modules that will be used by the module registrator
        /// during bootstrapper configuration. Use this to implement efficient discovery.
        /// </summary>
        /// <value>
        /// The prefixes.
        /// </value>
        public virtual string[] Prefixes
        {
            get { return new string[] {}; }
        }

        /// <summary>
        /// Gets the list of modules that were discovered during bootstrapper configuration.
        /// </summary>
        /// <value>
        /// The list of modules.
        /// </value>
        public IEnumerable<ICompositionModule> Modules { get; private set; }

        private void InitializeCompositionModules()
        {
            Modules = CompositionHelper.GetCompositionModules(ModulesPath, Prefixes,
                    false);
        }

        /// <summary>
        /// Configures the framework and executes boiler-plate registrations.
        /// </summary>
        protected sealed override void Configure()
        {
            base.Configure();
            BootstrapperHelper<TRootViewModel, TIocContainerAdapter>.RegisterCore(_iocContainerAdapter);                      
            InitializeViewLocator();
            InitializeAdapter();          
            BootstrapperHelper<TRootViewModel, TIocContainerAdapter>.InitializeDispatcher();            
            OnConfigure(_iocContainerAdapter);
        }

        private readonly Dictionary<string, Type> _typedic = new Dictionary<string, Type>();

        private void InitializeViewLocator()
        {
            //overriden for performance reasons (Assembly caching)
            ViewLocator.LocateTypeForModelType = (modelType, displayLocation, context) =>
            {
                var viewTypeName = modelType.FullName.Substring(0, modelType.FullName.IndexOf("`") < 0
                    ? modelType.FullName.Length
                    : modelType.FullName.IndexOf("`")
                    ).Replace("Model", string.Empty);

                if (context != null)
                {
                    viewTypeName = viewTypeName.Remove(viewTypeName.Length - 4, 4);
                    viewTypeName = viewTypeName + "." + context;
                }

                Type viewType;
                if (!_typedic.TryGetValue(viewTypeName, out viewType))
                {
                    _typedic[viewTypeName] = viewType = (from assembly in Assemblies
                                                         from type in assembly.DefinedTypes
                                                         where type.FullName == viewTypeName
                                                         select type.AsType()).FirstOrDefault();
                }

                return viewType;
            };
            ViewLocator.LocateForModelType = (modelType, displayLocation, context) =>
            {
                var viewType = ViewLocator.LocateTypeForModelType(modelType, displayLocation, context);

                return viewType == null
                    ? new TextBlock
                    {
                        Text = string.Format("Cannot find view for\nModel: {0}\nContext: {1} .", modelType, context)
                    }
                    : ViewLocator.GetOrCreateViewType(viewType);
            };
        }

        /// <summary>
        /// Override to tell the framework where to find assemblies to inspect for application components.
        /// </summary>
        /// <returns>
        /// A list of assemblies to inspect.
        /// </returns>
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return Assemblies;
        }

        /// <summary>
        /// Gets the assemblies that will be inspected for the application components.
        /// </summary>
        /// <value>
        /// The assemblies.
        /// </value>
        protected Assembly[] Assemblies { get; private set; }

        /// <summary>
        /// Override this to provide custom assembly namespaces collection.
        /// </summary>
        protected virtual void OnConfigureAssemblyResolution()
        {
        }

        private void InitializeInspectedAssemblies()
        {
            Assemblies = GetAssemblies();
        }

        private Assembly[] GetAssemblies()
        {
            OnConfigureAssemblyResolution();
            var assembliesResolver = new AssembliesResolver(GetType(),
                new ClientAssemblySourceProvider(Directory.GetCurrentDirectory()));
            return ((IAssembliesReadOnlyResolver)assembliesResolver).GetAssemblies().ToArray();
        }

        /// <summary>
        /// Override this method to inject custom logic during bootstrapper configuration.
        /// </summary>
        /// <param name="iocContainerAdapter">IoC container adapter</param>
        protected virtual void OnConfigure(TIocContainerAdapter iocContainerAdapter)
        {
        }

        private void InitializeAdapter()
        {
            _bootstrapperAdapter = _iocContainerAdapter;
        }

        /// <summary>
        /// Gets the service by its type and optional <see cref="string"/> key.
        /// Not intended to be used explicitly from code.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected override object GetInstance(Type service, string key)
        {
            return _bootstrapperAdapter.GetInstance(service, key);
        }

        /// <summary>
        /// Gets all instances of service by its type.
        /// Not intended to be used explicitly from code.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _bootstrapperAdapter.GetAllInstances(service);
        }

        /// <summary>
        /// Injects all missing properties from the IoC containerAdapter into the provided object.
        /// Not intended to be used explicitly from code.
        /// </summary>
        /// <param name="instance">The instance.</param>
        protected override void BuildUp(object instance)
        {
            _bootstrapperAdapter.BuildUp(instance);
        }

        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (Debugger.IsAttached)
            {
                DebugSettings.EnableFrameRateCounter = true;
            }
#endif            
            DisplayRootViewFor<TRootViewModel>();
        }

        ///<summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>

        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        protected override void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }        
    }
}
