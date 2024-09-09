using CommandWrapper.Core.Abstractions;

namespace CommandWrapper.Sox.Types;

public sealed class SoxProcess : CommandProcess
{
    private readonly bool _fromStream;

    private readonly string? _outputPathFile;
    
    internal SoxProcess(bool fromStream, string? outputPathFile, string executePath, string arguments) : base(executePath, arguments)
    {
        _fromStream = fromStream;
        _outputPathFile = outputPathFile;
    }

    public async Task<Stream> ProcessAudioAsync(Stream? audio = default, CancellationToken token = default)
    {
        if (_fromStream)
        {
            ArgumentNullException.ThrowIfNull(audio, nameof(audio));

            await audio.CopyToAsync(CreatedProcess.StandardInput.BaseStream, token);
            
            CreatedProcess.StandardInput.Close();
        }

        if (_outputPathFile is not null)
            return File.OpenRead(_outputPathFile);
        else
        {
            var output = CreatedProcess.StandardOutput.BaseStream;
            
            if (output.CanSeek)
                output.Seek(0, SeekOrigin.Begin);

            await output.FlushAsync(token);
            
            return CreatedProcess.StandardOutput.BaseStream;
        }
    }
}