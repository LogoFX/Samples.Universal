namespace Samples.Universal.Launcher
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
           
            //TODO: Resolve file path dynamically and copy the file dynamically as well - used with Infra.Providers
            const string path = @"C:\Users\genna\AppData\Local\DevelopmentFiles\27a966ff-8069-4e40-898a-b715cd3fc922VS.Debug_x86.genna\SerializedBuildersCollection.Data";                                   
        }
    }

}
