using CommandWrapper.Core.Abstractions;
using CommandWrapper.Sox.Positions.Effects;
using CommandWrapper.Sox.Positions.FormatOptions;
using CommandWrapper.Sox.Positions.GlobalOptions;
using CommandWrapper.Sox.Positions.Target;
using CommandWrapper.Sox.Positions.Target.Arguments;

namespace CommandWrapper.Sox.Types;

public sealed class SoxCommand(string executablePath) : Command(executablePath)
{
    private readonly TargetPosition _inputPosition = new TargetPosition(Constants.Priorities.InputFile, "input");
    
    private readonly TargetPosition _outputPosition = new TargetPosition(Constants.Priorities.OutputFile, "output");

    public readonly GlobalOptionsPosition GlobalOptions = new GlobalOptionsPosition();
    
    public readonly FormatOptionsPosition InputFormat = new FormatOptionsPosition(Constants.Priorities.InputFormatOptions);
    
    public TargetArgument Input => _inputPosition.Target;

    public readonly FormatOptionsPosition OutputFormat = new FormatOptionsPosition(Constants.Priorities.OutputFormatOptions);

    public TargetArgument Output => _outputPosition.Target;

    public readonly EffectsPosition Effects = new EffectsPosition();

    protected override SortedSet<CommandPosition>? Positions =>
    [
        GlobalOptions,
        InputFormat,
        _inputPosition,
        OutputFormat,
        _outputPosition,
        Effects
    ];
   
    public override CommandProcess Run()
    {
        throw new NotImplementedException();
    }
}