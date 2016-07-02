using Attest.Testing.Core;
using Samples.Client.Model.Shared;

namespace Samples.Universal.Client.Tests.Integration.Infra.Shared
{
    public static class TestHelper
    {
        public static void BeforeTeardown()
        {
            //TODO:
        }

        public static void AfterTeardown()
        {
            UserContext.Current = null;            
            ScenarioContext.Current.Clear();
            LogoFX.Client.Testing.Shared.Caliburn.Micro.TestHelper.Teardown();
        }
    }
}
