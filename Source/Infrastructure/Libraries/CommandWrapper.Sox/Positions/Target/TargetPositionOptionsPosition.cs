using CommandWrapper.Core.Abstractions;
using CommandWrapper.Sox.Positions.Target.Arguments;

namespace CommandWrapper.Sox.Positions.Target;

public sealed class TargetPosition : CommandPosition
{
    public readonly TargetArgument Target = new TargetArgument();
    
    internal TargetPosition(Constants.Priorities priority) : base((int) priority)
    {
        if
        (
            priority is not (Constants.Priorities.InputFile or Constants.Priorities.OutputFile)
        ) throw new ArgumentOutOfRangeException(nameof(priority));
    }
    
    protected override IEnumerable<CommandArgument>? Arguments => [ Target ];
}