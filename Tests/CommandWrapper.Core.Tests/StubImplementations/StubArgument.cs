using CommandWrapper.Core.Abstractions;
using CommandWrapper.Core.Interfaces;

namespace CommandWrapper.Core.Tests.StubImplementations;

public sealed class StubArgument : CommandArgument
{
    public StubArgument(ICommandArgumentFormatter formatter, string? name, string? value, bool hasChanges) :
        base(formatter)
    {
        Name = name;
        Value = value;

        HasChanges = hasChanges;
    }

    protected override string? Name { get; }

    protected override string? Value { get; }
}