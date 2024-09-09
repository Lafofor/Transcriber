using Core.SpeechRecognitions.Models;

namespace Core.Pipelines;

public interface IRecognitionPipeline
{
    public Task<Speech> ProcessAsync(Stream audio, CancellationToken token = default);
}