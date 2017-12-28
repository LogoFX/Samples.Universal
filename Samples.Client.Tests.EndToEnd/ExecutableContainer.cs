namespace Samples.Client.Tests.EndToEnd
{
    internal interface IExecutableContainer
    {
        string Path { get; }
    }

    class ExecutableContainer : IExecutableContainer
    {
        public string Path => "Samples.Universal.exe";
    }
}