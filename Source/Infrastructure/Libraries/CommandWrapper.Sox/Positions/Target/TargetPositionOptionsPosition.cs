using CommandWrapper.Core.Abstractions;
using CommandWrapper.Sox.Positions.Target.Arguments;

namespace CommandWrapper.Sox.Positions.Target;

public sealed class TargetPosition : CommandPosition
{
    public readonly TargetArgument Target;
    
    internal TargetPosition(Constants.Priorities priority, string direction) : base((int) priority)
    {
        if
        (
            priority is not (Constants.Priorities.InputFile or Constants.Priorities.OutputFile)
        ) throw new ArgumentOutOfRangeException(nameof(priority));

        Target = new TargetArgument(direction);
    }
    
    protected override IEnumerable<CommandArgument>? Arguments => [ Target ];
}