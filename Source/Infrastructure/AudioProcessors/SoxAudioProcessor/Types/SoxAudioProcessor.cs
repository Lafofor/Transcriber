using CommandWrapper.Sox.Types;
using Core.AudioProcessors;

namespace SoxAudioProcessor.Types;

public sealed class SoxAudioProcessor(SoxCommand command) : IAudioProcessor
{
    private readonly Task<bool> _cached = Task.FromResult(true);

    public Task<bool> AvailableAsync(Stream audioStream, CancellationToken token = default)
        => _cached;

    public async Task<Stream> ProcessAsync(Stream audioStream, CancellationToken token = default)
    {
        using var process = command.Run();
        
        var handledStream = await process.ProcessAudioAsync(audioStream, token);
            
        var output = new MemoryStream((int) audioStream.Length);

        await handledStream.CopyToAsync(output, token);

        await handledStream.FlushAsync(token);
        
        output.Seek(0, SeekOrigin.Begin);
        
        return output;
    }
}