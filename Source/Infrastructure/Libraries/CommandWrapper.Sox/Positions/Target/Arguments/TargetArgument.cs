using CommandWrapper.Core.Abstractions;
using CommandWrapper.Core.Constants;

namespace CommandWrapper.Sox.Positions.Target.Arguments;

public sealed class TargetArgument : CommandArgument
{
    private string? _path;

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
            if (Path is null)
                throw new ArgumentOutOfRangeException("...", nameof(Path));

            return Path.ToString();
        }
    }
}