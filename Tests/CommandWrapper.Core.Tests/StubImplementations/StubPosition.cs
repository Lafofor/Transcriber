using CommandWrapper.Core.Abstractions;

namespace CommandWrapper.Core.Tests.StubImplementations;

public sealed class StubPosition : CommandPosition
{
    public StubPosition(int priority, IEnumerable<CommandArgument> arguments) : base(priority)
    {
        Arguments = arguments;
    }

    protected override IEnumerable<CommandArgument>? Arguments { get; }
}