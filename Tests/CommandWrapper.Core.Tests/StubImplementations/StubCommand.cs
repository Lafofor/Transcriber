using CommandWrapper.Core.Abstractions;

namespace CommandWrapper.Core.Tests.StubImplementations;

public sealed class StubCommand : Command
{
    public StubCommand(string executablePath, SortedSet<CommandPosition> positions) : base(executablePath)
    {
        Positions = positions;
    }

    protected override SortedSet<CommandPosition>? Positions { get; }

    public override CommandProcess Run()
    {
        throw new NotSupportedException();
    }
}