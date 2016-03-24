using System.Collections.Concurrent;
using Solid.Practices.Modularity;

namespace Samples.Universal.Client.Presentation.Shell
{
    static class CompositionStorage
    {
        private static readonly ConcurrentDictionary<string, ICompositionModule[]> _internalStorage =
            new ConcurrentDictionary<string, ICompositionModule[]>();

        internal static void AddCompositionModules(string rootPath, ICompositionModule[] compositionModules)
        {
            _internalStorage.TryAdd(rootPath, compositionModules);
        }

        internal static ICompositionModule[] GetCompositionModules(string rootPath)
        {
            ICompositionModule[] compositionModules;
            _internalStorage.TryGetValue(rootPath, out compositionModules);
            return compositionModules;
        }
    }
}