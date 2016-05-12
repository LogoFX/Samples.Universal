using Samples.Universal.Client.Presentation.Shell.ViewModels;

namespace Samples.Universal.Client.Tests.Integration.Infra.Core
{
    /// <summary>
    /// Represents structure helper which provides easier API for accessing different parts of application
    /// </summary>
    public static class StructureHelper
    {
        private static ShellViewModel _rootObject;

        /// <summary>
        /// Sets the root object which is the shell view model.
        /// </summary>
        /// <param name="rootObject">The root object.</param>
        public static void SetRootObject(ShellViewModel rootObject)
        {
            _rootObject = rootObject;
        }

        /// <summary>
        /// Gets the shell view model.
        /// </summary>
        /// <returns>Shell view model</returns>
        public static ShellViewModel GetShell()
        {
            return GetShellInternal();
        }

        public static LoginViewModel GetLogin()
        {
            return GetShellInternal().LoginViewModel;
        }

        public static MainViewModel GetMain()
        {
            return GetShellInternal().MainViewModel;
        }

        private static ShellViewModel GetShellInternal()
        {
            return _rootObject;
        }
    }
}