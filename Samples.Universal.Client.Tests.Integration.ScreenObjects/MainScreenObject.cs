using Samples.Client.Tests.Contracts.ScreenObjects;
using Samples.Universal.Client.Tests.Integration.Infra.Core;

namespace Samples.Universal.Client.Tests.Integration.ScreenObjects
{
    public class MainScreenObject : IMainScreenObject
    {
        public bool IsActive()
        {
            var main = StructureHelper.GetMain();
            return main.IsActive;
        }
    }
}