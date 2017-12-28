using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LogoFX.Client.Testing.EndToEnd.FakeData.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Solid.Practices.Composition;

namespace HelloUniversal
{     
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App
    {      
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();     
            Initialize();
            //TODO: put this piece of code into Client.Bootstrapping
            PlatformProvider.Current = new UniversalPlatformProvider();
            //TODO: Resolve file path dynamically and copy the file dynamically as well
            const string path = @"C:\Users\genna\AppData\Local\DevelopmentFiles\27a966ff-8069-4e40-898a-b715cd3fc922VS.Debug_x86.genna\SerializedBuildersCollection.Data";           
            //TODO: the following lines should be removed as they are called inside the Client.Testing.EndToEnd.ProvidersModuleBase class
            string text;
            text = PlatformProvider.Current.ReadText(path);
            var serializerSettings = CreateSerializerSettings();
            //TODO: check why there are two instances of the same builder!
            var builders = JsonConvert.DeserializeObject<BuildersCollection>(text, serializerSettings);
        }

        private static JsonSerializerSettings CreateSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ContractResolver = new FieldsContractResolver()
            };
        }
    }

    internal sealed class FieldsContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var jsonProperties = type.GetRuntimeFields().Select(f => CreateProperty(f, memberSerialization)).ToList();

            foreach (var p in jsonProperties)
            {
                p.Writable = true;
                p.Readable = true;
            }

            return jsonProperties;
        }
    }
}
