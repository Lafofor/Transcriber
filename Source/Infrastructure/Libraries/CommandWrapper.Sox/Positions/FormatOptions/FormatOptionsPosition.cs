using CommandWrapper.Core.Abstractions;
using CommandWrapper.Sox.Positions.FormatOptions.Arguments;

namespace CommandWrapper.Sox.Positions.FormatOptions;

public sealed class FormatOptionsPosition : CommandPosition
{
    public readonly Rate Rate = new Rate();
    
    internal FormatOptionsPosition(Constants.Priorities priority) : base((int) priority)
    {
        if
        (
            priority is not (Constants.Priorities.InputFormatOptions or Constants.Priorities.OutputFormatOptions)
        ) throw new ArgumentOutOfRangeException(nameof(priority));
    }

    protected override IEnumerable<CommandArgument>? Arguments =>
    [
        Rate
    ];
}