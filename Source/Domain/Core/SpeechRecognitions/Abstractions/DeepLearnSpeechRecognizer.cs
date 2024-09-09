
using Core.SpeechRecognitions.Models;

namespace Core.SpeechRecognitions.Abstractions;

public abstract class DeepLearnSpeechRecognizer(string modelPath, params AudioFormat[] audioFormats) : ISpeechRecognizer
{
    protected readonly string ModelPath = modelPath;

    public IEnumerable<AudioFormat> SupportedFormats => audioFormats;
    
    public bool Initialized { get; protected set; }

    public abstract Task InitAsync(CancellationToken token = default);

    public abstract Task<Speech> TranscribeAsync(Stream audioStream, CancellationToken token = default);
}
