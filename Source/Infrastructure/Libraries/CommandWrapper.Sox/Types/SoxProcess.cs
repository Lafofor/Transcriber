using CommandWrapper.Core.Abstractions;

namespace CommandWrapper.Sox.Types;

public sealed class SoxProcess : CommandProcess
{
    internal SoxProcess(string executePath, string arguments) : base(executePath, arguments)
    {
    }
}