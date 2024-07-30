using CommandWrapper.Core.Abstractions;
using CommandWrapper.Core.Constants;

namespace CommandWrapper.Sox.Positions.Target.Arguments;

public sealed class TargetArgument : CommandArgument
{
    private readonly string _direction;
    
    private Stream? _stream;
    
    private string? _path;

    public Stream? Stream
    {
        get => _stream;
        set => SetValue(ref _stream, value);
    }
    
    public string? Path
    {
        get => _path;
        set => SetValue(ref _path, value);
    }
    
    internal TargetArgument(string direction) : base(ArgumentFormatters.Default)
    {
        _direction = direction;
    }

    protected override string? Name => null;

    protected override string? Value
    {
        get
        {
            if (Path is null && Stream is null)
                throw new ArgumentOutOfRangeException("...", "Value not set");

            return _stream is not null ? _direction : Path;
        }
    }
}