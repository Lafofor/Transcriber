using CommandWrapper.Core.Abstractions;
using CommandWrapper.Core.Constants;

namespace CommandWrapper.Sox.Positions.Target.Arguments;

public sealed class TargetArgument : CommandArgument
{
    private bool _fromStream;
    
    private string? _path;

    public bool FromStream
    {
        get => _fromStream;
        set => SetValue(ref _fromStream, value);
    }
    
    public string? Path
    {
        get => _path;
        set => SetValue(ref _path, value);
    }
    
    internal TargetArgument() : base(ArgumentFormatters.Default)
    {
    }

    protected override string? Name => null;

    protected override string? Value
    {
        get
        {
            if (Path is null && !FromStream)
                throw new ArgumentOutOfRangeException("...", "Value not set");

            return FromStream ? "-" : Path;
        }
    }
}