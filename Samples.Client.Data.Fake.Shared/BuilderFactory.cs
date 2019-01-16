using System;
using System.Reflection;

namespace Samples.Client.Data.Fake.Shared
{
    public static class BuilderFactory
    {
        private const string MethodName = "CreateBuilder";

        public static object CreateBuilderInstance(Type type) => CreateBuilderInstanceImpl(type);

        private static object CreateBuilderInstanceImpl(Type type) => type.GetRuntimeMethod(MethodName, new Type[] { }).Invoke(null, null);
    }
}
