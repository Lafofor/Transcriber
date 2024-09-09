using Core.Pipelines;
using Core.SpeechRecognitions.Models;
using Core.SpeechToText.Services;

namespace Services.Implementations;

public sealed class TranscriberService : ITranscriberService
{
    private readonly IRecognitionPipeline _pipeline;

    public TranscriberService(IRecognitionPipeline pipeline)
    {
        _pipeline = pipeline;
    }

    public async Task<Speech> TranscribeAudioAsync(byte[] audio, CancellationToken token = default)
        => await _pipeline.ProcessAsync(new MemoryStream(audio), token);
}